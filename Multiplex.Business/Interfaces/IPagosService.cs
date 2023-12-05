using Multiplex.Business.DTOs;
using Multiplex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface IPagosService
    {
        Task<int> AddPagoAsync(int abonadoId, CrearPagoDTO pago);
        Task<bool> UpdatePagoAsync(int abonadoId, PagoDTO pago);
        Task<bool> NotificarAbonadosConPagosPendientes();
    }
}
