using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace C42G01Demo.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UplodeFile(IFormFile file,string folderName)
        {
            //1. Get located FolderPath
            string FolderPath = Directory.GetCurrentDirectory() + "wwwroot\\Files" + folderName;

            //2. Get fileName and make it unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. Get filePath [FolderPath +fileName]
            string filePath = Path.Combine(FolderPath, fileName);

            //4. save file as streams
           using var filestream = new FileStream(filePath,FileMode.Create);
            file.CopyTo(filestream);

            return fileName;

        }
        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

}
