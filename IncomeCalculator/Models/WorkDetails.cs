using BootstrapBlazor.Components;
using IncomeCalculator.Data;
using IncomeCalculator.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace IncomeCalculator.Models
{
    public class WorkDetails
    {
        private string _hourlyRate = "0";
        private readonly MinWageService _minWageService;

        [Range(10, 120, ErrorMessage = "Please enter a valid age!")]
        public int Age { get; set; }
        [Range(1, 168)]
        public decimal HoursPW { get; set; }
        public bool IsMinWage { get; set; }
		public DateTime TaxYear { get; set; }
        [Required]
        public Period Period { get; set; }
        public decimal Total { get { return GetTotal(); } set { } }
        public string HourlyRate
        {
            get
            {
                if (IsMinWage && HoursPW >= 1)
                    GetMinWage();
                decimal rate;
                var parseAble = decimal.TryParse(_hourlyRate, System.Globalization.NumberStyles.Currency, CultureInfo.CurrentCulture, out rate);
                if (parseAble)
                    return rate.ToString("C");
                else
                    return "£0.00";
            }
            set => _hourlyRate = value;
        }

        public WorkDetails()
        {
            _minWageService = new MinWageService(new DAL.MinWagePersistence(new BenefitsContext()));
        }

        private decimal GetTotal()
        {
            decimal hourlyRate = decimal.Parse(HourlyRate, NumberStyles.Currency);
           
            var weeklyWage = HoursPW * hourlyRate;
            switch (Period)
            {
                case Period.Month:
                    return weeklyWage * 52 / 12;
                case Period.Year:
                    return weeklyWage * 52;
                default:
                    return weeklyWage;
            }
        }

        private void GetMinWage()
        {
            try
            {
                var minwage = _minWageService.GetMinWage(Age, TaxYear);
                _hourlyRate = minwage.Wage.ToString();
            }
            catch (Exception) { }
        }
    }

    public enum Period
    {
        Week,
        Month,
        Year
    }
}
