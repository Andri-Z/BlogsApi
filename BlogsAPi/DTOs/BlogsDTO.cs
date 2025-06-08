using BlogsAPi.Models;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BlogsAPi.DTOs
{
    public class BlogsDTO
    {
        [Required]
        [MinLength(2)]
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string[] Tags { get; set; } = Array.Empty<string>();
    }
}
