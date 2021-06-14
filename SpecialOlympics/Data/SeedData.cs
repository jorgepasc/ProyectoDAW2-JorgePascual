using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpecialOlympics.Models;
using System;
using System.Linq;

namespace SpecialOlympics.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SpecialOlympicsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SpecialOlympicsContext>>()))
            {
                // Look for any movies.
                if (!context.Voluntarios.Any())
                {
                    context.Voluntarios.AddRange(
                        new Voluntario
                        {
                            Nombre = "Jorge",
                            Apellido1 = "Perez",
                            Apellido2 = "Gaston",
                            DNI = "12345678A",
                            Telefono1 = "65656123",
                            Email = "jorge@gmail.com",
                            DireccionCompleta = "Av. Gran Via 8",
                            Poblacion = "Zaragoza",
                            Provincia = "Zaragoza",
                            CodigoPostal = "50009",
                            FechaNacimiento = new DateTime(2000, 01, 01),
                            FechaAlta = new DateTime(2018, 06, 12),
                            TelefonoEmergencia = "616322319",
                            IsAlergico = true,
                            IsActivo = true
                        },

                        new Voluntario
                        {
                            Nombre = "Mickey",
                            Apellido1 = "Mouse",
                            DNI = "12345678A",
                            Telefono1 = "777777772",
                            Email = "disney@gmail.com",
                            DireccionCompleta = "Calle Buenavista s/n",
                            Poblacion = "Cuarte",
                            Provincia = "Zaragoza",
                            CodigoPostal = "40005",
                            FechaNacimiento = new DateTime(1928, 11, 18),
                            FechaAlta = new DateTime(2010, 12, 12),
                            TelefonoEmergencia = "61333777",
                            IsAlergico = true,
                            IsActivo = false
                        },

                        new Voluntario
                        {
                            Nombre = "Pepe",
                            Apellido1 = "Ibañez",
                            Apellido2 = "Del Rio",
                            DNI = "12345678A",
                            Telefono1 = "777777772",
                            Email = "ibanez@gmail.com",
                            DireccionCompleta = "Pza. España 2",
                            Poblacion = "Zaragoza",
                            Provincia = "Zaragoza",
                            CodigoPostal = "50012",
                            FechaNacimiento = new DateTime(1998, 11, 07),
                            FechaAlta = new DateTime(2015, 02, 15),
                            TelefonoEmergencia = "61333777",
                            IsAlergico = false,
                            IsActivo = true
                        },

                        new Voluntario
                        {
                            Nombre = "Andrea",
                            Apellido1 = "Perez",
                            Apellido2 = "Berzosa",
                            DNI = "12345678A",
                            Telefono1 = "777777772",
                            Email = "andrea@gmail.com",
                            DireccionCompleta = "Pza. España 2",
                            Poblacion = "Zaragoza",
                            Provincia = "Zaragoza",
                            CodigoPostal = "50012",
                            FechaNacimiento = new DateTime(1999, 04, 19),
                            FechaAlta = new DateTime(2019, 12, 15),
                            TelefonoEmergencia = "61333777",
                            IsAlergico = false,
                            IsActivo = true
                        }
                    );
                }

                if (!context.Entrenamientos.Any())
                {
                    context.Entrenamientos.AddRange(
                        new Entrenamiento
                        {
                            Nombre = "Baloncesto 1",
                            Ubicacion = "La granja",
                            FechaInicio = new DateTime(2020, 01, 01),
                            Observaciones = "Bla bla"
                        },

                        new Entrenamiento
                        {
                            Nombre = "Atletismo",
                            Ubicacion = "El huevo calle s/n",
                            FechaInicio = new DateTime(2019, 01, 01)
                        },

                        new Entrenamiento
                        {
                            Nombre = "Natación",
                            Ubicacion = "Piscina Duquesa",
                            FechaInicio = new DateTime(2020, 01, 01),
                            Observaciones = "Contratar socorrista"
                        },

                        new Entrenamiento
                        {
                            Nombre = "Pádel iniciación",
                            Ubicacion = "CDM Gran Vía",
                            FechaInicio = new DateTime(2020, 01, 01)
                        }
                    );
                }

                if (!context.Campeonatos.Any())
                {
                    context.Campeonatos.AddRange(
                        new Campeonato
                        {
                            Nombre = "Olimpiadas 2020",
                            FechaInicio = new DateTime(2020, 07, 01),
                            FechaFin = new DateTime(2020, 07, 10),
                            Ubicacion = "Dubai"
                        },

                        new Campeonato
                        {
                            Nombre = "Campeonato de España de Atletismo",
                            FechaInicio = new DateTime(2021, 05, 13),
                            FechaFin = new DateTime(2021, 05, 16),
                            Ubicacion = "El huevo, Zaragoza"
                        },

                        new Campeonato
                        {
                            Nombre = "Marcha Senderista",
                            FechaInicio = new DateTime(2018, 01, 13),
                            FechaFin = new DateTime(2018, 01, 13),
                            Ubicacion = "Pirineos"
                        }

                    );
                }

                context.SaveChanges();

                if (!context.VoluntariosEntrenamientos.Any())
                {
                    context.VoluntariosEntrenamientos.AddRange(
                        new VoluntarioEntrenamiento
                        {
                            IdVoluntario = 1,
                            IdEntrenamiento = 1,
                            Funcion = "Entrenador"
                        },

                        new VoluntarioEntrenamiento
                        {
                            IdVoluntario = 2,
                            IdEntrenamiento = 1,
                            Funcion = "Entrenador"
                        },

                        new VoluntarioEntrenamiento
                        {
                            IdVoluntario = 2,
                            IdEntrenamiento = 2,
                            Funcion = "Entrenador"
                        },

                        new VoluntarioEntrenamiento
                        {
                            IdVoluntario = 3,
                            IdEntrenamiento = 3,
                            Funcion = "Entrenador"
                        }
                    );
                }

                if (!context.VoluntariosCampeonatos.Any())
                {
                    context.VoluntariosCampeonatos.AddRange(
                        new VoluntarioCampeonato
                        {
                            IdVoluntario = 1,
                            IdCampeonato = 1,
                            Funcion = "Voluntario"
                        },

                        new VoluntarioCampeonato
                        {
                            IdVoluntario = 2,
                            IdCampeonato = 2,
                            Funcion = "Entrenador"
                        },

                        new VoluntarioCampeonato
                        {
                            IdVoluntario = 3,
                            IdCampeonato = 1,
                            Funcion = "Entrenador"
                        },

                        new VoluntarioCampeonato
                        {
                            IdVoluntario = 3,
                            IdCampeonato = 2,
                            Funcion = "Organizacion"
                        },

                        new VoluntarioCampeonato
                        {
                            IdVoluntario = 4,
                            IdCampeonato = 3,
                            Funcion = "Organizacion"
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}