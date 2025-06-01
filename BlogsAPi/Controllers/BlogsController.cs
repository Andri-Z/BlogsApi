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
        public BlogsController(BlogsService blogs)=>
            _blogs = blogs;
        [HttpGet]
        public async Task<ActionResult<Blogs>> Get()
        {
            List<Blogs> ListBlogs = await _blogs.GetBlogsAsyn();
            if (!ListBlogs.Any())
                return NotFound();

            return Ok(ListBlogs);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Blogs>> Get(string id)
        {
            try
            {
                if (ObjectId.TryParse(id, out ObjectId _id))
                {
                    var result = await _blogs.GetBlogsById(_id);
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
        [HttpPost]
        public async Task<ActionResult> Post(Blogs blogs)
        {
            await _blogs.CreateBlogsAsync(blogs);

            return CreatedAtAction(nameof(Get), new { id = blogs.Id }, blogs);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, Blogs blog)
        {
            try
            {
                if (ObjectId.TryParse(id, out ObjectId _id))
                {
                    var result = await _blogs.UpdateBlogsAsync(_id, blog);
                    if (result.ModifiedCount > 0)
                        return CreatedAtAction(nameof(Get), new { id = blog.Id }, blog);
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
                return BadRequest();
            }
        }
    }
}
