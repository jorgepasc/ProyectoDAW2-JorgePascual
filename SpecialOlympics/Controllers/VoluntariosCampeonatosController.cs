using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecialOlympics.Data;
using SpecialOlympics.Models;

namespace SpecialOlympics.Controllers
{
    public class VoluntariosCampeonatosController : Controller
    {
        private readonly SpecialOlympicsContext _context;

        public VoluntariosCampeonatosController(SpecialOlympicsContext context)
        {
            _context = context;
        }

        // GET: VoluntariosCampeonatos
        public async Task<IActionResult> Index()
        {
            return Redirect(@"Campeonatos\Index");
        }

        // GET: VoluntariosCampeonatos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voluntarioCampeonato = await _context.VoluntariosCampeonatos
                .FirstOrDefaultAsync(m => m.IdVoluntarioCampeonato == id);
            if (voluntarioCampeonato == null)
            {
                return NotFound();
            }

            return View(voluntarioCampeonato);
        }

        // GET: VoluntariosCampeonatos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VoluntariosCampeonatos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] string idsFormAdd)
        {
            if (idsFormAdd == null)
                return NotFound();

            var listaIds = idsFormAdd.Split(";").ToList();
            listaIds.Remove(listaIds.Last());
            int IdCampeonato = Convert.ToInt32(listaIds[0]);

            for (int i = 1; i < listaIds.Count; i++)
            {
                var voluntarioCampeonato = new VoluntarioCampeonato();
                voluntarioCampeonato.IdCampeonato = IdCampeonato;
                voluntarioCampeonato.IdVoluntario = Convert.ToInt32(listaIds[i]);
                voluntarioCampeonato.Funcion = "Entrenador";
                _context.Add(voluntarioCampeonato);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), "Campeonatos", new { id = IdCampeonato });
        }

        // GET: VoluntariosCampeonatos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voluntarioCampeonato = await _context.VoluntariosCampeonatos.FindAsync(id);
            if (voluntarioCampeonato == null)
            {
                return NotFound();
            }
            return View(voluntarioCampeonato);
        }

        // POST: VoluntariosCampeonatos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVoluntarioCampeonato,IdVoluntario,IdCampeonato,Funcion")] VoluntarioCampeonato voluntarioCampeonato)
        {
            if (id != voluntarioCampeonato.IdVoluntarioCampeonato)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voluntarioCampeonato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoluntarioCampeonatoExists(voluntarioCampeonato.IdVoluntarioCampeonato))
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
            return View(voluntarioCampeonato);
        }

        // GET: VoluntariosCampeonatos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voluntarioCampeonato = await _context.VoluntariosCampeonatos
                .FirstOrDefaultAsync(m => m.IdVoluntarioCampeonato == id);
            if (voluntarioCampeonato == null)
            {
                return NotFound();
            }

            return View(voluntarioCampeonato);
        }

        // POST: VoluntariosCampeonatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voluntarioCampeonato = await _context.VoluntariosCampeonatos.FindAsync(id);
            _context.VoluntariosCampeonatos.Remove(voluntarioCampeonato);
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
                VoluntarioCampeonato voluntarioCampeonato = await _context.VoluntariosCampeonatos.FindAsync(Convert.ToInt32(listaIds[i]));               
                _context.Remove(voluntarioCampeonato);
            }

            await _context.SaveChangesAsync();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool VoluntarioCampeonatoExists(int id)
        {
            return _context.VoluntariosCampeonatos.Any(e => e.IdVoluntarioCampeonato == id);
        }
    }
}
