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
        Task<string> CreateHistorialPelicula(HistorialPeliculasDTO historialPelicula);
        Task<IEnumerable<PeliculaDTO>> GetHistorialPeliculas(int idUsr);
        Task<bool> DeleteHistorialPelicula(HistorialPeliculasDTO historialPelicula);
        Task<IEnumerable<PeliculaDTO>> GetRecomendaciones(int idUsr);
    }
}
