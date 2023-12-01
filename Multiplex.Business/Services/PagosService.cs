﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class PagosService : IPagosService
    {
        private readonly MultiplexContext context;
        private readonly ILogger<PagosService> logger;

        public PagosService(MultiplexContext context, ILogger<PagosService> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task<int> AddPagoAsync(PagoDTO pago)
        {
            // Obtener el usuario correspondiente
            var usuario = await context.Usuarios.FindAsync(pago.IdUsuario);

            if (usuario == null)
            {
                throw new InvalidOperationException($"Usuario con ID {pago.IdUsuario} no encontrado.");
            }

            //mappera el pagoDto a un pago
            Pagos newPago = new Pagos();
            newPago.Total = pago.Total;
            newPago.FechaPago = pago.FechaPago;
            newPago.IsPagado = pago.IsPagado;

            //guardar el pago y obtener el ID
            var pagoId = await context.Pagos.AddAsync(newPago);
            await context.SaveChangesAsync();

            UsuariosPagos usuariosPagos = new UsuariosPagos();
            usuariosPagos.IdUsr = pago.IdUsuario;
            usuariosPagos.IdPago = pagoId.Entity.IdPago;

            // Agregar el pago al usuario
            context.UsuarioPagos.Add(usuariosPagos);
            await context.SaveChangesAsync();

            return pagoId.Entity.IdPago;
        }

        public async Task<bool> UpdatePagoAsync(int id, PagoDTO pago)
        {
            try
            {
                var pagoToUpdate = await context.Pagos.FindAsync(id);

                if (pagoToUpdate == null)
                {
                    throw new InvalidOperationException($"Pago con ID {id} no encontrado.");
                }

                // Recargar la entidad desde la base de datos
                context.Entry(pagoToUpdate).Reload();

                // Verificar si la entidad todavía existe después de recargar
                if (pagoToUpdate == null)
                {
                    throw new InvalidOperationException($"Pago con ID {id} no encontrado después de recargar.");
                }

                // Actualizar la entidad
                pagoToUpdate.Total = pago.Total;
                pagoToUpdate.IsPagado = pago.IsPagado;
                pagoToUpdate.FechaPago = pago.FechaPago;

                // Configurar el estado de la entidad existente como modificado
                context.Entry(pagoToUpdate).State = EntityState.Modified;

                // Guardar cambios
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Error al actualizar el pago con ID {id}.");
                return false;
            }
        }

    }
}
