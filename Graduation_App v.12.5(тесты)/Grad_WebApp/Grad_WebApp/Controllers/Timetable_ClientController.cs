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

namespace Grad_WebApp.Controllers
{
    [OutputCache(PolicyName = "Expire7")]
    public class Timetable_ClientController : Controller
    {
        readonly Id idService;

        private readonly FitnessDbContext _context;

        private IOutputCacheStore cache;
        public Timetable_ClientController(FitnessDbContext context, Id idService, IOutputCacheStore cache)
        {
            _context = context;
            this.idService = idService;
            this.cache = cache;
        }

        // GET метод выводит список всех тренировок у выбранного клиента
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return NotFound();
            idService.Client_Id = id;  // инициализация свойства Client_Id сервиса Id.idService
            var fitnessDbContext = _context.Timetable_Clients.Include(t => t.Сlient).Include(t => t.Timetable).Include(t=>t.Timetable!.Workout).Where(t=>t.Сlient!.Id==id).OrderBy(t=>t.Timetable!.Date);
            if (fitnessDbContext == null)
            {
                return NotFound();
            }
            return View(await fitnessDbContext.ToListAsync());
        }

        // GET: Timetable_Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timetable_Client = await _context.Timetable_Clients
                .Include(t => t.Сlient)
                .Include(t => t.Timetable)
                .Include(t => t.Timetable!.Workout)
                .Include(t => t.Timetable!.Coach)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timetable_Client == null)
            {
                return NotFound();
            }

            return View(timetable_Client);
        }

        // выборка данных о клиенте в List<Client>
        private async Task<List<Client>> GetClient(int? id)
        {
            var client = await (from c in _context.Clients
                                where c.Id == id
                                select c).ToListAsync();
            return client;
        }

        // перенос данных о клиенте из List<Client> в List<SelectListItem> по Id клиента
        private async Task<List<SelectListItem>> GetClientName(int? id)
        {
            var client = await GetClient(id);
            var item = new List<SelectListItem>();
            foreach (var c in client)
            {
                string st = $"{c.LastName} {c.FirstName} {c.Patronymic}";
                item.Add(new SelectListItem() { Text = st, Value = c.Id.ToString() });
            }
            return item;
        }
        // получаем список тренировок в расписании
        private async Task<List<SelectListItem>> GetTimetables()
        {
            var ttbls = await _context.Timetables.Include(t => t.Workout).OrderBy(t=>t.Date).ToListAsync();
            List<SelectListItem> items = new();
            foreach (var ttbl in ttbls)
            {
                string st = $"{ttbl.Date} - (id:{ttbl.Workout!.Id}) {ttbl.Workout!.Type}";
                items.Add(new SelectListItem() { Text = st, Value = ttbl.Id.ToString() });
            }
            return items;
        }

        // получаем список тренировок одного типа в расписании
        private async Task<List<SelectListItem>> GetTimetables(string? workout_type)
        {
            var ttbls = await _context.Timetables.Include(t => t.Workout).Where(t=>t.Workout!.Type == workout_type).OrderBy(t => t.Date).ToListAsync();
            List<SelectListItem> items = new();
            foreach (var ttbl in ttbls)
            {
                string st = $"{ttbl.Date} - (id:{ttbl.Workout!.Id}) {ttbl.Workout!.Type}";
                items.Add(new SelectListItem() { Text = st, Value = ttbl.Id.ToString() });
            }
            return items;
        }

        // GET: Timetable_Client/Create
        public async Task<IActionResult> Create()
        {
            var client = await GetClientName(idService.Client_Id); // List<SelectListItem> с данными о клиенте
            var ttbls = await GetTimetables();  // получаем список тренировок в расписании
            ViewData["СlientId"] = new SelectList(client, "Value", "Text");
            ViewData["TimetableId"] = new SelectList(ttbls, "Value", "Text");
            return View();
        }

        // POST: Timetable_Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TimetableId,СlientId")] Timetable_Client timetable_Client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timetable_Client);
                await _context.SaveChangesAsync();
                await cache.EvictByTagAsync("ttbls7", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls7"
                return RedirectToAction(nameof(Index), new { id = idService.Client_Id });
            }
            var client = await GetClientName(idService.Client_Id); // List<SelectListItem> с данными о клиенте
            var ttbls = await GetTimetables();  // получаем список тренировок в расписании
            ViewData["СlientId"] = new SelectList(client, "Value", "Text", timetable_Client.СlientId);
            ViewData["TimetableId"] = new SelectList(ttbls, "Value", "Text", timetable_Client.TimetableId);
            return View(timetable_Client);
        }

        // метод для записи на тренировку самим клиентом (зарегистрированным в системе) со стартовой страницы
        [HttpGet]
        public async Task<IActionResult> CreateFromStartPage(int? timetableId, string userId)
        {
            if (timetableId is not null && userId is not null)
            {
                Client? client = await _context.Clients.FirstOrDefaultAsync(c=>c.UserId==userId);
                Timetable_Client timetable_Client = new Timetable_Client() { TimetableId = Convert.ToInt32(timetableId), СlientId = client!.Id };
                _context.Add(timetable_Client);
                await _context.SaveChangesAsync();
                await cache.EvictByTagAsync("ttbls7", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls7"
                return RedirectToAction("Index", "Timetables");
            }
            return RedirectToAction("Index", "Timetables");
        }

        // извлечение информации о записях авторизованного пользователя на тренировки 
        public async Task<IActionResult> ShowUserTimetables()
        { 
            if (idService.UserId is null) return NotFound();
            else
            {
                Client? client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == idService.UserId);
                var timetable_Clients = _context.Timetable_Clients.Include(t => t.Сlient).Include(t => t.Timetable).Include(t => t.Timetable!.Workout).Where(t => t.Сlient!.Id == client!.Id).OrderBy(t => t.Timetable!.Date);
                if (timetable_Clients is null)
                {
                    return NotFound();
                }
                var t = await timetable_Clients.ToListAsync();
                return PartialView(t);
            }
        }

        // GET: Timetable_Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var timetable_Client = await _context.Timetable_Clients.Include(tc => tc.Timetable).Include(tc=>tc.Timetable!.Workout).FirstOrDefaultAsync(tc=>tc.Id==id);
            idService.TtblClient = timetable_Client;  // инициализация свойства Timetable_Client? TtblClient сервиса Id.idService
            if (timetable_Client == null)
            {
                return NotFound();
            }
            var client = await GetClientName(idService.Client_Id);
            var ttbls = await GetTimetables(idService.TtblClient!.Timetable!.Workout!.Type);
            ViewData["СlientId"] = new SelectList(client, "Value", "Text", timetable_Client.СlientId);
            ViewData["TimetableId"] = new SelectList(ttbls, "Value", "Text", timetable_Client.TimetableId);
            return View(timetable_Client);
        }

        // POST: Timetable_Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TimetableId,СlientId")] Timetable_Client timetable_Client)
        {
            if (id != timetable_Client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timetable_Client);
                    await _context.SaveChangesAsync();
                    await cache.EvictByTagAsync("ttbls7", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls7"
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Timetable_ClientExists(timetable_Client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = idService.Client_Id });
            }
            var client = await GetClientName(idService.Client_Id);
            var ttbls = await GetTimetables(idService.TtblClient!.Timetable!.Workout!.Type);
            ViewData["СlientId"] = new SelectList(client, "Value", "Text", timetable_Client.СlientId);
            ViewData["TimetableId"] = new SelectList(ttbls, "Value", "Text", timetable_Client.TimetableId);
            return View(timetable_Client);
        }

        // GET: Timetable_Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timetable_Client = await _context.Timetable_Clients
                .Include(t => t.Сlient)
                .Include(t => t.Timetable)
                .Include(t => t.Timetable!.Workout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timetable_Client == null)
            {
                return NotFound();
            }

            return View(timetable_Client);
        }

        // POST: Timetable_Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timetable_Client = await _context.Timetable_Clients.FindAsync(id);
            if (timetable_Client != null)
            {
                _context.Timetable_Clients.Remove(timetable_Client);
            }
            await _context.SaveChangesAsync();
            await cache.EvictByTagAsync("ttbls7", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls7"
            return RedirectToAction(nameof(Index), new { id = idService.Client_Id });
        }

        private bool Timetable_ClientExists(int id)
        {
            return _context.Timetable_Clients.Any(e => e.Id == id);
        }
    }
}
