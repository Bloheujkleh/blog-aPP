using WebApplication7.Models;

namespace WebApplication7.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        bool  RemovePost(int id);
        bool UpdatePost(Post post);
        bool AddPost(Post post);
        Task<bool> SaveChangesAsync();
    }
}
