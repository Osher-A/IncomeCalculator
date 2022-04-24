using IncomeCalculator.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncomeCalculator.Shared.Interfaces
{
    public interface IMinWageService
    {
        List<MinWage> GetAllMinWages();

        Task AddMinWageAsync(MinWage dtoMinWage);
        Task<MinWage> GetMinWageAsync(int age, DateTime taxYear);
    }
}