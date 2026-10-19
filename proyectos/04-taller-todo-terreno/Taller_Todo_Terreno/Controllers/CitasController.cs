using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Taller_Todo_Terreno.Data;
using Taller_Todo_Terreno.Models;

namespace Taller_Todo_Terreno.Controllers
{
    [Authorize]
    public class CitasController : Controller
    {
        private readonly TallerContext _context;

        public CitasController(TallerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            var citas = _context.Citas
                .Include(c => c.Vehiculo)
                .Include(c => c.Horario)
                .Where(c => c.Estado == "Activa"
                            && c.Horario.Fecha >= DateTime.Today
                            && c.Vehiculo.DuenoID == duenoId);
            return View(await citas.ToListAsync());
        }

        public IActionResult Create()
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            ViewData["VehiculoId"] = new SelectList(
                _context.Vehiculos.Where(v => v.DuenoID == duenoId), "VehiculoId", "Placa");
            ViewData["HorarioId"] = new SelectList(ObtenerHorariosConCupo(), "HorarioId", "Texto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehiculoId,HorarioId")] Cita cita)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            var horario = await _context.Horarios
                .Include(h => h.Citas)
                .FirstOrDefaultAsync(h => h.HorarioId == cita.HorarioId);

            int citasActuales = horario?.Citas?.Count(c => c.Estado == "Activa") ?? 0;

            if (horario == null || horario.Fecha < DateTime.Today || citasActuales >= horario.CupoMaximo)
            {
                ModelState.AddModelError("", "Ese horario ya no tiene cupo disponible. Por favor elige otro.");
            }

            if (ModelState.IsValid)
            {
                cita.FechaSolicitud = DateTime.Now;
                cita.Estado = "Activa";
                _context.Add(cita);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Cita reservada con éxito.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["VehiculoId"] = new SelectList(
                _context.Vehiculos.Where(v => v.DuenoID == duenoId), "VehiculoId", "Placa", cita.VehiculoId);
            ViewData["HorarioId"] = new SelectList(ObtenerHorariosConCupo(), "HorarioId", "Texto");
            return View(cita);
        }

        public async Task<IActionResult> Cancel(int? id)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Vehiculo)
                .Include(c => c.Horario)
                .FirstOrDefaultAsync(c => c.CitaId == id);

            if (cita == null)
            {
                return NotFound();
            }

            if (cita.Vehiculo.DuenoID != duenoId)
            {
                return Forbid();
            }

            return View(cita);
        }

        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelConfirmed(int id)
        {
            int duenoId = int.Parse(User.FindFirst("DuenoId").Value);

            var cita = await _context.Citas
                .Include(c => c.Vehiculo)
                .FirstOrDefaultAsync(c => c.CitaId == id);

            if (cita != null && cita.Vehiculo.DuenoID == duenoId)
            {
                cita.Estado = "Cancelada";
                _context.Update(cita);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Cita cancelada.";
            }

            return RedirectToAction(nameof(Index));
        }

        // Solo horarios de HOY en adelante, con cupo libre (contando solo citas Activas)
        private List<object> ObtenerHorariosConCupo()
        {
            var cultura = new CultureInfo("es-CR");

            return _context.Horarios
                .Include(h => h.Citas)
                .ToList()
                .Where(h => h.Fecha >= DateTime.Today
                            && (h.Citas?.Count(c => c.Estado == "Activa") ?? 0) < h.CupoMaximo)
                .Select(h => new
                {
                    HorarioId = h.HorarioId,
                    Texto = h.Fecha.ToString("dd 'de' MMMM", cultura) + " · "
                            + FormatearHora(h.HoraInicio) + " - " + FormatearHora(h.HoraFin)
                            + " (cupo: " + (h.Citas?.Count(c => c.Estado == "Activa") ?? 0) + "/" + h.CupoMaximo + ")"
                })
                .Cast<object>()
                .ToList();
        }

        public static string FormatearHora(TimeSpan hora)
        {
            return DateTime.Today.Add(hora).ToString("h:mm tt");
        }
    }
}