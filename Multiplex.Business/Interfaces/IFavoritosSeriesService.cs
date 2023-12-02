using Multiplex.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface IFavoritosSeriesService
    {
        Task<string> CreateFavoritosSeries(int userId, int serieId);
        Task<bool> DeleteFavoritosSeries(int userId, int serieId);
        Task<IEnumerable<SerieDTO>> GetFavoritosSeries(int idUsr);
    }
}
