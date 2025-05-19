using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grad_WebApp.Data;
using Grad_WebApp.Models;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.OutputCaching;

namespace Grad_WebApp.Controllers
{
    [OutputCache(PolicyName = "Expire4")]
    public class Client_SubscriptionController : Controller
    {
        /* в связи с тем, что объект класса Client_SubscriptionController при каждом обращении к контексту создается вновь, то
         необходим сервис для хранения Id клиента (создается один объект класса Id на всё время работы приложения) */

        readonly Id idService;

        private readonly FitnessDbContext _context;

        private IOutputCacheStore cache;
        public Client_SubscriptionController(FitnessDbContext context, Id idService, IOutputCacheStore cache)
        {
            _context = context;
            this.idService = idService;
            this.cache = cache;
        }

        // GET метод выводит список всех абонементов у выбранного клиента
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            idService.Client_Id = id;  // инициализация свойства Client_Id сервиса Id.idService
            var client_Subscription = await (from clsub in _context.Client_Subscriptions.
            Include(c => c.Client).Include(c => c.Subscription)
            where clsub.Client!.Id == id
            select clsub).ToListAsync();
            if (client_Subscription == null)
            {
                return NotFound();
            }
            var res = DateTime.Compare(idService.LastOverTime, DateTime.Today);
            if (res < 0) await SubscriptionsAreOver(_context);
            return View(client_Subscription);
        }

        // обнуляем количество тренировок в абонементах, срок действия которых истёк
        private async Task SubscriptionsAreOver(FitnessDbContext _ctxt)
        {
            var end_cl_subs = await _ctxt.Client_Subscriptions.Where(b=>b.EndDate== DateTime.UtcNow || b.EndDate < DateTime.UtcNow).ToListAsync();
            if (end_cl_subs.Any())
            {
                foreach (var end_cl_sub in end_cl_subs)
                {
                    var result = DateTime.Compare(end_cl_sub.EndDate, DateTime.UtcNow);
                    if (result == 0 || result < 0)
                    {
                        end_cl_sub.Trainings = 0;
                        try
                        {
                            _ctxt.Update(end_cl_sub);
                            await _ctxt.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            throw;
                        }
                    }
                }
            }
            idService.LastOverTime = DateTime.Today;
        }

        // GET: Client_Subscription/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client_Subscription = await _context.Client_Subscriptions.
            Include(c => c.Client).Include(c => c.Subscription).Where(c=>c.Id==id).ToListAsync();
            if (client_Subscription == null)
            {
                return NotFound();
            }
            return View(client_Subscription);
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
                item.Add(new SelectListItem() { Text = st, Value = c.Id.ToString()});
            }
            return item;
        }

        // GET: Client_Subscription/Create
        public async Task<IActionResult> Create()
        {
            var client = await GetClientName(idService.Client_Id); // List<SelectListItem> с данными о клиенте
            var subs = await _context.Subscriptions.Where(s => s.IsActive == true).ToListAsync();
            ViewData["ClientId"] = new SelectList(client, "Value", "Text"); // добавляем данные в ViewData для вывода в представлении
            ViewData["SubscriptionId"] = new SelectList(subs, "Id", "Name");
            return View();
        }

        // POST: Client_Subscription/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,SubscriptionId,StartDate,EndDate,CostIncludingDiscount,Trainings")] Client_Subscription client_Subscription)
        {
            if (ModelState.IsValid)
            {
                int? _id = client_Subscription.SubscriptionId;
                DateTime startDate = client_Subscription.StartDate;
                client_Subscription.CostIncludingDiscount = await GetCostWithDiscount(_id, idService.Client_Id);
                client_Subscription.EndDate = await GetEndDate(_id, startDate);  // инициализируем свойство EndDate, присваивая (StartDate + Period) 
                client_Subscription.Trainings = await GetAmountOfTrainings(_id); // инициализируем свойство Trainings (кол-во оставшихся тренировок) в записи об абонементе
                _context.Add(client_Subscription);
                await _context.SaveChangesAsync();
                await cache.EvictByTagAsync("ttbls4", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls4"
                return RedirectToAction(nameof(Index), new { id = idService.Client_Id });
            }
            var client = await GetClientName(idService.Client_Id);
            var subs = await _context.Subscriptions.Where(s=>s.IsActive==true).ToListAsync();
            ViewData["ClientId"] = new SelectList(client, "Value", "Text", client_Subscription.ClientId);  
            ViewData["SubscriptionId"] = new SelectList(subs, "Id", "Name", client_Subscription.SubscriptionId);
            return View(client_Subscription);
        }

        // получаем стоимость со скидкой по Id абонемента и Id клиента
        public async Task<double?> GetCostWithDiscount(int? sub_id, int? cl_id)
        {
            if (sub_id == null || cl_id == null)
            {
                return 0.0;
            }
            var sub = await _context.Subscriptions.FindAsync(sub_id);
            var cl = await _context.Clients.FindAsync(cl_id);
            double? subDiscountPrice = sub!.Cost - sub!.Cost * cl!.Discount/100; 
            return subDiscountPrice;
        }

        public async Task<int?>GetAmountOfTrainings(int? id)
        {
            int? count = 0;
            if (id == null)
            {
                return count;
            }
            var sub = await _context.Subscriptions.FindAsync(id);
            count = sub!.AmountOfTrainings;
            return count;
        }

        public async Task<DateTime> GetEndDate(int? id, DateTime startDate)
        {
            DateTime endDate;
            if (id == null)
            {
                return DateTime.UtcNow;
            }
            var sub = await _context.Subscriptions.FindAsync(id);
            double period = (double)(sub!.Period ?? 0.0);
            endDate = startDate.AddDays(period);
            return endDate;
        }

        // GET: Client_Subscription/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client_Subscription = await _context.Client_Subscriptions.FindAsync(id);
            idService.ClSub = client_Subscription;  // инициализация свойства Client_Subscription ClSub сервиса Id.idService
            if (client_Subscription == null)
            {
                return NotFound();
            }
            var client = await GetClientName(idService.Client_Id);
            var sub_id = client_Subscription.SubscriptionId;
            var sub = await GetSubscriptionName(sub_id);
            ViewData["ClientId"] = new SelectList(client, "Value", "Text");
            ViewData["SubscriptionId"] = new SelectList(sub, "Value", "Text");
            return View(client_Subscription);
        }

        // POST: Client_Subscription/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,SubscriptionId,Trainings")] Client_Subscription client_Subscription)
        {
            if (id != client_Subscription.Id)
            {
                return NotFound();
            }
            // инициализация свойств StartDate, EndDate и CostIncludingDiscount данными, полученными из свойства Client_Subscription ClSub сервиса Id.idService
            TimeZoneInfo local = TimeZoneInfo.Local;                                                
            TimeSpan offset = local.GetUtcOffset(idService.ClSub!.StartDate);
            client_Subscription.StartDate = idService.ClSub!.StartDate.AddHours(-offset.Hours);
            client_Subscription.EndDate = idService.ClSub!.EndDate.AddHours(-offset.Hours);
            client_Subscription.CostIncludingDiscount = idService.ClSub!.CostIncludingDiscount;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client_Subscription);
                    await _context.SaveChangesAsync();
                    await cache.EvictByTagAsync("ttbls4", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls4"
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Client_SubscriptionExists(client_Subscription.Id))
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
            var sub_id = client_Subscription.SubscriptionId;
            var sub = await GetSubscriptionName(sub_id);
            ViewData["ClientId"] = new SelectList(client, "Value", "Text", client_Subscription.ClientId);
            ViewData["SubscriptionId"] = new SelectList(sub, "Value", "Text", client_Subscription.SubscriptionId);
            return View(client_Subscription);
        }

        // перенос данных об абонементе в List<SelectListItem> по Id абонемента
        private async Task<List<SelectListItem>> GetSubscriptionName(int? sub_id)
        {
            var sub = await _context.Subscriptions.FindAsync(sub_id);
            var item = new List<SelectListItem>();
            string st = sub!.Name!.ToString();
            item.Add(new SelectListItem() { Text = st, Value = sub.Id.ToString() });
            return item;
        }

        // GET: Client_Subscription/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client_Subscription = await _context.Client_Subscriptions
                .Include(c => c.Client)
                .Include(c => c.Subscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client_Subscription == null)
            {
                return NotFound();
            }

            return View(client_Subscription);
        }

        // POST: Client_Subscription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client_Subscription = await _context.Client_Subscriptions.FindAsync(id);
            if (client_Subscription != null)
            {
                _context.Client_Subscriptions.Remove(client_Subscription);
            }

            await _context.SaveChangesAsync();
            await cache.EvictByTagAsync("ttbls4", new CancellationToken()); // удаляем кэшированный объект с меткой "ttbls4"
            return RedirectToAction(nameof(Index), new { id = idService.Client_Id });
        }

        private bool Client_SubscriptionExists(int id)
        {
            return _context.Client_Subscriptions.Any(e => e.Id == id);
        }
    }
}
