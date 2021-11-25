using System;

namespace IncomeCalculator.Models
{
	public class FinancialDetails
	{
		public WorkDetails Parent1WorkDetails { get; set; }
		public WorkDetails Parent2WorkDetails { get; set; }
		public decimal OtherIncome { get; set; }
		public int ChildrenAmount { get; set; }
		public DateTime TaxYear { get; set; }
		public decimal? CombindedIncome
		{
			get { return Parent1WorkDetails.Total + Parent2WorkDetails.Total + OtherIncome; }
		}
	}
}
