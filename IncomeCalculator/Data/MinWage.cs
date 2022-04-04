using System;
using System.ComponentModel.DataAnnotations;
using IncomeCalculator.Shared.DTO;
using IncomeCalculator.Shared.Interfaces;

#nullable disable

namespace IncomeCalculator.Data
{
    public partial class MinWage 
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public DateTime TaxYear { get; set; } = DateTime.Now;
        public decimal Wage { get; set; }

        public MinWage() { }
        public MinWage(int age, DateTime taxYear, decimal wage)
        {
            Age = age;
            TaxYear = taxYear;
            Wage = wage;
        }
    }
}
