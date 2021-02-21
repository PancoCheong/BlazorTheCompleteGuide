// HiddenVilla_Server.Service.FileUpload.cs
using HiddenVilla_Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HiddenVilla_Server.Service
{
    public class FileUpload : IFileUpload
    {
        //use code to create folder
        private readonly IWebHostEnvironment _webHostEnvironment;

        // use DI to inject the Web Host Environment
        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public bool DeleteFile(string fileName) //question: why the path is not from DB?
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\RoomImages\\{fileName}";
                if(File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.Name);
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension; //rename file
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\RoomImages";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "RoomImages", fileName);

                //Copy the uploaded data stream into memory stream
                var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);

                //create folder if not existing
                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                //Write memory stream to file
                await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write)) 
                {
                    memoryStream.WriteTo(fs);
                }

                var fullPath = $"RoomImages/{fileName}"; //not include wwwroot
                return fullPath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
