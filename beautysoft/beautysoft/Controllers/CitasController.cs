using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using beautysoft.Models;
using Microsoft.AspNetCore.Razor.Language;

namespace beautysoft.Controllers
{
    public class CitasController : Controller
    {
        private readonly BeautysoftnetContext _context;

        public CitasController(BeautysoftnetContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {   
           var citas = await _context.Cita
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdEstilistaNavigation)
                .Include(c => c.IdServicioNavigation)
                .Select(
                    c => new Citas
                    {   
                        IdCita = c.IdCita,
                        ClienteNombre = c.IdClienteNavigation.IdUsuarioNavigation.Nombre,
                        EstilistaNombre = c.IdEstilistaNavigation.IdUsuarioNavigation.Nombre,
                        ServicioNombre = c.IdServicioNavigation.Nombre,
                        Fecha = c.Fecha,
                        Hora = c.Hora,
                        Precio = c.IdServicioNavigation.Precio,
                    }               
                ).ToListAsync();

            /*var beautysoftnetContext = _context.Cita.Include(c => c.IdClienteNavigation).Include(c => c.IdEstilistaNavigation).Include(c => c.IdServicioNavigation);
            return View(await beautysoftnetContext.ToListAsync());*/
            return View( citas );
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {

           
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citas = await _context.Cita
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdEstilistaNavigation)
                .Include(c => c.IdServicioNavigation)
                .Select(
                    c => new Citas
                    {
                        IdCita = c.IdCita,
                        ClienteNombre = c.IdClienteNavigation.IdUsuarioNavigation.Nombre,
                        EstilistaNombre = c.IdEstilistaNavigation.IdUsuarioNavigation.Nombre,
                        ServicioNombre = c.IdServicioNavigation.Nombre,
                        Fecha = c.Fecha,
                        Hora = c.Hora,
                    })
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (citas == null)
            {
                return NotFound();
            }

            return View(citas);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["IdEstilista"] = new SelectList(_context.Estilista, "IdEstilista", "IdEstilista");
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,IdCliente,IdEstilista,IdServicio,Fecha,Hora")] Citas citas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", citas.IdCliente);
            ViewData["IdEstilista"] = new SelectList(_context.Estilista, "IdEstilista", "IdEstilista", citas.IdEstilista);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", citas.IdServicio);
            return View(citas);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           /* var cita = await _context.Cita
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdEstilistaNavigation)
                .Include(c => c.IdServicioNavigation)
                .FirstOrDefaultAsync(c => c.IdCita == id);
            if(cita == null)
            {
                return NotFound();
            }

            var citaModel = new Citas
            {
                IdCita = cita.IdCita,
                IdCliente = cita.IdClienteNavigation.IdCliente,
                IdEstilista = cita.IdEstilistaNavigation.IdEstilista,
                IdServicio = cita.IdServicioNavigation.IdServicio,
                Fecha = cita.Fecha,
                Hora = cita.Hora,
                ClienteNombre = cita.IdClienteNavigation.IdUsuarioNavigation.Nombre,
                EstilistaNombre = cita.IdEstilistaNavigation.IdUsuarioNavigation.Nombre,
                ServicioNombre = cita.IdServicioNavigation.Nombre,
            };

            return View(citaModel);*/
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citas = await _context.Cita.FindAsync(id);
            if (citas == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", citas.IdCliente);
            ViewData["IdEstilista"] = new SelectList(_context.Estilista, "IdEstilista", "IdEstilista", citas.IdEstilista);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", citas.IdServicio);
           
            return View(citas);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public async Task<IActionResult> Edit(int id,  Citas cita)
        {
            if (id != cita.IdCita) {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var thisCita = await _context.Cita.FindAsync(id);

                if(thisCita == null)
                {
                    return NotFound();
                }

                thisCita.ClienteNombre = cita.ClienteNombre;
                thisCita.ServicioNombre = cita.ServicioNombre;
                thisCita.EstilistaNombre = cita.EstilistaNombre;
                thisCita.IdCliente = cita.IdCliente;
                thisCita.IdEstilista = cita.IdEstilista;
                thisCita.IdServicio = cita.IdServicio;
                thisCita.IdClienteNavigation = cita.IdClienteNavigation;
                thisCita.IdEstilistaNavigation = cita.IdEstilistaNavigation;
                thisCita.IdServicioNavigation = cita.IdServicioNavigation;
                thisCita.Fecha = cita.Fecha;
                thisCita.Hora = cita.Hora;

                _context.Update(thisCita);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(cita);
        }*/
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,IdCliente,IdEstilista,IdServicio,Fecha,Hora")] Citas citas)
        {
            if (id != citas.IdCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitasExists(citas.IdCita))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", citas.IdCliente);
            ViewData["IdEstilista"] = new SelectList(_context.Estilista, "IdEstilista", "IdEstilista", citas.IdEstilista);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", citas.IdServicio);
            return View(citas);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citas = await _context.Cita
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdEstilistaNavigation)
                .Include(c => c.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (citas == null)
            {
                return NotFound();
            }

            return View(citas);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cita == null)
            {
                return Problem("Entity set 'BeautysoftnetContext.Cita'  is null.");
            }
            var citas = await _context.Cita.FindAsync(id);
            if (citas != null)
            {
                _context.Cita.Remove(citas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitasExists(int id)
        {
          return (_context.Cita?.Any(e => e.IdCita == id)).GetValueOrDefault();
        }
    }
}
