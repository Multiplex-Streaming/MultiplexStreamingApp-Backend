﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Multiplex.Business.DTOs;
using Multiplex.Business.Helpers;
using Multiplex.Business.Interfaces;
using Multiplex.Domain.Contexts.AutoGenerated;
using Multiplex.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multiplex.Business.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly MultiplexContext context;
        private readonly ILogger logger;
        private readonly IConfiguration _configuration;
        private string _seriesTemp = "SeriesTemp";

        public SeriesService(MultiplexContext context, ILogger<UsuariosService> logger, IConfiguration configuration)
        {
            this.context = context;
            this.logger = logger;
            _configuration = configuration;
        }

        public async Task<List<SerieDTO>> GetSeries()
        {
            var seriesEntities = await context.Series
            .Include(serie => serie.CapituloSerie)
            .ToListAsync();

            var seriesDTO = seriesEntities.Select(serieEntity => new SerieDTO
            {
                Id = serieEntity.IdSr,
                Nombre = serieEntity.NombreSr,
                Descripcion = serieEntity.DescripcionSr,
                CantidadCapitulos = serieEntity.CantCapitulosSr,
                Url = serieEntity.UrlSr,
                Capitulos = serieEntity.CapituloSerie.Select(capituloEntity => new CapituloDTO
                {
                    IdSr = capituloEntity.IdSr,
                    IdCp = capituloEntity.IdCp,
                    NombreCp = capituloEntity.NombreCp,
                    DescripcionCp = capituloEntity.DescripcionCp,
                    DuracionCp = capituloEntity.DuracionCp,
                    Temporada = int.Parse(capituloEntity.TemporadaCp),
                    UrlCp = capituloEntity.UrlCp
                }).ToList(),
            }).ToList();

            return seriesDTO;
        }

        public async Task<SerieDTO> GetSerie(int IdSr)
        {
            var serieEntity = await context.Series
            .Include(serie => serie.CapituloSerie)
            .FirstOrDefaultAsync(serie => serie.IdSr == IdSr);

            if (serieEntity == null)
            {
                throw new Exception("La serie no existe");
            }

            var serieDTO = new SerieDTO
            {
                Id = serieEntity.IdSr,
                Nombre = serieEntity.NombreSr,
                Descripcion = serieEntity.DescripcionSr,
                CantidadCapitulos = serieEntity.CantCapitulosSr,
                Url = serieEntity.UrlSr,
                Portada = serieEntity.PortadaSr,
                Capitulos = serieEntity.CapituloSerie.Select(capituloEntity => new CapituloDTO
                {
                    IdSr = capituloEntity.IdSr,
                    IdCp = capituloEntity.IdCp,
                    NombreCp = capituloEntity.NombreCp,
                    DescripcionCp = capituloEntity.DescripcionCp,
                    DuracionCp = capituloEntity.DuracionCp,
                    UrlCp = capituloEntity.UrlCp,
                    Portada = capituloEntity.PortadaCp
                }).ToList(),
            };

            return serieDTO;

        }

        public async Task<bool> CreateSerie(SerieDTO serie)
        {
            try
            {
                long maxAllowedContentLength = _configuration.GetValue<long>("RequestLimits:MaxAllowedContentLength");

                if (serie.file?.Length > maxAllowedContentLength)
                    throw new Exception("El tamaño del archivo supera el límite permitido");

                if (serie.portadaFile?.Length > maxAllowedContentLength)
                    throw new Exception("El tamaño del archivo supera el límite permitido");

                string tempFileName = "", tempPortadaFileName = "";

                if (serie.file != null)
                    tempFileName = await ArchivosHelper.GuardarArchivo(serie.file, _seriesTemp);
                if (serie.portadaFile != null)
                    tempPortadaFileName = await ArchivosHelper.GuardarArchivo(serie.portadaFile, _seriesTemp);


                // Crear la entidad de la serie en la base de datos
                var serieEntity = new Series()
                {
                    NombreSr = serie.Nombre,
                    DescripcionSr = serie.Descripcion,
                    CantCapitulosSr = serie.CantidadCapitulos,
                    PortadaSr = tempPortadaFileName,
                    UrlSr = tempFileName,
                };

                context.Series.Add(serieEntity);
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            
        }

        public async Task<bool> DeleteSerie(int IdSr)
        {
            var serieEntity = await context.Series.Include(s => s.CapituloSerie)
                .Include(s => s.HistorialSeries)
                .Include(s => s.FavoritosSeries)
                .FirstOrDefaultAsync(s => s.IdSr == IdSr);

            if (serieEntity == null)
            {
                return false; // Serie not found
            }

            // Delete associated files from SeriesTemp folder
            string tempFolderPath = Path.Combine(Path.GetTempPath(), "SeriesTemp");
            foreach (var capitulo in serieEntity.CapituloSerie)
            {
                string tempFilePath = Path.Combine(tempFolderPath, capitulo.UrlCp);
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }

            // Remove the serie and associated capítulos from context
            context.CapituloSerie.RemoveRange(serieEntity.CapituloSerie);
            context.Series.Remove(serieEntity);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSerie(SerieDTO serie)
        {
            var serieEntity = await context.Series.Include(s => s.CapituloSerie)
                                                  .FirstOrDefaultAsync(s => s.IdSr == serie.Id);

            if (serieEntity == null)
            {
                return false; // Serie not found
            }

            long maxAllowedContentLength = _configuration.GetValue<long>("RequestLimits:MaxAllowedContentLength");

            string tempPortadaFileName = "";

            if (serie.portadaFile != null)
            {
                await ArchivosHelper.BorrarArchivo(serie.Portada, _seriesTemp);
                tempPortadaFileName = await ArchivosHelper.GuardarArchivo(serie.portadaFile, _seriesTemp);
            }

            // Update serie properties
            serieEntity.NombreSr = serie.Nombre;
            serieEntity.DescripcionSr = serie.Descripcion;
            serieEntity.CantCapitulosSr = serie.CantidadCapitulos;
            serieEntity.UrlSr = serie.Url;
            serieEntity.PortadaSr = tempPortadaFileName;

            context.Series.Update(serieEntity);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<FileStream> GetSerieFile(int idCap)
        {
            var capitulo = await context.CapituloSerie.Where(x => x.IdCp == idCap)
                .Select(x => new CapituloDTO()
                {
                    UrlCp = x.UrlCp
                })
                .FirstOrDefaultAsync();
            if (!File.Exists(capitulo.UrlCp))
                throw new Exception("No se encontró la película");

            return new FileStream(capitulo.UrlCp, FileMode.Open);
        }

        public async Task<FileStream> GetSeriePortada(int SrId)
        {
            var portadaSr = await context.Series.Where(x => x.IdSr == SrId)
                .Select(x => x.PortadaSr)
                .FirstOrDefaultAsync();
            if (!File.Exists(portadaSr))
                throw new Exception("No se encontró la serie");

            return new FileStream(portadaSr, FileMode.Open);
        }

        //Capitlos logica>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public async Task<bool> CreateCapitulo(CapituloDTO capitulo)
        {
            //Verificar que la serie exista
            var serie = await GetSerie(capitulo.IdSr);
            if(serie == null)
                return false;

            long maxAllowedContentLength = _configuration.GetValue<long>("RequestLimits:MaxAllowedContentLength");

            if (capitulo.file?.Length > maxAllowedContentLength)
                throw new Exception("El tamaño del archivo supera el límite permitido");

            if (capitulo.portadaFile?.Length > maxAllowedContentLength)
                throw new Exception("El tamaño del archivo supera el límite permitido");
            
            string tempFileName = "", tempPortadaFileName = "";

            if (capitulo.file != null)
                tempFileName = await ArchivosHelper.GuardarArchivo(capitulo.file, _seriesTemp);
            if (capitulo.portadaFile != null)
                tempPortadaFileName = await ArchivosHelper.GuardarArchivo(capitulo.portadaFile, _seriesTemp);

            //Crear la entidad del capitulo en la bd
            var newCapitulo = new CapituloSerie
            {
                IdSr = capitulo.IdSr,
                //IdCp = capitulo.IdCp,
                NombreCp = capitulo.NombreCp,
                DescripcionCp = capitulo.DescripcionCp,
                DuracionCp = capitulo.DuracionCp,
                TemporadaCp = capitulo.Temporada.ToString(),
                UrlCp = tempFileName,
                PortadaCp = tempPortadaFileName,
            };

            context.CapituloSerie.Add(newCapitulo);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCapitulo(CapituloDTO capitulo)
        {
            var capituloDb = await context.CapituloSerie.Where(x => x.IdCp == capitulo.IdCp).FirstOrDefaultAsync();
            if (capituloDb == null)
                return false;

            long maxAllowedContentLength = _configuration.GetValue<long>("RequestLimits:MaxAllowedContentLength");

            //Update file
            if (capitulo.file != null)
            {
                if (capitulo.file?.Length > maxAllowedContentLength)
                    throw new Exception("El tamaño del archivo supera el límite permitido");

                await ArchivosHelper.BorrarArchivo(capituloDb.UrlCp, _seriesTemp);
                capituloDb.UrlCp = await ArchivosHelper.GuardarArchivo(capitulo.file, _seriesTemp);
            }
            //Update portada
            if (capitulo.portadaFile != null)
            {
                if (capitulo.portadaFile?.Length > maxAllowedContentLength)
                    throw new Exception("El tamaño del archivo supera el límite permitido");

                await ArchivosHelper.BorrarArchivo(capituloDb.PortadaCp, _seriesTemp);
                capituloDb.PortadaCp = await ArchivosHelper.GuardarArchivo(capitulo.portadaFile, _seriesTemp);
            }
            //Update other properties
            capituloDb.NombreCp = capitulo.NombreCp;
            capituloDb.DescripcionCp = capitulo.DescripcionCp;
            capituloDb.DuracionCp = capitulo.DuracionCp;
            capituloDb.TemporadaCp = capitulo.Temporada.ToString();

            context.CapituloSerie.Update(capituloDb);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCapitulo(int cpId)
        {
            var capituloDb = await context.CapituloSerie.Where(x => x.IdCp == cpId).FirstOrDefaultAsync();
            if (capituloDb == null)
                return false;

            // Delete associated file from PeliculasTemp folder
            string tempFolderPath = Path.Combine(Path.GetTempPath(), "SeriesTemp");
            string tempFilePath = Path.Combine(tempFolderPath, capituloDb.UrlCp);
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }

            context.CapituloSerie.Remove(capituloDb);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<FileStream> GetCapituloPortada(int cpId)
        {
            var portadaCp = await context.CapituloSerie.Where(x => x.IdCp == cpId)
                .Select(x => x.PortadaCp).FirstOrDefaultAsync();
            if (!File.Exists(portadaCp))
                throw new Exception("No se encontró el capitulo");

            return new FileStream(portadaCp, FileMode.Open);
        }
    }
}
