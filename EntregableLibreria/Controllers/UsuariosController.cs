using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntregableLibreria.Data;
using EntregableLibreria.Models;

namespace EntregableLibreria.Controllers
{
	public class UsuariosController : Controller
	{
		private readonly LibreriaContext _context;

		public UsuariosController(LibreriaContext context)
		{
			_context = context;
		}

		// 1. LISTA DE USUARIOS
		public async Task<IActionResult> Index()
		{
			return View(await _context.Usuarios.ToListAsync());
		}

		// 2. FORMULARIO DE REGISTRO (GET)
		public IActionResult Create()
		{
			return View();
		}

		// 3. GUARDAR USUARIO (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Usuario usuario)
		{
			if (ModelState.IsValid)
			{
				_context.Add(usuario);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(usuario);
		}
	}
}