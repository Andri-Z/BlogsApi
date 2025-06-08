using BlogsAPi.DTOs;
using BlogsAPi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlogsAPi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BlogsController : Controller
    {
        private readonly BlogsService _blogs;
        public BlogsController(BlogsService blogs) =>
            _blogs = blogs;
        [HttpGet]
        public async Task<ActionResult<Blogs>> Get()
        {
            List<Blogs> ListBlogs = await _blogs.GetBlogsAsyn();
            if (!ListBlogs.Any())
                return NotFound();

            return Ok(ListBlogs);
        }
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Blogs>> Get(string id)
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
        [HttpGet("search/{term}")]
        public async Task<ActionResult<Blogs>> GetblogsByTerm(string term)
        {
            try
            {
                var result = await _blogs.BusquedaAsync(term);
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
        public async Task<ActionResult> Post([FromBody] BlogsDTO blog)
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
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };

                await _blogs.CreateBlogsAsync(newBlog);
                return CreatedAtAction(nameof(Get), new { id = newBlog.Id},newBlog);
            }
            catch
            {
                return BadRequest("Ha ocurrido un error al crear el blog.");
            }
            
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, BlogsDTO blog)
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
                        UpdateAt = DateTime.UtcNow
                    };
                    var result = await _blogs.UpdateBlogsAsync(_id, newBlog);
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
        [HttpDelete]
        public async Task<ActionResult> Delete (string id)
        {
            try
            {
                if(ObjectId.TryParse(id, out ObjectId _id))
                {
                    var result = await _blogs.DeleteAsync(_id);
                    if (result.DeletedCount > 0)
                        return Ok("El blog fue eliminado correctamente.");
                    else if (result.DeletedCount == 0 && result.IsAcknowledged == true)
                        return BadRequest("Este blog ya ha sido eliminado");
                    else
                        return BadRequest();
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
