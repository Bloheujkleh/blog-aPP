using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApplication7.Models;
using WebApplication7.Data.Repository;


namespace WebApplication7.Controllers
{
    [Authorize(Roles="Admin")]
    
    public class PanelController : Controller
    {
        private IRepository _repo;
        public PanelController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();

            return View(posts);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Post());

            else
            {
                var post = _repo.GetPost((int)id);
                return View(post);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.Id == 0)
            {
                _repo.AddPost(post);
            }
            else
            {
                _repo.UpdatePost(post);
            }

            if (await _repo.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }


            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int? id)
        {
            _repo.RemovePost((int)id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");

        }



    }
}
