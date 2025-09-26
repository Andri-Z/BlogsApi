using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Antiforgery;

namespace BlogsAPi.Models;

public class Pagination
{
    [Required]
    [Range(1,10000)]
    [JsonPropertyName("page")]
    public int Page { get; set; }
    [Required]
    [Range(1,10000)]
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
}
