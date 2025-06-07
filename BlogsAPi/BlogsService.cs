using BlogsAPi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlogsAPi
{
    public class BlogsService
    {
        private readonly IMongoCollection<Blogs> _blogsCollection;
        public BlogsService(IOptions<BlogsSettings> options) 
        {
            var mongoClient = new MongoClient(options.Value.BlogsApi);
            var database = mongoClient.GetDatabase(options.Value.DatabaseName);
            _blogsCollection = database.GetCollection<Blogs>(options.Value.CollectionName);
        }
        public async Task<List<Blogs>> GetBlogsAsyn() =>
            await _blogsCollection.Find(x => true).ToListAsync();
        public async Task<Blogs> GetBlogsByIdAsync(ObjectId id) =>
            await _blogsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateBlogsAsync(Blogs blog) =>
            await _blogsCollection.InsertOneAsync(blog);
        public async Task<UpdateResult> UpdateBlogsAsync(ObjectId id, Blogs blog)
        {
            var update = Builders<Blogs>.Update
                                        .Set(x => x.Title, blog.Title)
                                        .Set(x => x.Content, blog.Content)
                                        .Set(x => x.Category, blog.Category)
                                        .Set(x => x.Tags, blog.Tags)
                                        .Set(x => x.UpdateAt, blog.UpdateAt);
           return await _blogsCollection.UpdateOneAsync(x => x.Id == id, update);
        }
           
        public async Task<DeleteResult> DeleteAsync(ObjectId id) =>
            await _blogsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
