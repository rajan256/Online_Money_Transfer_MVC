using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Money_Transfer_MVC.Data;
using Online_Money_Transfer_MVC.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Money_Transfer_MVC.Controllers
{
    [Authorize]
    public class MoneyTransfersController : Controller
    {
        private readonly Online_Money_Transfer_DbContext _context;

        public MoneyTransfersController(Online_Money_Transfer_DbContext context)
        {
            _context = context;
        }

        // GET: MoneyTransfers
        public async Task<IActionResult> Index()
        {
            var online_Money_Transfer_DbContext = _context.MoneyTransfer.Include(m => m.MoneyReceiver).Include(m => m.MoneySender).Include(m => m.Provider);
            return View(await online_Money_Transfer_DbContext.ToListAsync());
        }

        // GET: MoneyTransfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyTransfer = await _context.MoneyTransfer
                .Include(m => m.MoneyReceiver)
                .Include(m => m.MoneySender)
                .Include(m => m.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moneyTransfer == null)
            {
                return NotFound();
            }

            return View(moneyTransfer);
        }

        // GET: MoneyTransfers/Create
        public IActionResult Create()
        {
            ViewData["MoneyReceiverId"] = new SelectList(_context.MoneyReceiver, "Id", "Id");
            ViewData["MoneySenderId"] = new SelectList(_context.MoneySender, "Id", "Id");
            ViewData["ProviderId"] = new SelectList(_context.Set<Provider>(), "Id", "Id");
            return View();
        }

        // POST: MoneyTransfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransferAmount,TransferDateTime,MoneySenderId,MoneyReceiverId,ProviderId")] MoneyTransfer moneyTransfer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moneyTransfer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MoneyReceiverId"] = new SelectList(_context.MoneyReceiver, "Id", "Id", moneyTransfer.MoneyReceiverId);
            ViewData["MoneySenderId"] = new SelectList(_context.MoneySender, "Id", "Id", moneyTransfer.MoneySenderId);
            ViewData["ProviderId"] = new SelectList(_context.Set<Provider>(), "Id", "Id", moneyTransfer.ProviderId);
            return View(moneyTransfer);
        }

        // GET: MoneyTransfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyTransfer = await _context.MoneyTransfer.FindAsync(id);
            if (moneyTransfer == null)
            {
                return NotFound();
            }
            ViewData["MoneyReceiverId"] = new SelectList(_context.MoneyReceiver, "Id", "Id", moneyTransfer.MoneyReceiverId);
            ViewData["MoneySenderId"] = new SelectList(_context.MoneySender, "Id", "Id", moneyTransfer.MoneySenderId);
            ViewData["ProviderId"] = new SelectList(_context.Set<Provider>(), "Id", "Id", moneyTransfer.ProviderId);
            return View(moneyTransfer);
        }

        // POST: MoneyTransfers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransferAmount,TransferDateTime,MoneySenderId,MoneyReceiverId,ProviderId")] MoneyTransfer moneyTransfer)
        {
            if (id != moneyTransfer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moneyTransfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoneyTransferExists(moneyTransfer.Id))
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
            ViewData["MoneyReceiverId"] = new SelectList(_context.MoneyReceiver, "Id", "Id", moneyTransfer.MoneyReceiverId);
            ViewData["MoneySenderId"] = new SelectList(_context.MoneySender, "Id", "Id", moneyTransfer.MoneySenderId);
            ViewData["ProviderId"] = new SelectList(_context.Set<Provider>(), "Id", "Id", moneyTransfer.ProviderId);
            return View(moneyTransfer);
        }

        // GET: MoneyTransfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyTransfer = await _context.MoneyTransfer
                .Include(m => m.MoneyReceiver)
                .Include(m => m.MoneySender)
                .Include(m => m.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moneyTransfer == null)
            {
                return NotFound();
            }

            return View(moneyTransfer);
        }

        // POST: MoneyTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moneyTransfer = await _context.MoneyTransfer.FindAsync(id);
            _context.MoneyTransfer.Remove(moneyTransfer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoneyTransferExists(int id)
        {
            return _context.MoneyTransfer.Any(e => e.Id == id);
        }
    }
}
