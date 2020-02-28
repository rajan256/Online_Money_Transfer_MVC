using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Money_Transfer_MVC.Data;
using Online_Money_Transfer_MVC.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Money_Transfer_MVC.Controllers
{
    [Authorize]
    public class MoneyReceiversController : Controller
    {
        private readonly Online_Money_Transfer_DbContext _context;

        public MoneyReceiversController(Online_Money_Transfer_DbContext context)
        {
            _context = context;
        }

        // GET: MoneyReceivers
        public async Task<IActionResult> Index()
        {
            return View(await _context.MoneyReceiver.ToListAsync());
        }

        // GET: MoneyReceivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyReceiver = await _context.MoneyReceiver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moneyReceiver == null)
            {
                return NotFound();
            }

            return View(moneyReceiver);
        }

        // GET: MoneyReceivers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoneyReceivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReceiverName,MobileNumber")] MoneyReceiver moneyReceiver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moneyReceiver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moneyReceiver);
        }

        // GET: MoneyReceivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyReceiver = await _context.MoneyReceiver.FindAsync(id);
            if (moneyReceiver == null)
            {
                return NotFound();
            }
            return View(moneyReceiver);
        }

        // POST: MoneyReceivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReceiverName,MobileNumber")] MoneyReceiver moneyReceiver)
        {
            if (id != moneyReceiver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moneyReceiver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoneyReceiverExists(moneyReceiver.Id))
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
            return View(moneyReceiver);
        }

        // GET: MoneyReceivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyReceiver = await _context.MoneyReceiver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moneyReceiver == null)
            {
                return NotFound();
            }

            return View(moneyReceiver);
        }

        // POST: MoneyReceivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moneyReceiver = await _context.MoneyReceiver.FindAsync(id);
            _context.MoneyReceiver.Remove(moneyReceiver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoneyReceiverExists(int id)
        {
            return _context.MoneyReceiver.Any(e => e.Id == id);
        }
    }
}
