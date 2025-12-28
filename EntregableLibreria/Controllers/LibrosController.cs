using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntregableLibreria.Data;
using EntregableLibreria.Models;

namespace EntregableLibreria.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibreriaContext _context;

        public LibrosController(LibreriaContext context)
        {
            _context = context;
        }

        // 1. PANTALLA PRINCIPAL (LISTA DE LIBROS)
        public async Task<IActionResult> Index()
        {
            // Busca todos los libros en la base de datos y los envía a la vista
            return View(await _context.Libros.ToListAsync());
        }

        // 2. PANTALLA DE CREAR (GET: Muestra el formulario vacío)
        public IActionResult Create()
        {
            return View();
        }

        // 3. GUARDAR EL LIBRO (POST: Recibe los datos y guarda)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Regresa a la lista
            }
            return View(libro);
        }
    }
}