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
        Task<bool> UpdateHistorial(int idUser, int idPl, int minutos, int segundos);
        Task<HistorialPeliculasDTO> GetHistorialPelicula(int idUsr, int idPl);
    }
}
