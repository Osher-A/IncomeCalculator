using System;
using System.ComponentModel.DataAnnotations;

namespace IncomeCalculator.ViewModels
{
    public class FinancialDetails
    {
        public bool SingleParent { get; set; }
        public WorkDetails Parent1WorkDetails { get; set; }
        public WorkDetails Parent2WorkDetails { get; set; }
        public decimal OtherIncome { get; set; }
        public int ChildrenAmount { get; set; }
        public DateTime TaxYear { get; set; }
        public decimal CombindedIncome
        {
            get
            {
                if (Parent2WorkDetails != null)
                    return Parent1WorkDetails.Total + Parent2WorkDetails.Total + OtherIncome;
                else
                    return Parent1WorkDetails.Total + OtherIncome;
            }
        }
    }
}
