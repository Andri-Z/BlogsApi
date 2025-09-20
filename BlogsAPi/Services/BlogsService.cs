using BlogsAPi.Interfaces;
using BlogsAPi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlogsAPi.Services
{
    public class BlogsService : IBlogs
    {
        private readonly IMongoCollection<Blogs> _blogs;
        public BlogsService(MongoClient client, IOptions<BlogsSettings> options)
        {
            var database = client.GetDatabase(options.Value.DatabaseName);
            _blogs = database.GetCollection<Blogs>(options.Value.CollectionName);
        }
        public async Task<ApiResponse<List<Blogs>>> GetBlogsAsync(Pagination pag)
        {
            var data = await _blogs.Find(x => true)
                                   .SortBy(x => x.Id)
                                   .Skip((pag.Page - 1) * pag.Limit)
                                   .Limit(pag.Limit)
                                   .ToListAsync();
            return new ApiResponse<List<Blogs>>(data.Count, pag, data);
        }
        public async Task<ApiResponse<Blogs>> GetBlogsByIdAsync(string id)
        {
            var blog = await _blogs.Find(x => x.Id == id).FirstOrDefaultAsync();
            return new ApiResponse<Blogs>(1,new Pagination{Page=1},blog);
        }

        public async Task<ApiResponse<List<Blogs>>> GetBlogsByTermAsync(string term, Pagination pag)
        {
            var filtro = Builders<Blogs>.Filter.Or(
                Builders<Blogs>.Filter.Regex(x => x.Title, new BsonRegularExpression(term, "i")),
                Builders<Blogs>.Filter.Regex(x => x.Category, new BsonRegularExpression(term, "i")),
                Builders<Blogs>.Filter.Regex(x => x.Content, new BsonRegularExpression(term, "i"))
            );
            var data = await _blogs.Find(filtro)
                                        .SortBy(x => x.Title)
                                        .Skip((pag.Page - 1) * pag.Limit)
                                        .Limit(pag.Limit)
                                        .ToListAsync();
            return new ApiResponse<List<Blogs>>(data.Count, new Pagination {Page = 1}, data);                    
        }
        
        public async Task<UpdateResult> PutBlogsAsync(string id, Blogs blog)
        {
            var update = Builders<Blogs>.Update
                                        .Set(x => x.Title, blog.Title)
                                        .Set(x => x.Content, blog.Content)
                                        .Set(x => x.Category, blog.Category)
                                        .Set(x => x.Tags, blog.Tags)
                                        .Set(x => x.UpdatedAt, blog.UpdatedAt);
            return await _blogs.UpdateOneAsync(x => x.Id == id, update);
        }
        public async Task PostBlogsAsync(Blogs blog)
        {
            blog.CreatedAt = DateTime.UtcNow;
            await _blogs.InsertOneAsync(blog);
        }
        public async Task<DeleteResult> DeleteBlogsAsync(string id) =>
            await _blogs.DeleteOneAsync(x => x.Id == id);
    }
}