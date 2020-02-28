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
    public class MoneySendersController : Controller
    {
        private readonly Online_Money_Transfer_DbContext _context;

        public MoneySendersController(Online_Money_Transfer_DbContext context)
        {
            _context = context;
        }

        // GET: MoneySenders
        public async Task<IActionResult> Index()
        {
            return View(await _context.MoneySender.ToListAsync());
        }

        // GET: MoneySenders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneySender = await _context.MoneySender
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moneySender == null)
            {
                return NotFound();
            }

            return View(moneySender);
        }

        // GET: MoneySenders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoneySenders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SenderName,MobileNumber")] MoneySender moneySender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moneySender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moneySender);
        }

        // GET: MoneySenders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneySender = await _context.MoneySender.FindAsync(id);
            if (moneySender == null)
            {
                return NotFound();
            }
            return View(moneySender);
        }

        // POST: MoneySenders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SenderName,MobileNumber")] MoneySender moneySender)
        {
            if (id != moneySender.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moneySender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoneySenderExists(moneySender.Id))
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
            return View(moneySender);
        }

        // GET: MoneySenders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneySender = await _context.MoneySender
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moneySender == null)
            {
                return NotFound();
            }

            return View(moneySender);
        }

        // POST: MoneySenders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moneySender = await _context.MoneySender.FindAsync(id);
            _context.MoneySender.Remove(moneySender);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoneySenderExists(int id)
        {
            return _context.MoneySender.Any(e => e.Id == id);
        }
    }
}
