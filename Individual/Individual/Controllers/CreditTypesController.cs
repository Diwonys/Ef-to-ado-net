using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Individual.Models;
using Individual.Models.Entities;

namespace Individual.Controllers
{
    public class CreditTypesController : Controller
    {
        private readonly ApplicationContext _context;

        public CreditTypesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: CreditTypes
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.CreditTypes.Include(c => c.Bank);
            return View(await applicationContext.ToListAsync());
        }

        // GET: CreditTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditType = await _context.CreditTypes
                .Include(c => c.Bank)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditType == null)
            {
                return NotFound();
            }

            return View(creditType);
        }

        // GET: CreditTypes/Create
        public IActionResult Create()
        {
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name");
            return View();
        }

        // POST: CreditTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BankId")] CreditType creditType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creditType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", creditType.BankId);
            return View(creditType);
        }

        // GET: CreditTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditType = await _context.CreditTypes.FindAsync(id);
            if (creditType == null)
            {
                return NotFound();
            }
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", creditType.BankId);
            return View(creditType);
        }

        // POST: CreditTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BankId")] CreditType creditType)
        {
            if (id != creditType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditTypeExists(creditType.Id))
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
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", creditType.BankId);
            return View(creditType);
        }

        // GET: CreditTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditType = await _context.CreditTypes
                .Include(c => c.Bank)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditType == null)
            {
                return NotFound();
            }

            return View(creditType);
        }

        // POST: CreditTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creditType = await _context.CreditTypes.FindAsync(id);
            _context.CreditTypes.Remove<CreditType>(creditType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditTypeExists(int id)
        {
            return _context.CreditTypes.Any(e => e.Id == id);
        }
    }
}
