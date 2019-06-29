using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaUTN.Context;
using BibliotecaUTN.Models;

namespace BibliotecaUTN.Controllers
{
    public class StatusPrestamosController : Controller
    {
        private readonly BibliotecaUTNContext _context;

        public StatusPrestamosController(BibliotecaUTNContext context)
        {
            _context = context;
        }

        // GET: StatusPrestamos
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusPrestamos.ToListAsync());
        }

        // GET: StatusPrestamos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusPrestamo = await _context.StatusPrestamos
                .FirstOrDefaultAsync(m => m.IdStatusPrestamo == id);
            if (statusPrestamo == null)
            {
                return NotFound();
            }

            return View(statusPrestamo);
        }

        // GET: StatusPrestamos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusPrestamos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdStatusPrestamo,Nombre")] StatusPrestamo statusPrestamo)
        {
            if (ModelState.IsValid)
            {
                statusPrestamo.IdStatusPrestamo = Guid.NewGuid();
                _context.Add(statusPrestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusPrestamo);
        }

        // GET: StatusPrestamos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusPrestamo = await _context.StatusPrestamos.FindAsync(id);
            if (statusPrestamo == null)
            {
                return NotFound();
            }
            return View(statusPrestamo);
        }

        // POST: StatusPrestamos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdStatusPrestamo,Nombre")] StatusPrestamo statusPrestamo)
        {
            if (id != statusPrestamo.IdStatusPrestamo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusPrestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusPrestamoExists(statusPrestamo.IdStatusPrestamo))
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
            return View(statusPrestamo);
        }

        // GET: StatusPrestamos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusPrestamo = await _context.StatusPrestamos
                .FirstOrDefaultAsync(m => m.IdStatusPrestamo == id);
            if (statusPrestamo == null)
            {
                return NotFound();
            }

            return View(statusPrestamo);
        }

        // POST: StatusPrestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var statusPrestamo = await _context.StatusPrestamos.FindAsync(id);
            _context.StatusPrestamos.Remove(statusPrestamo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusPrestamoExists(Guid id)
        {
            return _context.StatusPrestamos.Any(e => e.IdStatusPrestamo == id);
        }
    }
}
