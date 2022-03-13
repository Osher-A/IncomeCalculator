using IncomeCalculator.Data;
using System;
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
        public ChildTaxCredit CTCDetails()
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

        public WorkingTaxCredit WTCDetails()
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
    }
}
