using IncomeCalculator.DAL;
using IncomeCalculator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncomeCalculator.Services
{
    public class MinWageService
    {
        private MinWageRepository _minWageRepository;
        public List<MinWage> MinWages { get; set; }
        public  MinWageService(MinWageRepository minWageRepository)
        {
            _minWageRepository = minWageRepository;
            SetMinWages();
        }

        public MinWage GetMinWage(int age, DateTime taxYear)
        {
            return MinWages.Where(mw => mw.TaxYear.Year == taxYear.Year && mw.Age <= age).FirstOrDefault();
        }

        private async void SetMinWages()
        {
            MinWages = await _minWageRepository.GetMinWagesAsync();
        }
    }
}
