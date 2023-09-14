using CodeTasks.Models;
using CodeTasks.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTasks.Tasks
{
    public class SpotifyTracks
    {
        private SpotifyTrackRepository _spotifyTrackRepository;

        public SpotifyTracks(SpotifyTrackRepository spotifyTrackRepository)
        {
            this._spotifyTrackRepository = spotifyTrackRepository;
        }

        // Returns an unsorted list of all tracks.
        public async Task<List<SpotifyTrack>> GetAllTracks()
        {
            return await this._spotifyTrackRepository.GetAllTracks();
        }

        // Returns a list of the top N most popular tracks.
        public async Task<List<SpotifyTrack>> GetNMostPopular(int numberOfTracks)
        {
            var filter = Builders<SpotifyTrack>.Filter.Empty;
            var sort = Builders<SpotifyTrack>.Sort.Descending(x => x.Popularity);
            var result = await _spotifyTrackRepository.GetCollection().Find(filter).Sort(sort).Limit(numberOfTracks).ToListAsync();
            return result;
        }

        // Returns a list of all genres sorted alphabetically.
        public async Task<List<string>> GetAllGenreSortedAZ()
        {
            var pipeline = new[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    {"_id", "$Genre"}
                }),
                new BsonDocument("$sort", new BsonDocument
                {
                    {"_id", 1}
                })
            };

            var cursor = await _spotifyTrackRepository.GetCollection().AggregateAsync<BsonDocument>(pipeline);

            var genres = cursor.ToList().Select(x => x["_id"].AsString).ToList();

            return genres;
        }

        // Returns a list of N Alubms with there track names sorted by the number of tracks in each genre.
        public async Task<List<(string albumName, List<string> TrackNames)>> GetTopNAlbumsWithTrackNamesWithTheMostTracksWithinAlbum(int numberOfAlbums)
        {
            var pipeline = new[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    {"_id", "$AlbumName"},
                    {"TrackNames", new BsonDocument("$addToSet", "$TrackName")}
                }),
                new BsonDocument("$project", new BsonDocument
                {
                    {"AlbumName", "$_id"},
                    {"TrackNames", 1},
                    {"_id", 0}
                }),
                new BsonDocument("$sort", new BsonDocument
                {
                    {"TrackCount", -1} // Sort by the number of tracks in descending order
                }),
                new BsonDocument("$limit", numberOfAlbums)
            };

            var cursor = await _spotifyTrackRepository.GetCollection().AggregateAsync<BsonDocument>(pipeline);

            var albumsWithTracks = cursor.ToList()
                .Select(doc => (doc["AlbumName"].AsString, doc["TrackNames"].AsBsonArray.Select(t => t.AsString).ToList()))
                .ToList();

            return albumsWithTracks;
        }

        // Returns The Track Name Which is used the most (alot of songs have the same title)
        public async Task<string> GetTheMostUsedSongTitle()
        {
            var pipeline = new[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    {"_id", "$TrackName"},
                    {"count", new BsonDocument("$sum", 1)}
                }),
                new BsonDocument("$sort", new BsonDocument
                {
                    {"count", -1}
                }),
                new BsonDocument("$limit", 1)
            };

            var cursor = await _spotifyTrackRepository.GetCollection().AggregateAsync<BsonDocument>(pipeline);

            var mostUsedSong = cursor.FirstOrDefault();

            if (mostUsedSong != null)
            {
                return mostUsedSong["_id"].AsString;
            }

            return "No songs found";
        }
    }
}
