using Multiplex.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface IFavoritosPeliculaService
    {
        Task<string> CreateFavoritosPelicula(int userId, int peliculaId);
        Task<bool> DeleteFavoritosPelicula(int userId, int peliculaId);
        Task<IEnumerable<PeliculaDTO>> GetFavoritosPelicula(int idUsr);
    }
}
