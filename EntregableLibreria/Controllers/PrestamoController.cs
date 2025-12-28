using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntregableLibreria.Data;
using EntregableLibreria.Models;

namespace EntregableLibreria.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly LibreriaContext _context;

        public PrestamosController(LibreriaContext context)
        {
            _context = context;
        }

        // 1. LISTA DE PRÉSTAMOS (Incluye datos de Libro y Usuario)
        public async Task<IActionResult> Index()
        {
            var prestamos = _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario);
            return View(await prestamos.ToListAsync());
        }

        // 2. MOSTRAR FORMULARIO (Cargar listas desplegables)
        public IActionResult Create()
        {
            // Enviamos la lista de Usuarios y Libros a la vista para los <select>
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo");
            return View();
        }

        // 3. GUARDAR PRÉSTAMO (Con validación de negocio)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prestamo prestamo)
        {
            // REGLA DE NEGOCIO: Verificar cuántos libros tiene prestados este usuario
            int prestamosActivos = _context.Prestamos
                .Count(p => p.UsuarioId == prestamo.UsuarioId && p.FechaDevolucion == null);

            if (prestamosActivos >= 3)
            {
                ModelState.AddModelError("", "¡ERROR! El usuario ya tiene 3 libros sin devolver.");
            }

            if (ModelState.IsValid)
            {
                prestamo.FechaPrestamo = DateTime.Now; // Fecha actual automática
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si falla, recargamos las listas para que no salga error en la vista
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", prestamo.UsuarioId);
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", prestamo.LibroId);
            return View(prestamo);
        }

        // 4. DEVOLVER LIBRO (Acción rápida)
        public async Task<IActionResult> Devolver(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                prestamo.FechaDevolucion = DateTime.Now; // Marca devolución hoy
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}