using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParfumEcommerce.Data;
using ParfumEcommerce.Models;

namespace ParfumEcommerce.Controllers
{
    public class ParfumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParfumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parfums
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Parfums.Include(p => p.Categorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Parfums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parfums == null)
            {
                return NotFound();
            }

            var parfum = await _context.Parfums
                .Include(p => p.Categorie)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parfum == null)
            {
                return NotFound();
            }

            return View(parfum);
        }

        // GET: Parfums/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "ID", "ID");
            return View();
        }

        // POST: Parfums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ParfumName,Prix,Image,CategorieId")] Parfum parfum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parfum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "ID", "ID", parfum.CategorieId);
            return View(parfum);
        }

        // GET: Parfums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parfums == null)
            {
                return NotFound();
            }

            var parfum = await _context.Parfums.FindAsync(id);
            if (parfum == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "ID", "ID", parfum.CategorieId);
            return View(parfum);
        }

        // POST: Parfums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ParfumName,Prix,Image,CategorieId")] Parfum parfum)
        {
            if (id != parfum.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parfum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParfumExists(parfum.ID))
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
            ViewData["CategorieId"] = new SelectList(_context.Categories, "ID", "ID", parfum.CategorieId);
            return View(parfum);
        }

        // GET: Parfums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parfums == null)
            {
                return NotFound();
            }

            var parfum = await _context.Parfums
                .Include(p => p.Categorie)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parfum == null)
            {
                return NotFound();
            }

            return View(parfum);
        }

        // POST: Parfums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parfums == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Parfums'  is null.");
            }
            var parfum = await _context.Parfums.FindAsync(id);
            if (parfum != null)
            {
                _context.Parfums.Remove(parfum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParfumExists(int id)
        {
          return (_context.Parfums?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
