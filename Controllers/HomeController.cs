using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Data;
using WebApplication7.Data.Repository;
using WebApplication7.Models;

namespace WebApplication7.Controllers;

public class HomeController : Controller
{
    private IRepository _repo;

    public HomeController(IRepository repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        var posts = _repo.GetAllPosts();
      
        return View(posts);
    }

    public IActionResult Post(int id)
    {
        var post = _repo.GetPost(id);
      
        return View(post);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if(id==null)    
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
    public async Task <IActionResult> Remove(int? id)
    {
        _repo.RemovePost((int)id);
        await _repo.SaveChangesAsync();
        return RedirectToAction("Index");

    }


}
