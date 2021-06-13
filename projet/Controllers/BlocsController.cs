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
    public class BlocsController : Controller
    {
        private readonly bd_gestion_blocsContext _context;

        public BlocsController(bd_gestion_blocsContext context)
        {
            _context = context;
        }

        // GET: Blocs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blocs.ToListAsync());
        }

        // GET: Blocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloc = await _context.Blocs
                .FirstOrDefaultAsync(m => m.IdBloc == id);
            if (bloc == null)
            {
                return NotFound();
            }

            return View(bloc);
        }

        // GET: Blocs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBloc,AdresseBloc,NbEtages,Contracteur,AnneeConstruction,ImageBloc")] Bloc bloc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bloc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloc);
        }

        // GET: Blocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloc = await _context.Blocs.FindAsync(id);
            if (bloc == null)
            {
                return NotFound();
            }
            return View(bloc);
        }

        // POST: Blocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBloc,AdresseBloc,NbEtages,Contracteur,AnneeConstruction,ImageBloc")] Bloc bloc)
        {
            if (id != bloc.IdBloc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlocExists(bloc.IdBloc))
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
            return View(bloc);
        }

        // GET: Blocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloc = await _context.Blocs
                .FirstOrDefaultAsync(m => m.IdBloc == id);
            if (bloc == null)
            {
                return NotFound();
            }

            return View(bloc);
        }

        // POST: Blocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bloc = await _context.Blocs.FindAsync(id);
            _context.Blocs.Remove(bloc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlocExists(int id)
        {
            return _context.Blocs.Any(e => e.IdBloc == id);
        }
    }
}
