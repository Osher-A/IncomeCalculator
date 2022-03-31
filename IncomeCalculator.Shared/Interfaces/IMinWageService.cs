using IncomeCalculator.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncomeCalculator.Shared.Interfaces
{
    public interface IMinWageService
    {
        List<MinWage> MinWages { get; set; }

        bool CanAddMinWage(MinWage dtoMinWage);
        Task<MinWage> GetMinWage(int age, DateTime taxYear);
    }
}