using IncomeCalculator.Data;
using System;
using System.Threading.Tasks;

namespace IncomeCalculator.DAL
{
    public interface ITaxCreditsRepository
    {
        DateTime TaxYear { get; set; }

        ChildTaxCredit CTCDetails();
        WorkingTaxCredit WTCDetails();
    }
}