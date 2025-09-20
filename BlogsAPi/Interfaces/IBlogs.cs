using BlogsAPi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace BlogsAPi.Interfaces
{
    public interface IBlogs
    {
        Task<ApiResponse<List<Blogs>>> GetBlogsAsync(Pagination pag);
        Task<ApiResponse<Blogs>> GetBlogsByIdAsync(string id);
        Task<ApiResponse<List<Blogs>>> GetBlogsByTermAsync(string term,Pagination pag);
        Task PostBlogsAsync(Blogs blog);
        Task<UpdateResult> PutBlogsAsync(string id, Blogs blog);
        Task<DeleteResult> DeleteBlogsAsync(string id);
    }
}
