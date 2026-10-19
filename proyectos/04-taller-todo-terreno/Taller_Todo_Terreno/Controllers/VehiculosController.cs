using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Taller_Todo_Terreno.Data;
using Taller_Todo_Terreno.Models;

namespace Taller_Todo_Terreno.Controllers
{
    [Authorize]
    public class VehiculosController : Controller
    {
        private readonly TallerContext _context;

        public VehiculosController(TallerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            var tallerContext = _context.Vehiculos
                .Include(v => v.Dueno)
                .Where(v => v.DuenoID == duenoId);

            return View(await tallerContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Dueno)
                .Include(v => v.Citas)
                    .ThenInclude(c => c.Horario)
                .FirstOrDefaultAsync(m => m.VehiculoId == id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            if (vehiculo.DuenoID != duenoId)
            {
                return Forbid();
            }

            // Próxima cita: activa y con fecha de hoy en adelante
            var proximaCita = vehiculo.Citas?
                .Where(c => c.Estado == "Activa" && c.Horario.Fecha >= DateTime.Today)
                .OrderBy(c => c.Horario.Fecha)
                .ThenBy(c => c.Horario.HoraInicio)
                .FirstOrDefault();

            ViewData["ProximaCita"] = proximaCita;

            // Historial: todas las citas ordenadas de más reciente a más antigua,
            // con su estado calculado (Cancelada / Completada / Activa)
            var historial = vehiculo.Citas?
                .OrderByDescending(c => c.Horario.Fecha)
                .ThenByDescending(c => c.Horario.HoraInicio)
                .Select(c => new
                {
                    Cita = c,
                    EstadoMostrado = c.Estado == "Cancelada"
                        ? "Cancelada"
                        : (c.Horario.Fecha < DateTime.Today ? "Completada" : "Activa")
                })
                .ToList();

            ViewData["Historial"] = historial;

            return View(vehiculo);
        }

        public IActionResult Create()
        {
            ViewData["DuenoID"] = new SelectList(_context.Duenos, "DuenoID", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehiculoId,Placa,Marca,Modelo,Año,TipoTransmision,DuenoID")] Vehiculo vehiculo)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);
            vehiculo.DuenoID = duenoId; // se ignora lo que venga del formulario

            if (ModelState.IsValid)
            {
                _context.Add(vehiculo);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Vehículo registrado con éxito.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DuenoID"] = new SelectList(_context.Duenos, "DuenoID", "Nombre", vehiculo.DuenoID);
            return View(vehiculo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            if (vehiculo.DuenoID != duenoId)
            {
                return Forbid();
            }

            ViewData["DuenoID"] = new SelectList(_context.Duenos, "DuenoID", "Nombre", vehiculo.DuenoID);
            return View(vehiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehiculoId,Placa,Marca,Modelo,Año,TipoTransmision,DuenoID")] Vehiculo vehiculo)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            if (id != vehiculo.VehiculoId)
            {
                return NotFound();
            }

            vehiculo.DuenoID = duenoId; // se ignora lo que venga del formulario, no se puede transferir el vehiculo

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Vehículo actualizado con éxito.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.VehiculoId))
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
            ViewData["DuenoID"] = new SelectList(_context.Duenos, "DuenoID", "Nombre", vehiculo.DuenoID);
            return View(vehiculo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Dueno)
                .FirstOrDefaultAsync(m => m.VehiculoId == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            if (vehiculo.DuenoID != duenoId)
            {
                return Forbid();
            }

            return View(vehiculo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo != null && vehiculo.DuenoID == duenoId)
            {
                _context.Vehiculos.Remove(vehiculo);
            }

            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Vehículo eliminado.";
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.VehiculoId == id);
        }
    }
}