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
    public class PiecesController : Controller
    {
        private readonly bd_gestion_blocsContext _context;

        public PiecesController(bd_gestion_blocsContext context)
        {
            _context = context;
        }

        // GET: Pieces
        public async Task<IActionResult> Index()
        {
            var bd_gestion_blocsContext = _context.Pieces.Include(p => p.IdAppartementNavigation);
            return View(await bd_gestion_blocsContext.ToListAsync());
        }

        // GET: Pieces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piece = await _context.Pieces
                .Include(p => p.IdAppartementNavigation)
                .FirstOrDefaultAsync(m => m.IdPiece == id);
            if (piece == null)
            {
                return NotFound();
            }

            return View(piece);
        }

        // GET: Pieces/Create
        public IActionResult Create()
        {
            ViewData["IdAppartement"] = new SelectList(_context.Appartements, "IdAppartement", "IdAppartement");
            return View();
        }

        // POST: Pieces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPiece,IdAppartement,SuperficiePiece,CouleurPiece,DateRenovation,ImagePiece")] Piece piece)
        {
            if (ModelState.IsValid)
            {
                _context.Add(piece);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAppartement"] = new SelectList(_context.Appartements, "IdAppartement", "IdAppartement", piece.IdAppartement);
            return View(piece);
        }

        // GET: Pieces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piece = await _context.Pieces.FindAsync(id);
            if (piece == null)
            {
                return NotFound();
            }
            ViewData["IdAppartement"] = new SelectList(_context.Appartements, "IdAppartement", "IdAppartement", piece.IdAppartement);
            return View(piece);
        }

        // POST: Pieces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPiece,IdAppartement,SuperficiePiece,CouleurPiece,DateRenovation,ImagePiece")] Piece piece)
        {
            if (id != piece.IdPiece)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(piece);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PieceExists(piece.IdPiece))
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
            ViewData["IdAppartement"] = new SelectList(_context.Appartements, "IdAppartement", "IdAppartement", piece.IdAppartement);
            return View(piece);
        }

        // GET: Pieces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piece = await _context.Pieces
                .Include(p => p.IdAppartementNavigation)
                .FirstOrDefaultAsync(m => m.IdPiece == id);
            if (piece == null)
            {
                return NotFound();
            }

            return View(piece);
        }

        // POST: Pieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var piece = await _context.Pieces.FindAsync(id);
            _context.Pieces.Remove(piece);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PieceExists(int id)
        {
            return _context.Pieces.Any(e => e.IdPiece == id);
        }
    }
}
