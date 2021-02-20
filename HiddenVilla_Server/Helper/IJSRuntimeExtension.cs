// HiddenVilla_Server.Helper.IJSRuntimeExtension.cs
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace HiddenVilla_Server.Helper
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime JsRuntime, string message)
        {
            await JsRuntime.InvokeVoidAsync("ShowToastr", "success", message);
        }
        public static async ValueTask ToastrError(this IJSRuntime JsRuntime, string message)
        {
            await JsRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }

        public static async ValueTask SweetAlertSuccess(this IJSRuntime JsRuntime, string message)
        {
            await JsRuntime.InvokeVoidAsync("ShowSweetAlert", "success", message);
        }
        public static async ValueTask SweetAlertError(this IJSRuntime JsRuntime, string message)
        {
            await JsRuntime.InvokeVoidAsync("ShowSweetAlert", "error", message);
        }

        //better version of using SweetAlert (ie. ditect JS call to Swal.fire) and Confirm Dialog
        public static async ValueTask DisplayMessage(this IJSRuntime JsRuntime, string message)
        {
            await JsRuntime.InvokeVoidAsync("Swal.fire", message);
        }

        public static async ValueTask DisplayMessage(this IJSRuntime JsRuntime, string title, string message, SweetAlertType sweetAlertType)
        {
            await JsRuntime.InvokeVoidAsync("Swal.fire", title, message, sweetAlertType.ToString());
        }

        public static async ValueTask<bool> DisplayConfirmDialog(this IJSRuntime JsRuntime, string title, string message, SweetAlertType sweetAlertType)
        {
            return await JsRuntime.InvokeAsync<bool>("CustomConfirmDialog", title, message, sweetAlertType.ToString());
        }
    }

    public enum SweetAlertType
    {
        warning, error, success, info, question
    }
}
