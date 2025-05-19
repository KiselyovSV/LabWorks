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
using Microsoft.AspNetCore.Identity;

namespace Grad_WebApp.Controllers
{
    [OutputCache(PolicyName = "Expire2")]
    public class ClientsController : Controller
    {
        internal  FitnessDbContext _context;

        private readonly IOutputCacheStore cache;

        private readonly UserManager<User> _userManager;
        public ClientsController(FitnessDbContext context, IOutputCacheStore cache, UserManager<User> userManager)
        {              
            _context = context;
            this.cache = cache;
            _userManager = userManager;
        }

        // GET: Clients
        public async Task<ViewResult> Index()
        {
            return View(await _context.Clients.OrderBy(c => c.Id).ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,Patronymic,LastName,Email,Phone,DateOfBirth,Сomment,Discount")] Client client)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(client.Email!);
                if (user is not null) client.UserId = user.Id;
                _context.Add(client);
                await _context.SaveChangesAsync();
                await cache.EvictByTagAsync("ttbls2", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls"
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Patronymic,LastName,Email,Phone,DateOfBirth,Сomment,Discount")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await cache.EvictByTagAsync("ttbls2", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls"
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
            await _context.SaveChangesAsync();
            await cache.EvictByTagAsync("ttbls2", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls"
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
