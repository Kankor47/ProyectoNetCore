using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaUTN.Context;
using BibliotecaUTN.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BibliotecaUTN.Controllers
{
    public class LibrosController : Controller
    {
        private readonly BibliotecaUTNContext _context;

        private IHostingEnvironment _env;

        public LibrosController(BibliotecaUTNContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        private async Task<string> GuardarArchivo(IFormFile imagen, string archivo="")
        {
            if(imagen == null || imagen.Length == 0)
            {
                return archivo;
            }

            if(string.IsNullOrWhiteSpace(archivo))
            {
                archivo = $"{Guid.NewGuid()}{Path.GetExtension(imagen.FileName)}";
            }

            var ruta = Path.Combine(_env.WebRootPath,"Libros",archivo);

            using (var stream = new FileStream(ruta,FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }

            return archivo;
        }

        private string DescargarImagen(string imagen)
        {
            if(imagen == null)
            {
                return string.Empty;
            }

            var ruta = Path.Combine(_env.WebRootPath,"Libros",imagen);
            var bytes = System.IO.File.ReadAllBytes(ruta);

            return "data:image/png;base64," + Convert.ToBase64String(bytes);
        }

        // GET: Libros
        public async Task<IActionResult> Index()
        {
            var bibliotecaUTNContext = _context.Libros.Include(l => l.FK_EditorialLibro).Include(l => l.FK_GeneroLibro).Include(l => l.FK_PaisLibro);
            return View(await bibliotecaUTNContext.ToListAsync());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.FK_EditorialLibro)
                .Include(l => l.FK_GeneroLibro)
                .Include(l => l.FK_PaisLibro)
                .Include(l => l.AutoresLibros)
                    .ThenInclude(l => l.FK_Autor)
                .FirstOrDefaultAsync(m => m.IdLibro == id);
            if (libro == null)
            {
                return NotFound();
            }

            libro.Portada = DescargarImagen(libro.Imagen);

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            ViewData["IdEditorial"] = new SelectList(_context.Editoriales, "IdEditorial", "Nombre");
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "Nombre");
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre");
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLibro,ISBN,Titulo,IdEditorial,IdGenero,IdPais,Año,Imagen")] Libro libro, IFormFile portada)
        {
            if (ModelState.IsValid)
            {
                libro.Imagen = await GuardarArchivo(portada);
                libro.IdLibro = Guid.NewGuid();
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEditorial"] = new SelectList(_context.Editoriales, "IdEditorial", "Nombre", libro.IdEditorial);
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "Nombre", libro.IdGenero);
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", libro.IdPais);

            libro.Portada = DescargarImagen(libro.Imagen);

            return View(libro);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
                return NotFound();

            libro.AutoresLibros = await _context.AutoresLibros.Where(x=> x.IdLibro==libro.IdLibro).ToListAsync();

            ViewData["IdEditorial"] = new SelectList(_context.Editoriales, "IdEditorial", "Nombre", libro.IdEditorial);
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "Nombre", libro.IdGenero);
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", libro.IdPais);
            ViewData["Autores"] = new SelectList(_context.Autores,"IdAutor","Nombre");

            libro.Portada = DescargarImagen(libro.Imagen);

            return View(libro);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdLibro,ISBN,Titulo,IdEditorial,IdGenero,IdPais,Año,Imagen")] Libro libro, IFormFile portada)
        {
            if (id != libro.IdLibro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    libro.Imagen = await GuardarArchivo(portada, libro.Imagen);
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.IdLibro))
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
            ViewData["IdEditorial"] = new SelectList(_context.Editoriales, "IdEditorial", "Nombre", libro.IdEditorial);
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "Nombre", libro.IdGenero);
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", libro.IdPais);
            ViewData["Autores"] = new SelectList(_context.Autores,"IdAutor","Nommbre");

            libro.Portada = DescargarImagen(libro.Imagen);

            return View(libro);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.FK_EditorialLibro)
                .Include(l => l.FK_GeneroLibro)
                .Include(l => l.FK_PaisLibro)
                .Include(l => l.AutoresLibros)
                    .ThenInclude(l => l.FK_Autor)
                .FirstOrDefaultAsync(m => m.IdLibro == id);
            if (libro == null)
            {
                return NotFound();
            }

            libro.Portada = DescargarImagen(libro.Imagen);

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var libro = await _context.Libros.FindAsync(id);
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(Guid id)
        {
            return _context.Libros.Any(e => e.IdLibro == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAutor(Guid idLibro,Microsoft.AspNetCore.Http.IFormCollection form)
        {
            var idAutor = new Guid(form["idAutor"]);
            if (!_context.AutoresLibros.Any(x=>x.IdAutor==idAutor && x.IdLibro ==idLibro))
            {
                var autorLibro = new AutorLibro()
                {
                    IdAutor = idAutor,
                    IdLibro = idLibro
                };
                _context.AutoresLibros.Add(autorLibro);
                await _context.SaveChangesAsync();
            }
            var libro = await _context.Libros.FindAsync(idLibro);
            libro.AutoresLibros = await _context.AutoresLibros.Where(x => x.IdLibro == idLibro).ToListAsync();

            ViewData["IdEditorial"] = new SelectList(_context.Editoriales,"IdEditorial","Nombre",libro.IdEditorial);
            ViewData["IdGenero"] = new SelectList(_context.Generos,"IdGenero","Nombre",libro.IdGenero);
            ViewData["IdPais"] = new SelectList(_context.Paises,"IdPais","Nombre",libro.IdPais);
            ViewData["Autores"] = new SelectList(_context.Autores,"IdAutor","Nombre");

            libro.Portada = DescargarImagen(libro.Imagen);

            return View("Edit", libro);
        }

        public async Task<IActionResult> RemoveAutor(Guid idLibro, Guid idAutor)
        {
            var autorLibro = _context.AutoresLibros.FirstOrDefault(x => x.IdAutor == idAutor && x.IdLibro == idLibro);
            if (autorLibro!=null)
            {
                _context.AutoresLibros.Remove(autorLibro);
                await _context.SaveChangesAsync();
            }
            var libro = await _context.Libros.FindAsync(idLibro);
            libro.AutoresLibros = await _context.AutoresLibros.Where(x=>x.IdLibro==idLibro).ToListAsync();

            ViewData["IdEditorial"] = new SelectList(_context.Editoriales, "IdEditorial", "Nombre", libro.IdEditorial);
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "Nombre", libro.IdGenero);
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", libro.IdPais);
            ViewData["Autores"] = new SelectList(_context.Autores, "IdAutor", "Nombre");

            libro.Portada = DescargarImagen(libro.Imagen);

            return View("Edit", libro);
        }

    }
}
