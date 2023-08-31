using CodeTasks.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTasks.Repository
{
    public class SpotifyTrackRepository
    {
        private IMongoCollection<SpotifyTrack> _collection;

        public SpotifyTrackRepository(IMongoDatabase mongoDatabase)
        {
            this._collection = mongoDatabase.GetCollection<SpotifyTrack>(nameof(SpotifyTrack));
        }

        public async Task<List<SpotifyTrack>> GetAllTracks()
        {
            return await this._collection.Find(_ => true).ToListAsync();
        }
    }
}
