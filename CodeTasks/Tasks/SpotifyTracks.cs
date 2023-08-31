using CodeTasks.Models;
using CodeTasks.Repository;
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
            return new List<SpotifyTrack>();
        }

        // Returns a list of all genres sorted alphabetically.
        public async Task<List<string>> GetAllGenreSortedAZ()
        {
            return new List<string>();
        }

        // Returns a list of N Alubms with there track names sorted by the number of tracks in each genre.
        public async Task<
            List<(string albumName, List<string> TrackNames)>
        > GetTopNAlbumsWithTrackNamesWithTheMostTracksWithinAlbum(int numberOfAlbums)
        {
            return new List<(string albumName, List<string> TrackNames)>();
        }

        // Returns The Track Name Which is used the most (alot of songs have the same title)
        public async Task<string> GetTheMostUsedSongTitle()
        {
            return string.Empty;
        }
    }
}
