using AutoMapper;
using IncomeCalculator.Data;
using IncomeCalculator.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncomeCalculator.DAL
{
    public class MinWageRepository : IMinWageRepository
    {
        private BenefitsContext _dbContext;
        public MinWageRepository(BenefitsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<MinWage> GetAllMinWages()
        {
                var result = _dbContext.MinWages
                    .OrderBy(mw => mw.TaxYear)
                    .OrderByDescending(mw => mw.Age).ToList();
                return result;
        }
        public async Task<List<MinWage>> GetAllMinWagesAsync()
        {
             return  await _dbContext.MinWages
                    .OrderBy(mw => mw.TaxYear)
                    .OrderByDescending(mw => mw.Age).ToListAsync();
            
        }
        public void AddMinWage(Data.MinWage minWage)
        {
            _dbContext.Add(minWage);
            _dbContext.SaveChanges(); 
        }
    }
}
