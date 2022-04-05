using System;
using System.ComponentModel.DataAnnotations;

namespace IncomeCalculator.WASM.ViewModel
{
    public class FinancialDetails
    {
        public bool SingleParent { get; set; }
        public WorkDetails Parent1WorkDetails { get; set; }
        public WorkDetails Parent2WorkDetails { get; set; } = new();
        public decimal OtherIncome { get; set; }
        public int ChildrenAmount { get; set; }
        public DateTime TaxYear { get; set; }
       
        public async Task<decimal> CombindedIncome()
        {

            if (Parent2WorkDetails != null)
                return await Parent1WorkDetails.Total() + await Parent2WorkDetails.Total() + OtherIncome;
            else
                return await Parent1WorkDetails.Total() + OtherIncome;
        }
    }
}
