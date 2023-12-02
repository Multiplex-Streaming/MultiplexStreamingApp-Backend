using Multiplex.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface IMetricasService
    {
        Task<IEnumerable<TopPeliculasDTO>> GetTopPeliculas();
        Task<IEnumerable<TopSeriesDTO>> GetTopSeries();
        Task<IEnumerable<(string Genero, int TotalVisualizaciones)>> GetGenerosMasVistosPeliculas();
        Task<IEnumerable<(string Genero, int TotalVisualizaciones)>> GetGenerosMasVistosSeries();
        Task<IEnumerable<(string Usuario, int TotalVisualizaciones)>> GetUsuariosMasVieronPeliculas();
        Task<IEnumerable<(string Usuario, int TotalVisualizaciones)>> GetUsuariosMasVieronSeries();
    }
}
