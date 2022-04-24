using System.Collections.Generic;
using System.Threading.Tasks;
using IncomeCalculator.Shared.DTO;

namespace IncomeCalculator.Shared.Interfaces
{
    public interface IMinWageRepository
    {
        void AddMinWage(MinWage minWage);
        List<MinWage> GetAllMinWages();
        Task<List<MinWage>> GetAllMinWagesAsync();
    }
}