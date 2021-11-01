using IncomeCalculator.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncomeCalculator.DAL
{
    public class MinWageRepository
    {
        private BenefitsContext _dbContext;
        public MinWageRepository(BenefitsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MinWage>> GetMinWagesAsync()
        {
            try
            {
                var result =  await _dbContext.MinWages
                    .OrderBy(mw => mw.TaxYear)
                    .OrderByDescending(mw => mw.Age).ToListAsync();
                return result;
            }
            catch (NullReferenceException )
            {
                //Console.WriteLine("Sorry there doesn't exist any information for the min wage of your selection!");
            }
            catch (InvalidOperationException ex)
            {
                //Console.WriteLine("Please check your Database connection, and try again");
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
