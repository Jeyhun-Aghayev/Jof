using Jof.DAL;
using Jof.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Jof.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<Fruit> list = await _db.Fruits?.Where(f => !f.IsDeleted)?.ToListAsync();
            return View(list);
        }
    }
}