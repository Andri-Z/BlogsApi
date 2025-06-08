using BlogsAPi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace BlogsAPi.Interfaces
{
    public interface IBlogs
    {
        Task<List<Blogs>> GetBlogsAsync();
        Task<Blogs> GetBlogsByIdAsync(ObjectId id);
        Task<List<Blogs>> GetBlogsByTermAsync(string term);
        Task PostBlogsAsync(Blogs blog);
        Task<UpdateResult> PutBlogsAsync(ObjectId id, Blogs blog);
        Task<DeleteResult> DeleteBlogsAsync(ObjectId id);
    }
}
