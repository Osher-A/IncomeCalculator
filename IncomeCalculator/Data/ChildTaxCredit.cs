using System;

namespace IncomeCalculator.Data
{
    public class ChildTaxCredit
    {
        public int Id { get; set; }
        public DateTime TaxYear { get; set; }
        public decimal FamilyElement { get; set; }
        public decimal ChildElement { get; set; }
        public decimal Threshold { get; set; }
        public float WithdrawalRate { get; set; }
    }
}
