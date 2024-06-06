using Microsoft.AspNetCore.Routing;

namespace PLayer.Helper
{
    public static class DocumentSetting
    {
        public static string UploadFile (IFormFile file,string folderName)
        {
            var pathFolder = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Files", folderName);

             var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";

            var filepath = Path.Combine(pathFolder, fileName);

            using var fileStream =new FileStream(filepath ,FileMode.Create);
            
            file.CopyTo(fileStream);

            return fileName;
        
        }
        public static void DeleteFile(string folderName ,string fileName)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileName);
            if (File.Exists(filepath))
            {
                File.Delete(filepath);

            }

        }
    }
}
