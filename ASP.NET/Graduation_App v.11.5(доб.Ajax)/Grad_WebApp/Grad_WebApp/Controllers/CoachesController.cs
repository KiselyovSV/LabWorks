using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grad_WebApp.Data;
using Grad_WebApp.Models;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Grad_WebApp.Controllers
{
    [OutputCache(PolicyName = "Expire5")]
    //[Authorize(Roles = "admin")]
    public class CoachesController : Controller
    {
        private readonly FitnessDbContext _context;

        private readonly IWebHostEnvironment appEnvironment;

        private IOutputCacheStore cache;
        public CoachesController(FitnessDbContext context, IWebHostEnvironment appEnvironment, IOutputCacheStore cache)
        {
            _context = context;
            this.cache = cache; 
            this.appEnvironment = appEnvironment;
        }

        // GET: Coaches
        public async Task<IActionResult> Index()
        {
            ViewData["Photo"] = await _context.CoachPhotos.ToListAsync();

            return View(await _context.Сoachs.OrderBy(c => c.Id).ToListAsync());
        }

        // GET: Coaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Сoachs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,Patronymic,LastName,Email,Phone,Information,Сomment")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coach);
                await _context.SaveChangesAsync();
                await cache.EvictByTagAsync("ttbls5", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls5"
                return RedirectToAction(nameof(Index));
            }
            return View(coach);
        }

        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Сoachs.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Patronymic,LastName,Email,Phone,Information,Сomment")] Coach coach)
        {
            if (id != coach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coach);
                    await cache.EvictByTagAsync("ttbls5", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls5"
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coach.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(coach);
        }

        // GET: Coaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Сoachs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coach = await _context.Сoachs.FindAsync(id);
            if (coach != null)
            {
                _context.Сoachs.Remove(coach);
            }

            await _context.SaveChangesAsync();
            await cache.EvictByTagAsync("ttbls5", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls5"
            return RedirectToAction(nameof(Index));
        }

        private bool CoachExists(int id)
        {
            return _context.Сoachs.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto(int Id, IFormFile uploadedFile)
        {
            CoachPhoto? coachPhoto = await _context.CoachPhotos.FirstOrDefaultAsync(cp=>cp.CoachId==Id);
            if (uploadedFile != null && coachPhoto is null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                CoachPhoto file = new CoachPhoto { Name = uploadedFile.FileName, Path = path, CoachId = Id };
                await _context.CoachPhotos.AddAsync(file);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index","Coaches");
        }

        public async Task<IActionResult> DeletePhoto(int Id)
        {
            CoachPhoto? coachPhoto = await _context.CoachPhotos.FirstOrDefaultAsync(cp => cp.CoachId == Id);
            if (coachPhoto is not null)
            {
                FileInfo fileInf = new FileInfo(appEnvironment.WebRootPath + coachPhoto.Path!);
                if (fileInf.Exists) fileInf.Delete();
                _context.CoachPhotos.Remove(coachPhoto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Coaches");
        }
    }
}
