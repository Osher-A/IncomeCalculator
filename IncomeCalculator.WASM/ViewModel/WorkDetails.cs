using IncomeCalculator.Shared.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace IncomeCalculator.WASM.ViewModel
{
    public class WorkDetails
    {
        private string _hourlyRate = "£0.00";
        private readonly IMinWageService _minWageService;

        [Range(10, 120, ErrorMessage = "Please enter a valid age!")]
        public int Age { get; set; }
        [Range(1, 168)]
        public decimal HoursPW { get; set; }
        public bool IsMinWage { get; set; }
        public DateTime TaxYear { get; set; }
        [Required]
        public Period Period { get; set; }
        public string HourlyRate
        {
            get
            {
                decimal rate;
                var parseAble = decimal.TryParse(_hourlyRate, NumberStyles.Currency, CultureInfo.CurrentCulture, out rate);
                if (parseAble)
                    return rate.ToString("C");
                else
                    return "£0.00";
            }
            set => _hourlyRate = value;
        }

        public WorkDetails(IMinWageService minWageService)
        {
            _minWageService = minWageService;
        }

        public async Task<decimal> Total()
        {
            if (IsMinWage && HoursPW >= 1)
               await GetMinWageAsync();

            return GetTotal();
        }
        private decimal GetTotal()
        {
            bool parasAble = decimal.TryParse(HourlyRate, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal hourlyRate);

            if (parasAble)
            {
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
            return 0;
        }

        private async Task GetMinWageAsync()
        {
            try
            {
                var minwage = await _minWageService.GetMinWage(Age, TaxYear);
                _hourlyRate = minwage.Wage.ToString();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public enum Period
    {
        Week,
        Month,
        Year
    }
}
