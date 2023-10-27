using Multiplex.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface IHistorialPeliculasService
    {
        Task<string> CreateHistorialPelicula(int userId, int peliculaId);
        Task<IEnumerable<PeliculaDTO>> GetHistorialPeliculas(int idUsr);
        Task<bool> DeleteHistorialPelicula(int peliculaId, int idUsr);
        Task<IEnumerable<PeliculaDTO>> GetRecomendaciones(int idUsr);
    }
}
