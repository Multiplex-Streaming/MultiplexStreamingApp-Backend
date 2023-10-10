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
        Task<bool> CreateHistorialPelicula(HistorialPeliculasDTO historialPelicula);
        Task<IEnumerable<HistorialPeliculasDTO>> GetHistorialPeliculas(int idUsr);
        Task<HistorialPeliculasDTO> UpdateHistorialPelicula(HistorialPeliculasDTO historialPelicula);

    }
}
