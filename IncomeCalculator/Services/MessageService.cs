using IncomeCalculator.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace IncomeCalculator.Services
{
    public  class MessageService : IMessageService
    {
        private IJSRuntime _jsRuntime;
        public MessageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        public async Task SweetAlert(string heading, string message)
        {
            await _jsRuntime.InvokeVoidAsync("ShowSwal", heading, message);
        }

        public async Task TostrAlert(MessageType type, string message)
        {
            await _jsRuntime.InvokeVoidAsync("ShowToastr", type.ToString(), message);
        }
       
    }
}
