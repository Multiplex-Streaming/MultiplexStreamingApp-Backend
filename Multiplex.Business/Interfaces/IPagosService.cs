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
        Task<int> AddPagoAsync(PagoDTO pago);
        Task<bool> UpdatePagoAsync(int id, PagoDTO pago);
    }
}
