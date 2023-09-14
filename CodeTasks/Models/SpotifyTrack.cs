using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTasks.Models
{
    public class SpotifyTrack
    {
        [BsonId]
        public long Id { get; set; }
        public string? TrackId { get; set; }
        public string? Artists { get; set; }
        public string? AlbumName { get; set; }
        public string? TrackName { get; set; }
        public long Popularity { get; set; }
        public long DurationMS { get; set; }
        public bool Explicit { get; set; }
        public string? Genre { get; set; }

        internal object Sort(SortDefinition<SpotifyTrack> sort)
        {
            throw new NotImplementedException();
        }
    }
}
