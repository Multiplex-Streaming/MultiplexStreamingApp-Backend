using Multiplex.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface IHistorialSeriesService
    {
        Task<string> CreateHistorialSerie(int userId, int serieId);
        Task<IEnumerable<SerieDTO>> GetHistorialSeries(int idUsr);
        Task<bool> DeleteHistorialSerie(int idUsr, int serieId);
        Task<IEnumerable<SerieDTO>> GetRecomendaciones(int idUsr);
    }
}
