using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Taller_Todo_Terreno.Data;
using Taller_Todo_Terreno.Models;
using Microsoft.AspNetCore.Authorization;

namespace Taller_Todo_Terreno.Controllers
{
    public class DuenosController : Controller
    {
        private readonly TallerContext _context;

        public DuenosController(TallerContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            var duenos = await _context.Duenos
                .Where(d => d.DuenoID == duenoId)
                .ToListAsync();

            return View(duenos);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            if (id == null || id != duenoId)
            {
                return Forbid();
            }

            var dueno = await _context.Duenos
                .FirstOrDefaultAsync(m => m.DuenoID == id);
            if (dueno == null)
            {
                return NotFound();
            }

            return View(dueno);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DuenoID,Identificacion,Nombre,Direccion,Telefono,Email,Contrasena")] Dueno dueno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dueno);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Registro exitoso. Ya puedes iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }
            return View(dueno);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            if (id == null || id != duenoId)
            {
                return Forbid();
            }

            var dueno = await _context.Duenos.FindAsync(id);
            if (dueno == null)
            {
                return NotFound();
            }
            return View(dueno);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DuenoID,Identificacion,Nombre,Direccion,Telefono,Email,Contrasena")] Dueno dueno)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            if (id != dueno.DuenoID || id != duenoId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dueno);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Datos actualizados con éxito.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuenoExists(dueno.DuenoID))
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
            return View(dueno);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            if (id == null || id != duenoId)
            {
                return Forbid();
            }

            var dueno = await _context.Duenos
                .FirstOrDefaultAsync(m => m.DuenoID == id);
            if (dueno == null)
            {
                return NotFound();
            }

            return View(dueno);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            if (id != duenoId)
            {
                return Forbid();
            }

            var dueno = await _context.Duenos.FindAsync(id);
            if (dueno != null)
            {
                _context.Duenos.Remove(dueno);
            }

            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Dueño eliminado.";

            // Como el dueño se elimino a si mismo, cerramos su sesion
            return RedirectToAction("Logout", "Login");
        }

        private bool DuenoExists(int id)
        {
            return _context.Duenos.Any(e => e.DuenoID == id);
        }
    }
}