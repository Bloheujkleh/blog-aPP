using WebApplication7.Controllers;
using WebApplication7.Models;
using System.Linq;

namespace WebApplication7.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public bool AddPost(Post post)
        {
            post.Id = 0;
            _ctx.Posts.Add(post);
            _ctx.SaveChanges();
            return true;
        }

        public List<Post> GetAllPosts()
        {
            return _ctx.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return _ctx.Posts.FirstOrDefault(p => p.Id == id);
        }
        public bool RemovePost(int id)
        {
            var post = GetPost(id);
            if (post == null) return false;

            _ctx.Posts.Remove(post);
            return true;
        }

        public bool UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
            return true;
        }
        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;

        }
    }
}
