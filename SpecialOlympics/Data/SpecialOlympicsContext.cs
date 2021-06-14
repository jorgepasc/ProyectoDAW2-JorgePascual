using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpecialOlympics.Models;
using SpecialOlympics.Models.StoredProcedures;

namespace SpecialOlympics.Data
{
    public class SpecialOlympicsContext : IdentityDbContext
    {
        #region Constructor
        public SpecialOlympicsContext(DbContextOptions<SpecialOlympicsContext> options)
            : base(options)
        {
        }
        #endregion

        #region Voluntarios
        public DbSet<Voluntario> Voluntarios { get; set; }

        /// <summary>
        /// Lista de Voluntarios que participan en una actividad. Ya sea Campeonato o Entrenamiento
        /// </summary>
        public DbSet<SpGetVoluntariosFromActividadByID> VoluntariosFromActividad { get; set; }
        #endregion

        #region Entrenamientos
        public DbSet<Entrenamiento> Entrenamientos { get; set; }
        public DbSet<VoluntarioEntrenamiento> VoluntariosEntrenamientos { get; set; }
        /// <summary>
        /// Lista de Entrenamientos en los que participa un Voluntario
        /// </summary>
        public DbSet<SpGetEntrenamientosFromVoluntarioByID> EntrenamientosFromVoluntario { get; set; }
        #endregion

        #region Campeonatos
        public DbSet<Campeonato> Campeonatos { get; set; }
        public DbSet<VoluntarioCampeonato> VoluntariosCampeonatos { get; set; }

        /// <summary>
        /// Lista de Campeonatos en los que participa un Voluntario
        /// </summary>
        public DbSet<SpGetCampeonatosFromVoluntarioByID> CampeonatosFromVoluntario { get; set; }
        #endregion        

        public DbSet<Documento> Documentos { get; set; }

    }
}