﻿using Multiplex.Business.DTOs;
using Multiplex.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multiplex.Domain.Contexts.AutoGenerated;

namespace Multiplex.Business.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly MultiplexContext context;
        private readonly ILogger logger;
        public UsuariosService(MultiplexContext context, ILogger<UsuariosService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<List<AbonadosDTO>> GetAbonadosPendientes() => await context.Usuarios.Where(x => x.IdEcNavigation.DescripcionEc.Equals("Pendiente")
        && x.IdTcNavigation.DescripcionTc.Equals("Abonados"))
            .Select(x => new AbonadosDTO() 
            { Name = $"{x.ApellidoUsr} {x.NombreUsr}" })
            .ToListAsync();

        public UserInfoDTO UserExists(string userMail, string userPass) =>
            context.Usuarios.Where(x => x.CorreoUsr.Equals(userMail) && x.PasswordUsr.Equals(userPass))
            .Select(x => new UserInfoDTO()
            {
                Id = x.IdUsr,
                UserName = $"{x.ApellidoUsr} {x.NombreUsr}",
                IsAdmin = x.IdTcNavigation.DescripcionTc.Equals("Administrador")
            })
            .FirstOrDefault();
    }
}
