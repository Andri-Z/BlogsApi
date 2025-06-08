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
    public class BlogsController : Controller
    {
        private readonly IBlogs _blogs;
        public BlogsController(IBlogs blogs) =>
            _blogs = blogs;
        [HttpGet]
        public async Task<ActionResult<List<Blogs>>> GetBlogs()
        {
            List<Blogs> ListBlogs = await _blogs.GetBlogsAsync();
            if (!ListBlogs.Any())
                return NotFound();

            return Ok(ListBlogs);
        }
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Blogs>> GetBlogsById(string id)
        {
            try
            {
                if (ObjectId.TryParse(id, out ObjectId _id))
                {
                    var result = await _blogs.GetBlogsByIdAsync(_id);
                    if (result == null)
                        return NotFound("Blog no encontrado");

                    return Ok(result);
                }
                else
                {
                    return BadRequest("Este Id no es valido");
                }
            }
            catch
            {
                return BadRequest("Ha ocurrido un error durante la operación.");
            }

        }
        [HttpGet("search")]
        public async Task<ActionResult<List<Blogs>>> GetblogsByTerm([FromQuery] string term)
        {
            try
            {
                var result = await _blogs.GetBlogsByTermAsync(term);
                if (result.Count == 0)
                    return NotFound();
                else
                    return Ok(result);
            }
            catch
            {
                return BadRequest("Ha ocurrido un error durante la operacion.");
            }

        }
        [HttpPost]
        public async Task<ActionResult> PostBlogs([FromBody] BlogsDTO blog)
        {
            try
            {
                var newBlog = new Blogs
                {
                    Id = ObjectId.GenerateNewId(),
                    Title = blog.Title,
                    Content = blog.Content,
                    Category = blog.Category,
                    Tags = blog.Tags,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _blogs.PostBlogsAsync(newBlog);
                return CreatedAtAction(nameof(GetBlogsById), new { id = newBlog.Id }, newBlog);
            }
            catch
            {
                return BadRequest("Ha ocurrido un error al crear el blog.");
            }

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBlogs(string id, BlogsDTO blog)
        {
            try
            {
                if (ObjectId.TryParse(id, out ObjectId _id))
                {
                    var newBlog = new Blogs
                    {
                        Title = blog.Title,
                        Content = blog.Content,
                        Category = blog.Category,
                        Tags = blog.Tags,
                        UpdatedAt = DateTime.UtcNow
                    };
                    var result = await _blogs.PutBlogsAsync(_id, newBlog);
                    var updateBlog = await _blogs.GetBlogsByIdAsync(_id);
                    if (result.ModifiedCount > 0)
                        return Ok(updateBlog);
                    else
                        return BadRequest("Ha ocurrido un error al actualizar el archivo.");
                }
                else
                {
                    return BadRequest("El id no es correcto.");
                }
            }
            catch
            {
                return BadRequest("Ha ocurrido un error durante la operacion.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlogs (string id)
        {
            try
            {
                if(ObjectId.TryParse(id, out ObjectId _id))
                {
                    var result = await _blogs.DeleteBlogsAsync(_id);
                    if (result.DeletedCount > 0)
                        return NoContent();
                    else
                        return NotFound("Este blog no existe o fue eliminado.");
                }
                else
                {
                    return BadRequest("Este id no es valido.");
                }
            }
            catch
            {
                return BadRequest("Ha ocurrido un error durante la operacion.");
            }
        }
    }
}