// HiddenVilla_Server.Service.IService.IFileUpload.cs
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace HiddenVilla_Server.Service.IService
{
    public interface IFileUpload
    {
        public Task<string> UploadFile(IBrowserFile file);

        public bool DeleteFile(string fileName);
    }
}
