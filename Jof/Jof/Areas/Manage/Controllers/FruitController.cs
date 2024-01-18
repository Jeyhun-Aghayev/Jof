using Jof.Areas.Manage.Models;
using Jof.DAL;
using Jof.Helper;
using Jof.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jof.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class FruitController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public FruitController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Fruit> fruits = await _db.Fruits.Where(f => !f.IsDeleted).ToListAsync();
            return View(fruits);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateFruitVm vm)
        {
            if(!ModelState.IsValid) return View(vm);
            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("Image", "Yanliz sekil daxil ede bilersiz");
                return View(vm);
            }
            if(!vm.Image.CheckLength(3000))
            {
                ModelState.AddModelError("Image", "Maxsimum 3mb sekil ywkleye bilersiniz");
                return View(vm);
            }
            Fruit fruit = new Fruit()
            {
                Title= vm.Title,
                SubTitle= vm.SubTitle,
                ImgUrl = vm.Image.Upload(_env.WebRootPath,@"\Upload\Images\"),
                CreatedAt = DateTime.UtcNow,
            };
            await _db.Fruits.AddAsync(fruit);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Fruit fruit = await _db.Fruits.Where(f => !f.IsDeleted && f.Id == id).FirstOrDefaultAsync();
            if (fruit is null) return BadRequest();
            UpdateFruitVm vm = new UpdateFruitVm()
            {
                Id = id,
                Title = fruit.Title,
                SubTitle = fruit.SubTitle,
                ImgUrl = fruit.ImgUrl
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateFruitVm vm)
        {
            Fruit fruit = await _db.Fruits.Where(f => !f.IsDeleted && f.Id == vm.Id).FirstOrDefaultAsync();
            if (fruit is null) return BadRequest();
            if (!ModelState.IsValid) { return View(vm); }
            if(vm.Image is not null)
            {
                if (!vm.Image.CheckType("image"))
                {
                    ModelState.AddModelError("Image", "Yanliz sekil daxil ede bilersiz");
                    return View(vm);
                }
                if (!vm.Image.CheckLength(3000))
                {
                    ModelState.AddModelError("Image", "Maxsimum 3mb sekil ywkleye bilersiniz");
                    return View(vm);
                }
                fruit.ImgUrl = vm.Image.Upload(_env.WebRootPath, @"\Upload\Images\");
            }
            fruit.Title = vm.Title;
            fruit.SubTitle = vm.SubTitle;
            fruit.UpdatedAt = DateTime.UtcNow;
            _db.Update(fruit);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Fruit fruit = await _db.Fruits.Where(f => !f.IsDeleted && f.Id==id).FirstOrDefaultAsync();
            if (fruit is not null) fruit.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
