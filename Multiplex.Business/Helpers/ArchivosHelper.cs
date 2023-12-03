using Microsoft.AspNetCore.Http;
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
        private const string folder = "C:\\Users\\cesar\\source\\repos\\MultiplexStreamingApp-Frontend\\Multiplex-Streaming\\src\\assets\\";
        public static async Task<bool> BorrarArchivo(string fileName, string path)
        {
            if(string.IsNullOrEmpty(fileName)) return false;
            try
            {
                var tempFolderPath = Path.Combine(folder, path);
                var prevTempFilePath = Path.Combine(tempFolderPath, fileName);
                if (File.Exists(prevTempFilePath))
                    File.Delete(prevTempFilePath);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static async Task<string> GuardarArchivo(IFormFile file, string path)
        {
            if (file == null) return ErrorMessages.NoGraboArchivo;

            try
            {
                //Path.GetTempPath
                var tempFolderPath = Path.Combine(folder, path);
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
