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

        [HttpGet]
        public async Task<ActionResult<ApiResponse<Blogs>>> Get([FromQuery]Pagination pag)
        {
            var blogList = await _blogsService.GetBlogsAsync(pag);
            if (!blogList.Data.Any())
                return NotFound();

            return Ok(blogList);
        }
        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse<Blogs>>> Get([FromQuery]string id)
        {
            if (ObjectId.TryParse(id, out ObjectId _id))
            {
                var result = await _blogsService.GetBlogsByIdAsync(_id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("search")]
        public async Task<ActionResult<List<Blogs>>> GetblogsByTerm([FromQuery] string term, Pagination pag)
        {
            var result = await _blogsService.GetBlogsByTermAsync(term,pag);
            if (result.Data.Count == 0)
                return NotFound();
            
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> PostBlogs([FromBody] BlogsDTO blog)
        {
            var newBlog = new Blogs(blog);
            await _blogsService.PostBlogsAsync(newBlog);
            return CreatedAtAction(nameof(Get), new { id = newBlog.Id }, newBlog);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBlogs(string id, BlogsDTO blog)
        {
            try
            {
                if (ObjectId.TryParse(id, out ObjectId _id))
                {
                    var newBlog = new Blogs(blog)
                    {
                        Title = blog.Title,
                        Content = blog.Content,
                        Category = blog.Category,
                        Tags = blog.Tags,
                        UpdatedAt = DateTime.UtcNow
                    };
                    var result = await _blogsService.PutBlogsAsync(_id, newBlog);
                    var updateBlog = await _blogsService.GetBlogsByIdAsync(_id);
                    if (result.ModifiedCount > 0)
                        return Ok(updateBlog);
                    else
                        return BadRequest();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlogs (string id)
        {
            if(ObjectId.TryParse(id, out ObjectId _id))
            {
                var result = await _blogsService.DeleteBlogsAsync(_id);
               if (result.DeletedCount > 0)
                    return NoContent();
                else
                    return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}