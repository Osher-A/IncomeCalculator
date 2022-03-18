using Excubo.Generators.Blazor;
using System;
using System.Diagnostics.CodeAnalysis;

namespace IncomeCalculator.DTO
{
    public class ChildTaxCredit
    {
        public int Id { get; set; }
        [Required]
        public DateTime TaxYear { get; set; } = new DateTime(DateTime.Now.Year, 04, 06);
        [Required]
        [NotNull]
        public decimal? FamilyElement { get; set; }
        [Required]
        [NotNull]
        public decimal? ChildElement { get; set; }
        [Required]
        [NotNull]
        public decimal? Threshold { get; set; }
        [Required]
        [NotNull]
        public float? WithdrawalRate { get; set; }

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
