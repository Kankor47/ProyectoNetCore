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
    public class AutoresLibrosController : Controller
    {
        private readonly BibliotecaUTNContext _context;

        public AutoresLibrosController(BibliotecaUTNContext context)
        {
            _context = context;
        }

        // GET: AutoresLibros
        public async Task<IActionResult> Index()
        {
            var bibliotecaUTNContext = _context.AutoresLibros.Include(a => a.FK_Autor).Include(a => a.FK_Libro);
            return View(await bibliotecaUTNContext.ToListAsync());
        }

        // GET: AutoresLibros/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorLibro = await _context.AutoresLibros
                .Include(a => a.FK_Autor)
                .Include(a => a.FK_Libro)
                .FirstOrDefaultAsync(m => m.IdLibro == id);
            if (autorLibro == null)
            {
                return NotFound();
            }

            return View(autorLibro);
        }

        // GET: AutoresLibros/Create
        public IActionResult Create()
        {
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "IdAutor");
            ViewData["IdLibro"] = new SelectList(_context.Libros, "IdLibro", "IdLibro");
            return View();
        }

        // POST: AutoresLibros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLibro,IdAutor")] AutorLibro autorLibro)
        {
            if (ModelState.IsValid)
            {
                autorLibro.IdLibro = Guid.NewGuid();
                _context.Add(autorLibro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "IdAutor", autorLibro.IdAutor);
            ViewData["IdLibro"] = new SelectList(_context.Libros, "IdLibro", "IdLibro", autorLibro.IdLibro);
            return View(autorLibro);
        }

        // GET: AutoresLibros/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorLibro = await _context.AutoresLibros.FindAsync(id);
            if (autorLibro == null)
            {
                return NotFound();
            }
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "IdAutor", autorLibro.IdAutor);
            ViewData["IdLibro"] = new SelectList(_context.Libros, "IdLibro", "IdLibro", autorLibro.IdLibro);
            return View(autorLibro);
        }

        // POST: AutoresLibros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdLibro,IdAutor")] AutorLibro autorLibro)
        {
            if (id != autorLibro.IdLibro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autorLibro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorLibroExists(autorLibro.IdLibro))
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
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "IdAutor", autorLibro.IdAutor);
            ViewData["IdLibro"] = new SelectList(_context.Libros, "IdLibro", "IdLibro", autorLibro.IdLibro);
            return View(autorLibro);
        }

        // GET: AutoresLibros/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorLibro = await _context.AutoresLibros
                .Include(a => a.FK_Autor)
                .Include(a => a.FK_Libro)
                .FirstOrDefaultAsync(m => m.IdLibro == id);
            if (autorLibro == null)
            {
                return NotFound();
            }

            return View(autorLibro);
        }

        // POST: AutoresLibros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var autorLibro = await _context.AutoresLibros.FindAsync(id);
            _context.AutoresLibros.Remove(autorLibro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorLibroExists(Guid id)
        {
            return _context.AutoresLibros.Any(e => e.IdLibro == id);
        }
    }
}
