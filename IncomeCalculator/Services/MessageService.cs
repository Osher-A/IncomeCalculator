using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace IncomeCalculator.Services
{
    public  class MessageService : IMessageService
    {
        public async Task SweetAlert(object jSRuntime, string heading, string message)
        {
            await ((JSRuntime)jSRuntime).InvokeVoidAsync("ShowSwal", heading, message);
        }

        public async Task TostrAlert(object jSRuntime, string type, string message)
        {
            JSRuntime js = jSRuntime as JSRuntime;
            await js.InvokeVoidAsync("ShowToastr", type, message);
        }
       
    }
}
