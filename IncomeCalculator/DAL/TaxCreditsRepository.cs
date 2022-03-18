using IncomeCalculator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IncomeCalculator.DAL
{
    public class TaxCreditsRepository : ITaxCreditsRepository
    {
        public DateTime TaxYear { get; set; }
        private BenefitsContext _benefitsContext;
        public TaxCreditsRepository(BenefitsContext benefitsContext)
        {
            _benefitsContext = benefitsContext;
        }
        public TaxCreditsRepository() { }
        public ChildTaxCredit CTCDetails()         // Move to services
        {
            try
            {
                return _benefitsContext.ChildTaxCredits.Single(ctc => ctc.TaxYear.Year == TaxYear.Year); 
            }
            catch (InvalidOperationException)
            {
                throw new Exception("No records found!");
            }
            catch (Exception) { throw; }
        }

        public WorkingTaxCredit WTCDetails()  // Move to services
        {
            try
            {
                return  _benefitsContext.WorkingTaxCredits.Single(wtc => wtc.TaxYear.Year == TaxYear.Year);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("No records found!");
            }
            catch (Exception) { throw; }
        }

        public List<WorkingTaxCredit> GetAllWTCData()
        {
            return _benefitsContext.WorkingTaxCredits.ToList();
        }

        public List<ChildTaxCredit> GetAllCTCData()
        {
            return _benefitsContext.ChildTaxCredits.ToList();
        }

        public void AddWTCData(WorkingTaxCredit wtc)
        {
            _benefitsContext.WorkingTaxCredits.Add(wtc);
            _benefitsContext.SaveChanges();
        }

        public void AddCTCData(ChildTaxCredit ctc)
        {
            _benefitsContext.ChildTaxCredits.Add(ctc);
            _benefitsContext.SaveChanges();
        }
    }
}
