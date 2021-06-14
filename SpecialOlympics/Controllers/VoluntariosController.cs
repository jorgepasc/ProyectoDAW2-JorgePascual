using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecialOlympics.Data;
using SpecialOlympics.Models;
using SpecialOlympics.Models.StoredProcedures;
using SpecialOlympics.Models.ViewModels.Voluntarios;

namespace SpecialOlympics.Controllers
{
    public class VoluntariosController : Controller
    {
        private readonly SpecialOlympicsContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public VoluntariosController(SpecialOlympicsContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Voluntarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Voluntarios.ToListAsync());
        }

        // GET: Voluntarios/Details/5
        public async Task<IActionResult> Details()
        {
            return View(await _context.Voluntarios.ToListAsync());
            // A mejorar: Posicionamiento de los botones, estilos de los botones. Exportaciones que hace, sino se pueden cambiar hacerlas custom. Los PDFs/impresiones salen cortados si la tabla es muy grande
            // Modal View para meter la función al añadir voluntario a actividad
            // Meter indicador de clase active en navbar
            // Mejorar exportar PDF que saca las tablas cortadas
            // Poner imagenes en pequeño en las tablas
            // Confirmación del email y recuperar contraseña
        }

        // GET: Voluntarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Voluntarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVoluntario,Nombre,Apellido1,Apellido2,DNI," +
            "Telefono1,Email,DireccionCompleta,Poblacion,Provincia,CodigoPostal," +
            "FechaNacimiento,FechaAlta,Telefono2,TelefonoEmergencia,IsAlergico,DescripcionAlergias,IsActivo,Observaciones,ProfileImagePath")] Voluntario voluntario, IFormFile Foto, IFormFile Documento1)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Foto != null)
                        voluntario.ProfileImagePath = Utils.Utils.SaveFileInLocalFolder(Foto, Path.Combine(webHostEnvironment.WebRootPath, "images"), voluntario.ProfileImagePath);
                    if (Documento1 != null)
                        voluntario.RutaDocumento1 = Utils.Utils.SaveFileInLocalFolder(Documento1, Path.Combine(webHostEnvironment.WebRootPath, "Uploads"), voluntario.RutaDocumento1);
                }
                catch
                {
                    throw;
                }

                _context.Add(voluntario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(voluntario);
        }

        // GET: Voluntarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voluntario = await _context.Voluntarios.FindAsync(id);
            if (voluntario == null)
            {
                return NotFound();
            }

            // Creamos el ViewModel para pasarle a la vista de Edit
            var modelDetails = new VoluntarioEditViewModel
            {
                Voluntario = voluntario,
                Foto = Utils.Utils.GetFormFileFromLocalFile(voluntario.ProfileImagePath, Path.Combine(webHostEnvironment.WebRootPath, "images")),
                Documento1 = Utils.Utils.GetFormFileFromLocalFile(voluntario.RutaDocumento1, Path.Combine(webHostEnvironment.WebRootPath, "Uploads")),
                EntrenamientosFromVoluntario = GetEntrenamientosFromVoluntario(id).Result,
                CampeonatosFromVoluntario = GetCampeonatosFromVoluntario(id).Result
            };

            return View(modelDetails);
        }

        // POST: Voluntarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Edit(int id, VoluntarioEditViewModel model)
        {
            if (id != model.Voluntario.IdVoluntario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Foto != null)
                        model.Voluntario.ProfileImagePath = Utils.Utils.SaveFileInLocalFolder(model.Foto, Path.Combine(webHostEnvironment.WebRootPath, "images"), oldName: model.Voluntario.ProfileImagePath);
                    if (model.Documento1 != null)
                        model.Voluntario.RutaDocumento1 = Utils.Utils.SaveFileInLocalFolder(model.Documento1, Path.Combine(webHostEnvironment.WebRootPath, "Uploads"), oldName: model.Voluntario.RutaDocumento1);
                }
                catch
                {
                    throw;
                }

                try
                {
                    _context.Update(model.Voluntario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoluntariosExists(model.Voluntario.IdVoluntario))
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
            return View(model.Voluntario);
        }

        // GET: Voluntarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voluntarios = await _context.Voluntarios
                .FirstOrDefaultAsync(m => m.IdVoluntario == id);
            if (voluntarios == null)
            {
                return NotFound();
            }

            return View(voluntarios);
        }

        // POST: Voluntarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voluntarios = await _context.Voluntarios.FindAsync(id);
            _context.Voluntarios.Remove(voluntarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #region Helpers        

        private bool VoluntariosExists(int id)
        {
            return _context.Voluntarios.Any(e => e.IdVoluntario == id);
        }

        /// <summary>
        /// TODO: Esta función es estructuralmente exactamente igual que la de GetEntrenamientos. Seguro que se podría refactorizar
        /// TODO: Corregir SqlRaw -> Vulnerable a SQL Injection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<List<SpGetEntrenamientosFromVoluntarioByID>> GetEntrenamientosFromVoluntario(int? id)
        {
            List<SpGetEntrenamientosFromVoluntarioByID> listaEntrenamientos = new List<SpGetEntrenamientosFromVoluntarioByID>();
            try
            {
                string sqlQuery = String.Format("EXEC GetEntrenamientosFromVoluntarioByID {0}", id);
                listaEntrenamientos = await _context.EntrenamientosFromVoluntario.FromSqlRaw(sqlQuery).ToListAsync();
            }
            catch
            {
                throw;
            }

            return listaEntrenamientos;
        }

        /// <summary>
        /// TODO: Esta función es estructuralmente exactamente igual que la de GetEntrenamientos. Seguro que se podría refactorizar
        /// TODO: Corregir SqlRaw -> Vulnerable a SQL Injection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<List<SpGetCampeonatosFromVoluntarioByID>> GetCampeonatosFromVoluntario(int? id)
        {
            List<SpGetCampeonatosFromVoluntarioByID> listaCampeonatos = new List<SpGetCampeonatosFromVoluntarioByID>();
            try
            {
                string sqlQuery = String.Format("EXEC GetCampeonatosFromVoluntarioByID {0}", id);
                listaCampeonatos = await _context.CampeonatosFromVoluntario.FromSqlRaw(sqlQuery).ToListAsync();
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
