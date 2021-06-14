using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecialOlympics.Data;
using SpecialOlympics.Models;
using SpecialOlympics.Models.StoredProcedures;
using SpecialOlympics.Models.ViewModels.Campeonatos;

namespace SpecialOlympics.Controllers
{
    public class CampeonatosController : Controller
    {
        private readonly SpecialOlympicsContext _context;

        public CampeonatosController(SpecialOlympicsContext context)
        {
            _context = context;
        }

        // GET: Campeonatos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Campeonatos.ToListAsync());
        }

        // GET: Campeonatos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campeonato = await _context.Campeonatos
                .FirstOrDefaultAsync(m => m.IdCampeonato == id);
            if (campeonato == null)
            {
                return NotFound();
            }

            return View(campeonato);
        }

        // GET: Campeonatos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campeonatos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCampeonato,Nombre,FechaInicio,FechaFin,Ubicacion")] Campeonato campeonato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campeonato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campeonato);
        }

        // GET: Campeonatos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campeonato = await _context.Campeonatos.FindAsync(id);
            if (campeonato == null)
            {
                return NotFound();
            }

            var campeonatoEditVM = new CampeonatoEditViewModel
            {
                Campeonato = campeonato,
                VoluntariosFromCampeonato = GetVoluntariosFromActividad(id).Result,
                VoluntariosDisponibles = GetVoluntariosDisponiblesForActividad(id).Result
            };

            return View(campeonatoEditVM);
        }

        // POST: Campeonatos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCampeonato,Nombre,FechaInicio,FechaFin,Ubicacion")] Campeonato campeonato)
        {
            if (id != campeonato.IdCampeonato)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campeonato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampeonatoExists(campeonato.IdCampeonato))
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
            return View(campeonato);
        }

        // GET: Campeonatos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campeonato = await _context.Campeonatos
                .FirstOrDefaultAsync(m => m.IdCampeonato == id);
            if (campeonato == null)
            {
                return NotFound();
            }

            return View(campeonato);
        }

        // POST: Campeonatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campeonato = await _context.Campeonatos.FindAsync(id);
            _context.Campeonatos.Remove(campeonato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #region Helpers

        private bool CampeonatoExists(int id)
        {
            return _context.Campeonatos.Any(e => e.IdCampeonato == id);
        }

        /// <summary>
        /// TODO: Refactorizar función
        /// TODO: Corregir SqlRaw -> Vulnerable a SQL Injection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<List<SpGetVoluntariosFromActividadByID>> GetVoluntariosFromActividad(int? id)
        {
            List<SpGetVoluntariosFromActividadByID> listaCampeonatos = new List<SpGetVoluntariosFromActividadByID>();
            try
            {
                string sqlQuery = String.Format("EXEC GetVoluntariosFromCampeonatoByID {0}", id);
                listaCampeonatos = await _context.VoluntariosFromActividad.FromSqlRaw(sqlQuery).ToListAsync();
            }
            catch
            {
                throw;
            }

            return listaCampeonatos;
        }

        /// <summary>
        /// TODO: Refactorizar función
        /// TODO: Corregir SqlRaw -> Vulnerable a SQL Injection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<List<SpGetVoluntariosFromActividadByID>> GetVoluntariosDisponiblesForActividad(int? id)
        {
            List<SpGetVoluntariosFromActividadByID> listaCampeonatos = new List<SpGetVoluntariosFromActividadByID>();
            try
            {
                string sqlQuery = String.Format("EXEC GetVoluntariosDisponiblesForCampeonatoByID {0}", id);
                listaCampeonatos = await _context.VoluntariosFromActividad.FromSqlRaw(sqlQuery).ToListAsync();
            }
            catch
            {
                throw;
            }

            return listaCampeonatos;
        }
        #endregion
    }
}
