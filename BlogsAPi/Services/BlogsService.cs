using BlogsAPi.Interfaces;
using BlogsAPi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlogsAPi.Services
{
    public class BlogsService : IBlogs
    {
        private readonly IMongoCollection<Blogs> _blogsCollection;
        public BlogsService(MongoClient client, IOptions<BlogsSettings> options)
        {
            var database = client.GetDatabase(options.Value.DatabaseName);
            _blogsCollection = database.GetCollection<Blogs>(options.Value.CollectionName);
        }
        public async Task<List<Blogs>> GetBlogsAsync() =>
            await _blogsCollection.Find(x => true).ToListAsync();
        public async Task<Blogs> GetBlogsByIdAsync(ObjectId id) =>
            await _blogsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<List<Blogs>> GetBlogsByTermAsync(string term)
        {
            var filtro = Builders<Blogs>.Filter.Or(
                Builders<Blogs>.Filter.Regex(x => x.Title, new BsonRegularExpression(term, "i")),
                Builders<Blogs>.Filter.Regex(x => x.Category, new BsonRegularExpression(term, "i")),
                Builders<Blogs>.Filter.Regex(x => x.Content, new BsonRegularExpression(term, "i"))
            );
            return await _blogsCollection.Find(filtro).ToListAsync();
        }
        public async Task PostBlogsAsync(Blogs blog) =>
            await _blogsCollection.InsertOneAsync(blog);
        public async Task<UpdateResult> PutBlogsAsync(ObjectId id, Blogs blog)
        {
            var update = Builders<Blogs>.Update
                                        .Set(x => x.Title, blog.Title)
                                        .Set(x => x.Content, blog.Content)
                                        .Set(x => x.Category, blog.Category)
                                        .Set(x => x.Tags, blog.Tags)
                                        .Set(x => x.UpdatedAt, blog.UpdatedAt);
            return await _blogsCollection.UpdateOneAsync(x => x.Id == id, update);
        }
        public async Task<DeleteResult> DeleteBlogsAsync(ObjectId id) =>
            await _blogsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
