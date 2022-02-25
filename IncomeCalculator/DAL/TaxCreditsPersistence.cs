using IncomeCalculator.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IncomeCalculator.DAL
{
    public class TaxCreditsPersistence
    {
        private readonly DateTime _dateTime;
        private BenefitsContext _benefitsContext;
        public TaxCreditsPersistence(BenefitsContext benefitsContext, DateTime dateTime)
        {
            _benefitsContext = benefitsContext;
            _dateTime = dateTime;
        }
        public ChildTaxCredit CTCDetails()
        {
            try
            {
                return _benefitsContext.ChildTaxCredits.Single(ctc => ctc.TaxYear.Year == _dateTime.Year);
            }
            catch (InvalidOperationException )
            {
                throw new Exception("No records found!");
            }
            catch (Exception) {  throw ; }
        }

        public WorkingTaxCredit WTCDetails()
        {
            try
            {
                return _benefitsContext.WorkingTaxCredits.Single(wtc => wtc.TaxYear.Year == _dateTime.Year);
            }
            catch (InvalidOperationException )
            {
                throw new Exception("No records found!"); 
            }
            catch (Exception) { throw; }
        }
    }
}
