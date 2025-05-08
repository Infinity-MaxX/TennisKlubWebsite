using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace TennisWebsite.ClassLibrary.Helpers
{
    public class FileManager
    {
        public static string SaveImageFile(IFormFile file, IWebHostEnvironment webHostEnvironment)
        {
            if (file == null) throw new NullReferenceException("No file to save");
            string destinationfolder = Path.Combine(webHostEnvironment.WebRootPath, "images/memberimages");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(destinationfolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}
