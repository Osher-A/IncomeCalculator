using IncomeCalculator.Data;
using IncomeCalculator.Model;
using IncomeCalculator.Models;
using IncomeCalculator.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace IncomeCalculator.Pages
{
    public partial class Wages
    {

        public WorkDetails WdModel { get; private set; } = new WorkDetails();
        public List<TaxYear> TaxYears { get; private set; }
        public string Display
        {
            get
            {
                if (WdModel.IsMinWage)
                    return "normal";
                else
                    return "none";
            }
        }

        protected override void OnInitialized()
        {
            TaxYears = GetTaxYears();
            WdModel.MinWage.TaxYear = new DateTime(2020, 04, 06);
        }
       
        private List<TaxYear> GetTaxYears()
        {
            return new List<TaxYear>()
                {
                   new TaxYear{UiTaxYear= "2021-2022", DbTaxYear = new DateTime(2021,04,06)},
                   new TaxYear{UiTaxYear= "2022-2023", DbTaxYear = new DateTime(2022,04,06)},
                };
        }

        private void OnChange(ChangeEventArgs e)
        {
           WdModel.HourlyRate = decimal.Parse(e.Value.ToString(), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture);
        }
    }
}

