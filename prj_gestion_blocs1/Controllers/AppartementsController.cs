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
    public class AppartementsController : Controller
    {
        private readonly bd_gestion_blocsContext _context;

        public AppartementsController(bd_gestion_blocsContext context)
        {
            _context = context;
        }

        // GET: Appartements
        public async Task<IActionResult> Index()
        {
            var bd_gestion_blocsContext = _context.Appartements.Include(a => a.IdBlocNavigation).Include(a => a.IdLocataireNavigation);
            return View(await bd_gestion_blocsContext.ToListAsync());
        }

        // GET: Appartements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appartement = await _context.Appartements
                .Include(a => a.IdBlocNavigation)
                .Include(a => a.IdLocataireNavigation)
                .FirstOrDefaultAsync(m => m.IdAppartement == id);
            if (appartement == null)
            {
                return NotFound();
            }

            return View(appartement);
        }

        // GET: Appartements/Create
        public IActionResult Create()
        {
            ViewData["IdBloc"] = new SelectList(_context.Blocs, "IdBloc", "AdresseBloc");
            ViewData["IdLocataire"] = new SelectList(_context.Locataires, "IdLocataire", "EmailLocataire");
            return View();
        }

        // POST: Appartements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAppartement,IdBloc,IdLocataire,SuperficieAppartement,NbBalcons,PrixLocation")] Appartement appartement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appartement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBloc"] = new SelectList(_context.Blocs, "IdBloc", "AdresseBloc", appartement.IdBloc);
            ViewData["IdLocataire"] = new SelectList(_context.Locataires, "IdLocataire", "EmailLocataire", appartement.IdLocataire);
            return View(appartement);
        }

        // GET: Appartements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appartement = await _context.Appartements.FindAsync(id);
            if (appartement == null)
            {
                return NotFound();
            }
            ViewData["IdBloc"] = new SelectList(_context.Blocs, "IdBloc", "AdresseBloc", appartement.IdBloc);
            ViewData["IdLocataire"] = new SelectList(_context.Locataires, "IdLocataire", "EmailLocataire", appartement.IdLocataire);
            return View(appartement);
        }

        // POST: Appartements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAppartement,IdBloc,IdLocataire,SuperficieAppartement,NbBalcons,PrixLocation")] Appartement appartement)
        {
            if (id != appartement.IdAppartement)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appartement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppartementExists(appartement.IdAppartement))
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
            ViewData["IdBloc"] = new SelectList(_context.Blocs, "IdBloc", "AdresseBloc", appartement.IdBloc);
            ViewData["IdLocataire"] = new SelectList(_context.Locataires, "IdLocataire", "EmailLocataire", appartement.IdLocataire);
            return View(appartement);
        }

        // GET: Appartements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appartement = await _context.Appartements
                .Include(a => a.IdBlocNavigation)
                .Include(a => a.IdLocataireNavigation)
                .FirstOrDefaultAsync(m => m.IdAppartement == id);
            if (appartement == null)
            {
                return NotFound();
            }

            return View(appartement);
        }

        // POST: Appartements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appartement = await _context.Appartements.FindAsync(id);
            _context.Appartements.Remove(appartement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppartementExists(int id)
        {
            return _context.Appartements.Any(e => e.IdAppartement == id);
        }
    }
}
