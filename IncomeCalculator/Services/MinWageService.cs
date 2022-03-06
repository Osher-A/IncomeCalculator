using IncomeCalculator.DAL;
using IncomeCalculator.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IncomeCalculator.Services
{
    public class MinWageService
    {
        private MinWagePersistence _minWageRepository;
        public List<MinWage> MinWages { get; set; }
        public MinWageService(MinWagePersistence minWageRepository)
        {
            _minWageRepository = minWageRepository;
            SetMinWages();
        }

        public MinWage GetMinWage(int age, DateTime taxYear)
        {
            return MinWages.Where(mw => mw.TaxYear.Year == taxYear.Year && mw.Age <= age)
                .OrderByDescending(mw => mw.Wage)
                .First();
        }

        private async void SetMinWages()
        {
            MinWages = await _minWageRepository.GetMinWagesAsync();
        }
    }
}
