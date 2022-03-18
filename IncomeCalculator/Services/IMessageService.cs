using IncomeCalculator.Enums;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace IncomeCalculator.Services
{
    public interface IMessageService
    {
        Task TostrAlert(MessageType type, string message); 
        Task SweetAlert(string heading, string message);
    }
}
