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
        public async Task<Blogs> GetBlogsById(ObjectId id) =>
            await _blogsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateBlogsAsync(Blogs blog) =>
            await _blogsCollection.InsertOneAsync(blog);
        public async Task<ReplaceOneResult> UpdateBlogsAsync(ObjectId id, Blogs blog)=>
            await _blogsCollection.ReplaceOneAsync(x => x.Id == id, blog);
        public async Task<DeleteResult> DeleteAsync(ObjectId id) =>
            await _blogsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
