
using BlogsAPi.DTOs;
using BlogsAPi.Interfaces;
using BlogsAPi.Models;
using BlogsAPi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlogsAPi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogs _blogsService;
        public BlogsController(IBlogs blogsService) =>
            _blogsService = blogsService;

        [HttpGet] //Get: api/v1/Blogs/page?=1&limit?=1
        public async Task<ActionResult<ApiResponse<Blogs>>> Get([FromQuery] Pagination pag)
        {
            var blogList = await _blogsService.GetBlogsAsync(pag);
            if (!blogList.Data.Any())
                return NotFound();

            return Ok(blogList);
        }
        [HttpGet("{id:length(24)}")] //Get: api/v1/Blogs/id
        public async Task<ActionResult<ApiResponse<Blogs>>> Get(string id)
        {
            var result = await _blogsService.GetBlogsByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("search")] //Get: api/v1/Blogs/search/term?=
        public async Task<ActionResult<List<Blogs>>> GetblogsByTerm(string term, [FromQuery] Pagination pag)
        {
            var result = await _blogsService.GetBlogsByTermAsync(term, pag);
            if (!result.Data.Any())
                return NotFound();

            return Ok(result);
        }
        [HttpPost] //Post: api/v1/Blogs
        public async Task<ActionResult> PostBlogs([FromBody] BlogsDTO blog)
        {
            var newBlog = new Blogs(blog);
            await _blogsService.PostBlogsAsync(newBlog);
            return CreatedAtAction(nameof(Get), new { id = newBlog.Id }, newBlog);
        }
        [HttpPut("{id:length(24)}")] //Put: api/v1/Blogs/id
        public async Task<ActionResult> PutBlogs(string id, BlogsDTO blog)
        {
            var newBlog = new Blogs(blog);
            var result = await _blogsService.PutBlogsAsync(id, newBlog);
            if (result is not null)
                return Ok(result);
            else
                return NotFound();
        }
        [HttpDelete("{id}")] //Delete: api/v1/Blogs/id
        public async Task<ActionResult> DeleteBlogs(string id)
        {
            var result = await _blogsService.DeleteBlogsAsync(id);
            if (result is null)
                return BadRequest();

            if (result.DeletedCount > 0)
                return NoContent();
            else
                return NotFound();
        }
    }
}