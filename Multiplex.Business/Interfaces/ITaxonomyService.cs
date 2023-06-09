﻿using Multiplex.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface ITaxonomyService
    {
        Task<List<PeliculaDTO>> GetPeliculasPorGenero(int generoId);
    }
}
