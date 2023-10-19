﻿using Multiplex.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface IHistorialSeriesService
    {
        Task<string> CreateHistorialSerie(HistorialSeriesDTO historialSerie);
        Task<IEnumerable<SerieDTO>> GetHistorialSeries(int idUsr);
        Task<bool> DeleteHistorialSerie(HistorialSeriesDTO historialSerie);
        Task<IEnumerable<SerieDTO>> GetRecomendaciones(int idUsr);
    }
}