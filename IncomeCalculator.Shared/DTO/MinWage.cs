using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace IncomeCalculator.Shared.DTO
{
    public partial class MinWage 
    {
        public int Id { get; set; }
        [Required]
        [Range(10, 120, ErrorMessage = "Please provide a valid age!")]
        [NotNull]
        public int? Age { get; set; } = null;
        public DateTime TaxYear { get; set; } = new DateTime(DateTime.Now.Year, 04, 06);
        [Required]
        [Range(4, 100, ErrorMessage = "Please provide a valid wage!")]
        [NotNull]
        public decimal? Wage { get; set; } = null;

        public MinWage() { }
        public MinWage(int age, DateTime taxYear, decimal wage)
        {
            Age = age;
            TaxYear = taxYear;
            Wage = wage;
        }
    }
}
