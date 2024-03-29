﻿using IncomeCalculator.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncomeCalculator.DAL
{
    public interface IMinWageRepository
    {
        void AddMinWage(MinWage minWage);
        List<MinWage> GetAllMinWages();
        Task<List<MinWage>> GetAllMinWagesAsync();
    }
}