using Multiplex.Business.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface IPeliculasService
    {
        Task<List<PeliculaDTO>> GetPeliculas();
        Task<bool> CreatePelicula(PeliculaDTO pelicula);
        Task<bool> UpdatePelicula(PeliculaDTO pelicula);
        Task<bool> DeletePelicula(int plId);
        Task<PeliculaDTO> GetPelicula(int plId);
        Task<List<PeliculaDTO>> GetPeliculasPorGenero(int generoId);
        Task<FileStream> GetPeliculaFile(int plId);
        Task<FileStream> GetPeliculaPortada(int plId);
    }
}
