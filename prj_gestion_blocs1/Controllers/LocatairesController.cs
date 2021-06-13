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
    public class LocatairesController : Controller
    {
        private readonly bd_gestion_blocsContext _context;

        public LocatairesController(bd_gestion_blocsContext context)
        {
            _context = context;
        }

        // GET: Locataires
        public async Task<IActionResult> Index()
        {
            return View(await _context.Locataires.ToListAsync());
        }

        // GET: Locataires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locataire = await _context.Locataires
                .FirstOrDefaultAsync(m => m.IdLocataire == id);
            if (locataire == null)
            {
                return NotFound();
            }

            return View(locataire);
        }

        // GET: Locataires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locataires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLocataire,NomLocataire,PrenomLocataire,EmailLocataire,DateDebutBail,EtatCompteLocataire")] Locataire locataire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locataire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locataire);
        }

        // GET: Locataires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locataire = await _context.Locataires.FindAsync(id);
            if (locataire == null)
            {
                return NotFound();
            }
            return View(locataire);
        }

        // POST: Locataires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLocataire,NomLocataire,PrenomLocataire,EmailLocataire,DateDebutBail,EtatCompteLocataire")] Locataire locataire)
        {
            if (id != locataire.IdLocataire)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locataire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocataireExists(locataire.IdLocataire))
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
            return View(locataire);
        }

        // GET: Locataires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locataire = await _context.Locataires
                .FirstOrDefaultAsync(m => m.IdLocataire == id);
            if (locataire == null)
            {
                return NotFound();
            }

            return View(locataire);
        }

        // POST: Locataires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locataire = await _context.Locataires.FindAsync(id);
            _context.Locataires.Remove(locataire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocataireExists(int id)
        {
            return _context.Locataires.Any(e => e.IdLocataire == id);
        }
    }
}
