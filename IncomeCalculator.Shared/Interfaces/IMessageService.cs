using IncomeCalculator.Shared.Enums;
using System.Threading.Tasks;

namespace IncomeCalculator.Shared.Interfaces
{
    public interface IMessageService
    {
        Task TostrAlert(MessageType type, string message);
        Task SweetAlert(string heading, string message);
    }
}
