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
    public class GuarantorsController : Controller
    {
        private readonly ApplicationContext _context;

        public GuarantorsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Guarantors
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Guarantors.Include(g => g.Borrower);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Guarantors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guarantor = await _context.Guarantors
                .Include(g => g.Borrower)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guarantor == null)
            {
                return NotFound();
            }

            return View(guarantor);
        }

        // GET: Guarantors/Create
        public IActionResult Create()
        {
            ViewData["BorrowerId"] = new SelectList(_context.Borrowers, "Id", "Surname");
            return View();
        }

        // POST: Guarantors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,Patronymic,PasportNumber,Address,PhoneNumber,Salary,BorrowerId")] Guarantor guarantor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guarantor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BorrowerId"] = new SelectList(_context.Borrowers, "Id", "Surname", guarantor.BorrowerId);
            return View(guarantor);
        }

        // GET: Guarantors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guarantor = await _context.Guarantors.FindAsync(id);
            if (guarantor == null)
            {
                return NotFound();
            }
            ViewData["BorrowerId"] = new SelectList(_context.Borrowers, "Id", "Surname", guarantor.BorrowerId);
            return View(guarantor);
        }

        // POST: Guarantors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,Patronymic,PasportNumber,Address,PhoneNumber,Salary,BorrowerId")] Guarantor guarantor)
        {
            if (id != guarantor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guarantor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuarantorExists(guarantor.Id))
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
            ViewData["BorrowerId"] = new SelectList(_context.Borrowers, "Id", "Surname", guarantor.BorrowerId);
            return View(guarantor);
        }

        // GET: Guarantors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guarantor = await _context.Guarantors
                .Include(g => g.Borrower)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guarantor == null)
            {
                return NotFound();
            }

            return View(guarantor);
        }

        // POST: Guarantors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guarantor = await _context.Guarantors.FindAsync(id);
            _context.Guarantors.Remove<Guarantor>(guarantor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuarantorExists(int id)
        {
            return _context.Guarantors.Any(e => e.Id == id);
        }
    }
}
