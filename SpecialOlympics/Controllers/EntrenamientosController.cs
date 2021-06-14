using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecialOlympics.Data;
using SpecialOlympics.Models;
using SpecialOlympics.Models.StoredProcedures;
using SpecialOlympics.Models.ViewModels.Entrenamientos;

namespace SpecialOlympics.Controllers
{
    public class EntrenamientosController : Controller
    {
        private readonly SpecialOlympicsContext _context;

        public EntrenamientosController(SpecialOlympicsContext context)
        {
            _context = context;
        }

        // GET: Entrenamientos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Entrenamientos.ToListAsync());
        }

        // GET: Entrenamientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrenamiento = await _context.Entrenamientos
                .FirstOrDefaultAsync(m => m.IdEntrenamiento == id);
            if (entrenamiento == null)
            {
                return NotFound();
            }

            return View(entrenamiento);
        }

        // GET: Entrenamientos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entrenamientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEntrenamiento,Nombre,FechaInicio,Ubicacion")] Entrenamiento entrenamiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entrenamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entrenamiento);
        }

        // GET: Entrenamientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entrenamiento entrenamiento = await _context.Entrenamientos.FindAsync(id);
            if (entrenamiento == null)
            {
                return NotFound();
            }

            var entrenamientoEditVM = new EntrenamientoEditViewModel
            {
                Entrenamiento = entrenamiento,
                VoluntariosFromEntrenamiento = GetVoluntariosFromActividad(id).Result,
                VoluntariosDisponibles = GetVoluntariosDisponiblesForActividad(id).Result
            };

            return View(entrenamientoEditVM);
        }

        // POST: Entrenamientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEntrenamiento,Nombre,FechaInicio,Ubicacion")] Entrenamiento entrenamiento)
        {
            if (id != entrenamiento.IdEntrenamiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrenamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntrenamientoExists(entrenamiento.IdEntrenamiento))
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
            return View(entrenamiento);
        }

        // GET: Entrenamientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrenamiento = await _context.Entrenamientos
                .FirstOrDefaultAsync(m => m.IdEntrenamiento == id);
            if (entrenamiento == null)
            {
                return NotFound();
            }

            return View(entrenamiento);
        }

        // POST: Entrenamientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrenamiento = await _context.Entrenamientos.FindAsync(id);
            _context.Entrenamientos.Remove(entrenamiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #region Helpers
        private bool EntrenamientoExists(int id)
        {
            return _context.Entrenamientos.Any(e => e.IdEntrenamiento == id);
        }

        /// <summary>
        /// TODO: Refactorizar función
        /// TODO: Corregir SqlRaw -> Vulnerable a SQL Injection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<List<SpGetVoluntariosFromActividadByID>> GetVoluntariosFromActividad(int? id)
        {
            List<SpGetVoluntariosFromActividadByID> listaEntrenamientos = new List<SpGetVoluntariosFromActividadByID>();
            try
            {
                string sqlQuery = String.Format("EXEC GetVoluntariosFromEntrenamientoByID {0}", id);
                listaEntrenamientos = await _context.VoluntariosFromActividad.FromSqlRaw(sqlQuery).ToListAsync();
            }
            catch
            {
                throw;
            }

            return listaEntrenamientos;
        }

        /// <summary>
        /// TODO: Refactorizar función
        /// TODO: Corregir SqlRaw -> Vulnerable a SQL Injection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<List<SpGetVoluntariosFromActividadByID>> GetVoluntariosDisponiblesForActividad(int? id)
        {
            List<SpGetVoluntariosFromActividadByID> listaEntrenamientos = new List<SpGetVoluntariosFromActividadByID>();
            try
            {
                string sqlQuery = String.Format("EXEC GetVoluntariosDisponiblesForEntrenamientoByID {0}", id);
                listaEntrenamientos = await _context.VoluntariosFromActividad.FromSqlRaw(sqlQuery).ToListAsync();
            }
            catch
            {
                throw;
            }

            return listaEntrenamientos;
        }
        #endregion
    }
}
