using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FunOlympicDataManager.Library.Helper;
public class ImageUpload
{
    public ImageUpload()
    {
    }
    public async Task<string> UploadedFile(IFormFile photo, string memberNO, string type)
    {
        string uniqueFileName = null;
        string returnfilePath = "";
        try
        {
            if (photo != null)
            {
                string uploadsFolder = Path.Combine("Resources", "Library");
                string typepath = Path.Combine(uploadsFolder, type);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), typepath);
                if (!Directory.Exists(pathToSave))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(pathToSave);
                }
                var fileName = Path.GetExtension(ContentDispositionHeaderValue.Parse(photo.ContentDisposition).FileName).Trim('"');
                uniqueFileName = Guid.NewGuid().ToString() + "_" + memberNO + fileName;
                string filePath = Path.Combine(pathToSave, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
                returnfilePath = $"Images/{type}/{uniqueFileName}";
            }
            return returnfilePath;
        }
        catch
        {
            return null;
        }
    }

}