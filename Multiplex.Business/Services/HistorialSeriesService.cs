﻿using Microsoft.EntityFrameworkCore;
using Multiplex.Business.DTOs;
using Multiplex.Business.Interfaces;
using Multiplex.Domain.Contexts.AutoGenerated;
using Multiplex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Services
{
    public class HistorialSeriesService : IHistorialSeriesService
    {
        private readonly MultiplexContext context;

        public HistorialSeriesService(MultiplexContext context)
        {
            this.context = context;
        }

        public async Task<string> CreateHistorialSerie(int userId, int serieId)
        {
            try 
            {
                // Comprobar si el historial ya fue registrado
                if (!await context.HistorialSeries.AnyAsync(h => h.IdSr == serieId && h.IdUsr == userId))
                {
                    context.Add(new HistorialSeries()
                    {
                        IdSr = serieId,
                        IdUsr = userId
                    });


                    if (await context.SaveChangesAsync() > 0)
                    {
                        return "Historial registrado con éxito.";
                    }
                    else
                    {
                        return "No se pudo registrar el historial.";
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<IEnumerable<SerieDTO>> GetHistorialSeries(int idUsr)
        {
            var historialSeries = await context.HistorialSeries
                .Where(h => h.IdUsr == idUsr)
                .Select(h => new SerieDTO
                {
                    Id = h.IdSr,
                    Nombre = h.IdSrNavigation.NombreSr,
                    Portada = h.IdSrNavigation.PortadaSr,
                    Descripcion = h.IdSrNavigation.DescripcionSr,
                    Url = h.IdSrNavigation.UrlSr,
                    CantidadCapitulos = h.IdSrNavigation.CantCapitulosSr,
                    Capitulos = h.IdSrNavigation.CapituloSerie.Select(c => new CapituloDTO
                    {
                        IdCp = c.IdCp,
                        NombreCp = c.NombreCp,
                        DescripcionCp = c.DescripcionCp,
                        DuracionCp = c.DuracionCp,
                        UrlCp = c.UrlCp,
                        Portada = c.PortadaCp,
                        IdSr = c.IdSr,
                        Temporada = int.Parse(c.TemporadaCp)

                    }).ToList()
                })
                .ToListAsync();

            return historialSeries;
        }

        public async Task<bool> DeleteHistorialSerie(int userId, int serieId)
        {
            var existingHistorialSerie = await context.HistorialSeries.FindAsync(serieId, userId);
            if (existingHistorialSerie == null)
            {
                return false;
            }

            context.HistorialSeries.Remove(existingHistorialSerie);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<SerieDTO>> GetRecomendaciones(int idUsr)
        {
            // Obtén el historial de series del usuario
            var historialUsuario = await context.HistorialSeries
                .Where(h => h.IdUsr == idUsr)
                .Select(h => h.IdSr)
                .ToListAsync();

            // Encuentra otros usuarios que hayan visto las mismas series
            var usuariosSimilares = await context.HistorialSeries
                .Where(h => h.IdUsr != idUsr && historialUsuario.Contains(h.IdSr))
                .Select(h => h.IdUsr)
                .Distinct()
                .ToListAsync();

            // Recomendar series vistas por usuarios similares pero no vistas por el usuario actual
            var recomendaciones = await context.HistorialSeries
                .Where(h => usuariosSimilares.Contains(h.IdUsr) && !historialUsuario.Contains(h.IdSr))
                .Select(h => new SerieDTO
                {
                    Id = h.IdSr,
                    Nombre = h.IdSrNavigation.NombreSr,
                    Portada = h.IdSrNavigation.PortadaSr,
                    Descripcion = h.IdSrNavigation.DescripcionSr,
                    Url = h.IdSrNavigation.UrlSr,
                    CantidadCapitulos = h.IdSrNavigation.CantCapitulosSr,
                    Capitulos = h.IdSrNavigation.CapituloSerie.Select(c => new CapituloDTO
                    {
                        IdCp = c.IdCp,
                        NombreCp = c.NombreCp,
                        DescripcionCp = c.DescripcionCp,
                        DuracionCp = c.DuracionCp,
                        UrlCp = c.UrlCp,
                        Portada = c.PortadaCp,
                        IdSr = c.IdSr,
                        Temporada = int.Parse(c.TemporadaCp)

                    }).ToList()
                })
                .ToListAsync();

            return recomendaciones;
        }
    }
}
