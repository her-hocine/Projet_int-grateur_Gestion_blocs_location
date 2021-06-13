using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetIntegrateur.Models;

namespace projetIntegrateur.Controllers
{
    public class PartenairesController : Controller
    {
        private readonly bd_gestion_blocsContext _context;

        public PartenairesController(bd_gestion_blocsContext context)
        {
            _context = context;
        }

        // GET: Partenaires
        public async Task<IActionResult> Index()
        {
            return View(await _context.Partenaires.ToListAsync());
        }

        // GET: Partenaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partenaire = await _context.Partenaires
                .FirstOrDefaultAsync(m => m.IdPartenaire == id);
            if (partenaire == null)
            {
                return NotFound();
            }

            return View(partenaire);
        }

        // GET: Partenaires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partenaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPartenaire,NomPartenaire,PrenomPartenaire,FonctionPartenaire,EmailPartenaire,TelPartenaire")] Partenaire partenaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partenaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(partenaire);
        }

        // GET: Partenaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partenaire = await _context.Partenaires.FindAsync(id);
            if (partenaire == null)
            {
                return NotFound();
            }
            return View(partenaire);
        }

        // POST: Partenaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPartenaire,NomPartenaire,PrenomPartenaire,FonctionPartenaire,EmailPartenaire,TelPartenaire")] Partenaire partenaire)
        {
            if (id != partenaire.IdPartenaire)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partenaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartenaireExists(partenaire.IdPartenaire))
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
            return View(partenaire);
        }

        // GET: Partenaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partenaire = await _context.Partenaires
                .FirstOrDefaultAsync(m => m.IdPartenaire == id);
            if (partenaire == null)
            {
                return NotFound();
            }

            return View(partenaire);
        }

        // POST: Partenaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partenaire = await _context.Partenaires.FindAsync(id);
            _context.Partenaires.Remove(partenaire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartenaireExists(int id)
        {
            return _context.Partenaires.Any(e => e.IdPartenaire == id);
        }
    }
}
