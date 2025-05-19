using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grad_WebApp.Data;
using Grad_WebApp.Models;
using System.Xml;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Diagnostics;
using Formatting = Newtonsoft.Json.Formatting;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace Grad_WebApp.Controllers
{
    [OutputCache(PolicyName = "Expire3")]
    [Authorize]
    public class TimetablesController : Controller
    {
        private readonly FitnessDbContext _context;

        private IOutputCacheStore cache;

        private string? jsonResult;

        private string? txtResult;

        private readonly IWebHostEnvironment appEnvironment;
        public TimetablesController(FitnessDbContext context, IOutputCacheStore cache, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            this.cache = cache;
            this.appEnvironment = appEnvironment;
        }

        // GET: Timetables
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var fitnessDbContext = _context.Timetables.Include(t => t.Coach).Include(t => t.Workout).OrderBy(t=>t.Date);
            return View(await fitnessDbContext.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> TimetablesPrintTxt()
        {
            string fileName = "txtResult.txt";
            var timetables = await _context.Timetables.Include(t => t.Coach).Include(t => t.Workout).OrderBy(t => t.Date).ToListAsync();
            if (timetables.Count == 0)
            {
                ViewData["Mes"] = ("Записи отсутствуют");
                //return HttpNotFound();
            }
            txtResult = GreateTxt(timetables);
            SaveResult(txtResult, fileName);
            return GetFile(fileName);
        }

        [AllowAnonymous]
        public string GreateTxt(List<Timetable> list)
        {
            string text = "";
            foreach (var ttbls in list)
            {
                text += ttbls.ToString();
            }
            return text;
        }

        [AllowAnonymous]
        public IActionResult GetFile(string fileName)
        {
            // Путь к файлу
            string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appEnvironment.WebRootPath + $"/Files/{fileName}");
            // Тип файла - content-type
            string file_type = "application/octet-stream"; //   или так: string file_type = "text/txt" или "text/json"; "application/octet-stream" - это универсальный тип

            string file_name = fileName;            // Имя файла - необязательно
            return PhysicalFile(file_path, file_type, file_name);
        }

        public async Task<IActionResult> TimetablesPrintJson()
        {
            string fileName = "jsonResult.json";
            var timetables = await _context.Timetables.Include(t => t.Coach).Include(t => t.Workout).OrderBy(t => t.Date).ToListAsync();
            if (timetables.Count == 0)
            {
                ViewData["Mes"] = ("Записи отсутствуют");
                //return HttpNotFound();
            }
            jsonResult = JsonConvert.SerializeObject(timetables, Formatting.Indented);
            SaveResult(jsonResult, fileName);
            return GetFile(fileName);
        }

        [AllowAnonymous]
        public void SaveResult(string content, string fileName)
        {
            string file_path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\" + fileName);
            try
            {
                if (System.IO.File.Exists(file_path)) System.IO.File.Delete(file_path);
                using (var W = new StreamWriter(file_path))
                {
                    W.Write(content);
                    W.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
       
        // GET: Timetables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timetable = await _context.Timetables
                .Include(t => t.Coach)
                .Include(t => t.Workout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timetable == null)
            {
                return NotFound();
            }

            return View(timetable);
        }

        // GET: Timetables/Create
        public async Task<IActionResult> Create()
        {
            var items = GetCoachsNames();
            var active_workouts = await _context.Workouts.Where(s => s.IsActive == true).ToListAsync();
            ViewData["CoachId"] = new SelectList(items, "Value", "Text");
            ViewData["WorkoutId"] = new SelectList(active_workouts, "Id", "Type");
            return View();
        }

        // POST: Timetables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkoutId,CoachId,Date")] Timetable timetable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timetable);
                await _context.SaveChangesAsync();
                await cache.EvictByTagAsync("ttbls3", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls3"
                return RedirectToAction(nameof(Index));
            }
            var items = GetCoachsNames();
            var active_workouts = await _context.Workouts.Where(s => s.IsActive == true).ToListAsync();
            ViewData["CoachId"] = new SelectList(items, "Value", "Text");
            ViewData["WorkoutId"] = new SelectList(active_workouts, "Id", "Type", timetable.WorkoutId);
            return View(timetable);
        }

        // получаем List<SelectListItem> с Ф.И.О. тренеров
        public List<SelectListItem> GetCoachsNames()
        {
            var coaches = _context.Сoachs.ToList();
            var items = new List<SelectListItem>();
            foreach (var coach in coaches)
            {
                string st = $"{coach.LastName} {coach.FirstName} {coach.Patronymic}";
                items.Add(new SelectListItem() { Text = st, Value = coach.Id.ToString() });
            }
            return items;
        }
        // GET: Timetables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timetable = await _context.Timetables.FindAsync(id);
            if (timetable == null)
            {
                return NotFound();
            }
            var items = GetCoachsNames();
            var active_workouts = await _context.Workouts.Where(s => s.IsActive == true).ToListAsync();
            ViewData["CoachId"] = new SelectList(items, "Value", "Text");
            ViewData["WorkoutId"] = new SelectList(active_workouts, "Id", "Type", timetable.WorkoutId);
            return View(timetable);
        }

        // POST: Timetables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkoutId,CoachId,Date")] Timetable timetable)
        {
            if (id != timetable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timetable);
                    await cache.EvictByTagAsync("ttbls3", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls3"
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimetableExists(timetable.Id))
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
            var items = GetCoachsNames();
            var active_workouts = await _context.Workouts.Where(s => s.IsActive == true).ToListAsync();
            ViewData["CoachId"] = new SelectList(items, "Value", "Text");
            ViewData["WorkoutId"] = new SelectList(active_workouts, "Id", "Type", timetable.WorkoutId);
            return View(timetable);
        }

        // GET: Timetables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timetable = await _context.Timetables
                .Include(t => t.Coach)
                .Include(t => t.Workout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timetable == null)
            {
                return NotFound();
            }

            return View(timetable);
        }

        // POST: Timetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timetable = await _context.Timetables.FindAsync(id);
            if (timetable != null)
            {
                _context.Timetables.Remove(timetable);
            }
            await _context.SaveChangesAsync();
            await cache.EvictByTagAsync("ttbls3", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls3"
            return RedirectToAction(nameof(Index));
        }

        private bool TimetableExists(int id)
        {
            return _context.Timetables.Any(e => e.Id == id);
        }
    }
}
