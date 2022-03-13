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
        private IMinWageRepository _minWageRepository;
        public List<MinWage> MinWages { get; set; }
        public MinWageService(IMinWageRepository minWageRepository)
        {
            _minWageRepository = minWageRepository;
        }

        public async Task<MinWage> GetMinWage(int age, DateTime taxYear)
        {
             SetMinWages();

            return await Task.Run(() => MinWages.Where(mw => mw.TaxYear.Year == taxYear.Year && mw.Age <= age)
                .OrderByDescending(mw => mw.Wage)
                .First());
        }

        private void SetMinWages()
        {
            MinWages = _minWageRepository.GetMinWages();
        }
    }
}
