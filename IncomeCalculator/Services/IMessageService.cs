using System.Threading.Tasks;

namespace IncomeCalculator.Services
{
    public interface IMessageService
    {
        Task TostrAlert(object jsRuntime, string type, string message); 
        Task SweetAlert(object jsRuntime, string heading, string message);
    }
}
