using IncomeCalculator.Models;
using System;

namespace IncomeCalculator.Data
{
    public class WorkingTaxCredit
    {
        public int Id { get; set; }
        public DateTime TaxYear { get; set; }
        public decimal BasicElement { get; set; }
        public decimal SecondAdult { get; set; }
        public decimal ThirtyHourElement { get; set; }
        public decimal Threshold { get; set; }
        public float WithdrawalRate { get; set; }

        public WorkingTaxCredit(DateTime taxYear, decimal basicElement,
            decimal secondAdult, decimal thirtyHourElement, decimal threshold, float withdrawalRate)
        {
            TaxYear = taxYear;
            BasicElement = basicElement;
            SecondAdult = secondAdult;
            ThirtyHourElement = thirtyHourElement;
            Threshold = threshold;
            WithdrawalRate = withdrawalRate;
        }

        public WorkingTaxCredit() { }
    }
}
