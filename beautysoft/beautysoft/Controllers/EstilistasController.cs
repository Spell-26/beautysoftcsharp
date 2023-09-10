using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using beautysoft.Models;

namespace beautysoft.Controllers
{
    public class EstilistasController : Controller
    {
        private readonly BeautysoftnetContext _context;

        public EstilistasController(BeautysoftnetContext context)
        {
            _context = context;
        }

        // GET: Estilistas
        public async Task<IActionResult> Index()
        {
            var beautysoftnetContext = _context.Estilista.Include(e => e.IdRolNavigation).Include(e => e.IdUsuarioNavigation);
            return View(await beautysoftnetContext.ToListAsync());
        }

        // GET: Estilistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estilista == null)
            {
                return NotFound();
            }

            var estilistas = await _context.Estilista
                .Include(e => e.IdRolNavigation)
                .Include(e => e.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdEstilista == id);
            if (estilistas == null)
            {
                return NotFound();
            }

            return View(estilistas);
        }

        // GET: Estilistas/Create
        public IActionResult Create()
        {
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Estilistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstilista,IdUsuario,IdRol")] Estilistas estilistas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estilistas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol", estilistas.IdRol);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", estilistas.IdUsuario);
            return View(estilistas);
        }

        // GET: Estilistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estilista == null)
            {
                return NotFound();
            }

            var estilistas = await _context.Estilista.FindAsync(id);
            if (estilistas == null)
            {
                return NotFound();
            }
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol", estilistas.IdRol);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", estilistas.IdUsuario);
            return View(estilistas);
        }

        // POST: Estilistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstilista,IdUsuario,IdRol")] Estilistas estilistas)
        {
            if (id != estilistas.IdEstilista)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estilistas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstilistasExists(estilistas.IdEstilista))
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
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol", estilistas.IdRol);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", estilistas.IdUsuario);
            return View(estilistas);
        }

        // GET: Estilistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estilista == null)
            {
                return NotFound();
            }

            var estilistas = await _context.Estilista
                .Include(e => e.IdRolNavigation)
                .Include(e => e.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdEstilista == id);
            if (estilistas == null)
            {
                return NotFound();
            }

            return View(estilistas);
        }

        // POST: Estilistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estilista == null)
            {
                return Problem("Entity set 'BeautysoftnetContext.Estilista'  is null.");
            }
            var estilistas = await _context.Estilista.FindAsync(id);
            if (estilistas != null)
            {
                _context.Estilista.Remove(estilistas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstilistasExists(int id)
        {
          return (_context.Estilista?.Any(e => e.IdEstilista == id)).GetValueOrDefault();
        }
    }
}
