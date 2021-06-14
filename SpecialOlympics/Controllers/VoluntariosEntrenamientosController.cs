using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecialOlympics.Data;
using SpecialOlympics.Models;

namespace SpecialOlympics
{
    public class VoluntariosEntrenamientosController : Controller
    {
        private readonly SpecialOlympicsContext _context;

        public VoluntariosEntrenamientosController(SpecialOlympicsContext context)
        {
            _context = context;
        }

        // GET: VoluntariosEntrenamientos
        public async Task<IActionResult> Index()
        {
            return Redirect(@"Entrenamientos\Index");
        }

        // GET: VoluntariosEntrenamientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voluntarioEntrenamiento = await _context.VoluntariosEntrenamientos
                .FirstOrDefaultAsync(m => m.IdVoluntarioEntrenamiento == id);
            if (voluntarioEntrenamiento == null)
            {
                return NotFound();
            }

            return View(voluntarioEntrenamiento);
        }

        // GET: VoluntariosEntrenamientos/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: VoluntariosEntrenamientos/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("IdVoluntarioEntrenamiento,IdVoluntario,IdEntrenamiento,Funcion")] VoluntarioEntrenamiento voluntarioEntrenamiento)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(voluntarioEntrenamiento);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(voluntarioEntrenamiento);
        //}

        // POST: VoluntariosEntrenamientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] string idsForm)
        {
            if (idsForm == null)
                return NotFound();

            var listaIds = idsForm.Split(";").ToList();
            listaIds.Remove(listaIds.Last());
            int IdEntrenamiento = Convert.ToInt32(listaIds[0]);

            for (int i = 1; i < listaIds.Count; i++)
            {
                var voluntarioEntrenamiento = new VoluntarioEntrenamiento();
                voluntarioEntrenamiento.IdEntrenamiento = IdEntrenamiento;
                voluntarioEntrenamiento.IdVoluntario = Convert.ToInt32(listaIds[i]);
                voluntarioEntrenamiento.Funcion = "Entrenador";
                _context.Add(voluntarioEntrenamiento);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), "Entrenamientos", new { id = IdEntrenamiento });
        }

        // GET: VoluntariosEntrenamientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voluntarioEntrenamiento = await _context.VoluntariosEntrenamientos.FindAsync(id);
            if (voluntarioEntrenamiento == null)
            {
                return NotFound();
            }
            return View(voluntarioEntrenamiento);
        }

        // POST: VoluntariosEntrenamientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVoluntarioEntrenamiento,IdVoluntario,IdEntrenamiento,Funcion")] VoluntarioEntrenamiento voluntarioEntrenamiento)
        {
            if (id != voluntarioEntrenamiento.IdVoluntarioEntrenamiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voluntarioEntrenamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoluntarioEntrenamientoExists(voluntarioEntrenamiento.IdVoluntarioEntrenamiento))
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
            return View(voluntarioEntrenamiento);
        }

        // GET: VoluntariosEntrenamientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voluntarioEntrenamiento = await _context.VoluntariosEntrenamientos
                .FirstOrDefaultAsync(m => m.IdVoluntarioEntrenamiento == id);
            if (voluntarioEntrenamiento == null)
            {
                return NotFound();
            }

            return View(voluntarioEntrenamiento);
        }

        // POST: VoluntariosEntrenamientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voluntarioEntrenamiento = await _context.VoluntariosEntrenamientos.FindAsync(id);
            _context.VoluntariosEntrenamientos.Remove(voluntarioEntrenamiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: VoluntariosEntrenamientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMulti([FromForm] string idsFormDelete)
        {
            if (idsFormDelete == null)
                return NotFound();

            var listaIds = idsFormDelete.Split(";").ToList();
            listaIds.Remove(listaIds.Last());
            //int IdCampeonato = Convert.ToInt32(listaIds[0]);

            for (int i = 0; i < listaIds.Count; i++)
            {
                VoluntarioEntrenamiento voluntarioEntrenamiento = await _context.VoluntariosEntrenamientos.FindAsync(Convert.ToInt32(listaIds[i]));
                _context.Remove(voluntarioEntrenamiento);
            }

            await _context.SaveChangesAsync();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool VoluntarioEntrenamientoExists(int id)
        {
            return _context.VoluntariosEntrenamientos.Any(e => e.IdVoluntarioEntrenamiento == id);
        }
    }
}
