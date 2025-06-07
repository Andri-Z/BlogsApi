using BlogsAPi.Models;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BlogsAPi.DTOs
{
    public class BlogsDTO
    {
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
    }
}
