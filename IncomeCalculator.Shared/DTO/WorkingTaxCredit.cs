using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace IncomeCalculator.Shared.DTO
{
    public class WorkingTaxCredit
    {
        public int Id { get; set; }
        [Required]
        public DateTime TaxYear { get; set; } = new DateTime(DateTime.Now.Year, 04, 06);
        [Required]
        [NotNull]
        public decimal BasicElement { get; set; } 
        [Required]
        [NotNull]
        public decimal SecondAdult { get; set; }
        [Required]
        [NotNull]
        public decimal ThirtyHourElement { get; set; }
        [Required]
        [NotNull]
        public decimal Threshold { get; set; }
        [Required]
        [NotNull]
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
