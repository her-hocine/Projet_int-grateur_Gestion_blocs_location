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
    public class PlaceParkingsController : Controller
    {
        private readonly bd_gestion_blocsContext _context;

        public PlaceParkingsController(bd_gestion_blocsContext context)
        {
            _context = context;
        }

        // GET: PlaceParkings
        public async Task<IActionResult> Index()
        {
            var bd_gestion_blocsContext = _context.PlaceParkings.Include(p => p.IdBlocNavigation).Include(p => p.IdLocataireNavigation);
            return View(await bd_gestion_blocsContext.ToListAsync());
        }

        // GET: PlaceParkings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeParking = await _context.PlaceParkings
                .Include(p => p.IdBlocNavigation)
                .Include(p => p.IdLocataireNavigation)
                .FirstOrDefaultAsync(m => m.IdPlace == id);
            if (placeParking == null)
            {
                return NotFound();
            }

            return View(placeParking);
        }

        // GET: PlaceParkings/Create
        public IActionResult Create()
        {
            ViewData["IdBloc"] = new SelectList(_context.Blocs, "IdBloc", "AdresseBloc");
            ViewData["IdLocataire"] = new SelectList(_context.Locataires, "IdLocataire", "EmailLocataire");
            return View();
        }

        // POST: PlaceParkings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPlace,IdBloc,IdLocataire,DispoPark")] PlaceParking placeParking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placeParking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBloc"] = new SelectList(_context.Blocs, "IdBloc", "AdresseBloc", placeParking.IdBloc);
            ViewData["IdLocataire"] = new SelectList(_context.Locataires, "IdLocataire", "EmailLocataire", placeParking.IdLocataire);
            return View(placeParking);
        }

        // GET: PlaceParkings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeParking = await _context.PlaceParkings.FindAsync(id);
            if (placeParking == null)
            {
                return NotFound();
            }
            ViewData["IdBloc"] = new SelectList(_context.Blocs, "IdBloc", "AdresseBloc", placeParking.IdBloc);
            ViewData["IdLocataire"] = new SelectList(_context.Locataires, "IdLocataire", "EmailLocataire", placeParking.IdLocataire);
            return View(placeParking);
        }

        // POST: PlaceParkings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPlace,IdBloc,IdLocataire,DispoPark")] PlaceParking placeParking)
        {
            if (id != placeParking.IdPlace)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placeParking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceParkingExists(placeParking.IdPlace))
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
            ViewData["IdBloc"] = new SelectList(_context.Blocs, "IdBloc", "AdresseBloc", placeParking.IdBloc);
            ViewData["IdLocataire"] = new SelectList(_context.Locataires, "IdLocataire", "EmailLocataire", placeParking.IdLocataire);
            return View(placeParking);
        }

        // GET: PlaceParkings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeParking = await _context.PlaceParkings
                .Include(p => p.IdBlocNavigation)
                .Include(p => p.IdLocataireNavigation)
                .FirstOrDefaultAsync(m => m.IdPlace == id);
            if (placeParking == null)
            {
                return NotFound();
            }

            return View(placeParking);
        }

        // POST: PlaceParkings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placeParking = await _context.PlaceParkings.FindAsync(id);
            _context.PlaceParkings.Remove(placeParking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceParkingExists(int id)
        {
            return _context.PlaceParkings.Any(e => e.IdPlace == id);
        }
    }
}
