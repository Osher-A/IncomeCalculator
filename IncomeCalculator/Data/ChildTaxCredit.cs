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

        public ChildTaxCredit() { }
        public ChildTaxCredit(DateTime taxYear, decimal familyElement, decimal childElement,
            decimal threshold, float withdrawalRate)
        {
            TaxYear = taxYear;
            FamilyElement = familyElement;
            ChildElement = childElement;
            Threshold = threshold;
            WithdrawalRate = withdrawalRate;
        }
    }
}
