using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Antiforgery;

namespace BlogsAPi.Models;

public class Pagination
{
    [Required]
    [Range(1,10000)]
    public int Page { get; set; }
    [Required]
    [Range(1,10000)]
    public int Limit { get; set; }
}
