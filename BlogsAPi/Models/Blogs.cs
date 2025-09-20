using BlogsAPi.DTOs;
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
        public ObjectId? Id { get; set; }
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
        public DateTime CreatedAt { get; set; }
        [BsonElement("updateAt")]
        public DateTime UpdatedAt { get; set; }
        public Blogs(BlogsDTO blogs)
        {
            Id = ObjectId.GenerateNewId();
            Title = blogs.Title;
            Content = blogs.Content;
            Category = blogs.Category;
            Tags = blogs.Tags;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
