using IncomeCalculator.Data;
using System;
using System.Linq;

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
            return _benefitsContext.ChildTaxCredits.Single(ctc => ctc.TaxYear.Year == _dateTime.Year);
        }

        public WorkingTaxCredit WTCDetails()
        {
            return _benefitsContext.WorkingTaxCredits.Single(wtc => wtc.TaxYear.Year == _dateTime.Year);
        }
    }
}
