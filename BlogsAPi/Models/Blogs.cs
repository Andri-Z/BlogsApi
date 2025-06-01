using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.ComponentModel.DataAnnotations;

namespace BlogsAPi.Models
{
    public class Blogs
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [Required]
        [MinLength(1)]
        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;
        [BsonElement("content")]
        public string Content { get; set; } = string.Empty;
        [BsonElement("category")]
        public string Category { get; set; } = string.Empty;
        [BsonElement("tags")]
        public string[] Tags { get; set; } = Array.Empty<string>();
        [BsonElement("createAt")]
        public DateTime CreateAt { get; set; } = DateTime.Now;
        [BsonElement("updateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}
