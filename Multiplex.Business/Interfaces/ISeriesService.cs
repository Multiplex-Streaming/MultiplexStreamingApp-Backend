using Multiplex.Business.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface ISeriesService
    {
        Task<List<SerieDTO>> GetSeries();
        Task<bool> CreateSerie(SerieDTO serie);
        Task<bool> UpdateSerie(SerieDTO serie);
        Task<bool> DeleteSerie(int IdSr);
        Task<SerieDTO> GetSerie(int IdSr);
        Task<FileStream> GetSerieFile(string url);
    }
}
