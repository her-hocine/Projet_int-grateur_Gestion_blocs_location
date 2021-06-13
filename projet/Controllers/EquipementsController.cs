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
    public class EquipementsController : Controller
    {
        private readonly bd_gestion_blocsContext _context;

        public EquipementsController(bd_gestion_blocsContext context)
        {
            _context = context;
        }

        // GET: Equipements
        public async Task<IActionResult> Index()
        {
            var bd_gestion_blocsContext = _context.Equipements.Include(e => e.IdPieceNavigation);
            return View(await bd_gestion_blocsContext.ToListAsync());
        }

        // GET: Equipements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipement = await _context.Equipements
                .Include(e => e.IdPieceNavigation)
                .FirstOrDefaultAsync(m => m.IdEquipement == id);
            if (equipement == null)
            {
                return NotFound();
            }

            return View(equipement);
        }

        // GET: Equipements/Create
        public IActionResult Create()
        {
            ViewData["IdPiece"] = new SelectList(_context.Pieces, "IdPiece", "CouleurPiece");
            return View();
        }

        // POST: Equipements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEquipement,IdPiece,NomEquipement,CategorieEquipement,FournisseurEquipement,MarqueEquipement,DateAchatEquip,DateFinGarantieEquip,EtatEquipement,ImageEquipement")] Equipement equipement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPiece"] = new SelectList(_context.Pieces, "IdPiece", "CouleurPiece", equipement.IdPiece);
            return View(equipement);
        }

        // GET: Equipements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipement = await _context.Equipements.FindAsync(id);
            if (equipement == null)
            {
                return NotFound();
            }
            ViewData["IdPiece"] = new SelectList(_context.Pieces, "IdPiece", "CouleurPiece", equipement.IdPiece);
            return View(equipement);
        }

        // POST: Equipements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEquipement,IdPiece,NomEquipement,CategorieEquipement,FournisseurEquipement,MarqueEquipement,DateAchatEquip,DateFinGarantieEquip,EtatEquipement,ImageEquipement")] Equipement equipement)
        {
            if (id != equipement.IdEquipement)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipementExists(equipement.IdEquipement))
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
            ViewData["IdPiece"] = new SelectList(_context.Pieces, "IdPiece", "CouleurPiece", equipement.IdPiece);
            return View(equipement);
        }

        // GET: Equipements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipement = await _context.Equipements
                .Include(e => e.IdPieceNavigation)
                .FirstOrDefaultAsync(m => m.IdEquipement == id);
            if (equipement == null)
            {
                return NotFound();
            }

            return View(equipement);
        }

        // POST: Equipements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipement = await _context.Equipements.FindAsync(id);
            _context.Equipements.Remove(equipement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipementExists(int id)
        {
            return _context.Equipements.Any(e => e.IdEquipement == id);
        }
    }
}
