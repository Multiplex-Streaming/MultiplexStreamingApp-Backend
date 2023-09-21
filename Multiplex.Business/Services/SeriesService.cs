﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Multiplex.Business.DTOs;
using Multiplex.Business.Interfaces;
using Multiplex.Domain.Contexts.AutoGenerated;
using Multiplex.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly MultiplexContext context;
        private readonly ILogger logger;
        private readonly IConfiguration _configuration;

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
                Portada = serieEntity.PortadaSr,
                Capitulos = serieEntity.CapituloSerie.Select(capituloEntity => new CapituloDTO
                {
                    IdSr = capituloEntity.IdSr,
                    IdCp = capituloEntity.IdCp,
                    NombreCp = capituloEntity.NombreCp,
                    DescripcionCp = capituloEntity.DescripcionCp,
                    DuracionCp = capituloEntity.DuracionCp,
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
            long maxAllowedContentLength = _configuration.GetValue<long>("RequestLimits:MaxAllowedContentLength");

            foreach (var file in serie.files)
            {
                if (file.Length > maxAllowedContentLength)
                {
                    throw new Exception("El tamaño de uno o más archivos supera el límite permitido");
                }
            }

            // Guardar los archivos en una carpeta temporal
            string tempFolderPath = Path.Combine(Path.GetTempPath(), "SeriesTemp");
            if (!Directory.Exists(tempFolderPath))
            {
                Directory.CreateDirectory(tempFolderPath);
            }

            var tempFileNames = new List<string>();

            foreach (var file in serie.files)
            {
                string tempFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string tempFilePath = Path.Combine(tempFolderPath, tempFileName);
                using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                tempFileNames.Add(tempFileName);
            }

            // Crear la entidad de la serie en la base de datos
            var serieEntity = new Series()
            {
                IdSr = serie.Id,
                NombreSr = serie.Nombre,
                DescripcionSr = serie.Descripcion,
                CantCapitulosSr = serie.CantidadCapitulos,
                PortadaSr = serie.Portada,
                UrlSr = serie.Url,
            };

            context.Series.Add(serieEntity);
            await context.SaveChangesAsync();

            // Crear las entidades de los capítulos y asociarlos a la serie
            for (int i = 0; i < serie.Capitulos.Count; i++)
            {
                var capituloDto = serie.Capitulos[i];
                var capituloEntity = new CapituloSerie()
                {
                    IdSr = serieEntity.IdSr,
                    IdCp = capituloDto.IdCp,
                    NombreCp = capituloDto.NombreCp,
                    DescripcionCp = capituloDto.DescripcionCp,
                    DuracionCp = capituloDto.DuracionCp,
                    UrlCp = tempFileNames[i]
                };

                context.CapituloSerie.Add(capituloEntity);
            }

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteSerie(int IdSr)
        {
            var serieEntity = await context.Series.Include(s => s.CapituloSerie)
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

            foreach (var file in serie.files)
            {
                if (file.Length > maxAllowedContentLength)
                {
                    throw new Exception("El tamaño de uno o más archivos supera el límite permitido");
                }
            }

            // Guardar los archivos en una carpeta temporal
            string tempFolderPath = Path.Combine(Path.GetTempPath(), "SeriesTemp");
            if (!Directory.Exists(tempFolderPath))
            {
                Directory.CreateDirectory(tempFolderPath);
            }

            // Update serie properties
            serieEntity.NombreSr = serie.Nombre;
            serieEntity.DescripcionSr = serie.Descripcion;
            serieEntity.CantCapitulosSr = serie.CantidadCapitulos;
            serieEntity.UrlSr = serie.Url;
            serieEntity.PortadaSr = serie.Portada;
            // Update capítulos
            for (int i = 0; i < serie.Capitulos.Count; i++)
            {
                var capituloDto = serie.Capitulos[i];
                var capituloEntity = serieEntity.CapituloSerie.ElementAt(i);

                // Delete the previous file from SeriesTemp folder
                string prevTempFilePath = Path.Combine(tempFolderPath, capituloEntity.UrlCp);
                if (File.Exists(prevTempFilePath))
                {
                    File.Delete(prevTempFilePath);
                }

                // Save the new file to SeriesTemp folder
                string tempFileName = Guid.NewGuid().ToString() + Path.GetExtension(serie.files[i].FileName);
                string tempFilePath = Path.Combine(tempFolderPath, tempFileName);
                using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await serie.files[i].CopyToAsync(fileStream);
                }

                // Update capítulo properties
                capituloEntity.NombreCp = capituloDto.NombreCp;
                capituloEntity.DescripcionCp = capituloDto.DescripcionCp;
                capituloEntity.DuracionCp = capituloDto.DuracionCp;
                capituloEntity.UrlCp = tempFileName;
            }

            context.Series.Update(serieEntity);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<FileStream> GetSerieFile(string url)
        {
            string tempFolderPath = Path.Combine(Path.GetTempPath(), "SeriesTemp");
            string tempFilePath = Path.Combine(tempFolderPath, url);
            if (!File.Exists(tempFilePath))
            {
                throw new Exception("No se encontró la serie");
            }

            return new FileStream(tempFilePath, FileMode.Open);
        }
    }
}
