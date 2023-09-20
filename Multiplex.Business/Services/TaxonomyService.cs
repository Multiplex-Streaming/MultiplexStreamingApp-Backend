﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Multiplex.Business.DTOs;
using Multiplex.Business.Interfaces;
using Multiplex.Domain.Contexts.AutoGenerated;
using Multiplex.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multiplex.Business.Services
{
    public class TaxonomyService : ITaxonomyService
    {
        private readonly MultiplexContext context;
        private readonly ILogger logger;
        public TaxonomyService(MultiplexContext context, ILogger<UsuariosService> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task<List<GeneroDTO>> GetGeneros()
        {
            return await context.Generos.Select(x => new GeneroDTO()
            {
                Descripcion = x.DescripcionGn,
                Id = x.IdGn
            }).ToListAsync();
        }
        public async Task<bool> SaveGenero(GeneroDTO genero)
        {
            context.Add(new Generos()
            {
                DescripcionGn = genero.Descripcion
            });
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<List<CapituloDTO>> GetCapitulos()
        {
            return await context.CapituloSerie.Select(x => new CapituloDTO()
            {
                IdSr = x.IdSr,
                IdCp = x.IdCp,
                NombreCp = x.NombreCp,
                DescripcionCp = x.DescripcionCp,
                DuracionCp = x.DuracionCp,
                UrlCp = x.UrlCp,
            }).ToListAsync();
        }
    }
}
