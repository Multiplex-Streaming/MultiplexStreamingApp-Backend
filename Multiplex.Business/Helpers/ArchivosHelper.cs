﻿using Microsoft.AspNetCore.Http;
using Multiplex.Business.DTOs;
using Multiplex.Business.Enums;
using Multiplex.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Helpers
{
    public static class ArchivosHelper
    {
        public static async Task<bool> BorrarArchivo(string url)
        {
            if(string.IsNullOrEmpty(url)) return false;
            try
            {
                var tempFolderPath = Path.Combine(Path.GetTempPath(), "PeliculasTemp");
                var prevTempFilePath = Path.Combine(tempFolderPath, url);
                if (File.Exists(prevTempFilePath))
                    File.Delete(prevTempFilePath);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static async Task<string> GuardarArchivo(IFormFile file)
        {
            if (file != null) return ErrorMessages.NoGraboArchivo;

            try
            {
                var tempFolderPath = Path.Combine(Path.GetTempPath(), "PeliculasTemp");
                if (!Directory.Exists(tempFolderPath))
                    Directory.CreateDirectory(tempFolderPath);

                var tempFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var tempFilePath = Path.Combine(tempFolderPath, tempFileName);
                using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                    await file.CopyToAsync(fileStream);

                return tempFilePath;
            }
            catch (Exception ex)
            {
                return ErrorMessages.NoGraboArchivo;
            }            
        }
    }
}
