using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Post post)
    {
        if (ModelState.IsValid)
        {
            _repo.AddPost(post);
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Panel");
            }
        }
    
        return View(post);
    }

}
