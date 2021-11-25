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
    }
}
