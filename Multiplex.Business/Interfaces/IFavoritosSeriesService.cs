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
        Task<string> CreateFavoritosSeries(FavoritosSeriesDTO favoritosSeries);
        Task<bool> DeleteFavoritosSeries(FavoritosSeriesDTO favoritosSeries);
        Task<IEnumerable<SerieDTO>> GetFavoritosSeries(int idUsr);
    }
}
