using IncomeCalculator.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncomeCalculator.Shared.Interfaces
{
    public interface ITaxCreditsRepository
    {
        DateTime TaxYear { get; set; }

        void AddCTCData(ChildTaxCredit ctc);
        void AddWTCData(WorkingTaxCredit wtc);
        ChildTaxCredit CTCDetails();
        List<ChildTaxCredit> GetAllCTCData();
        List<WorkingTaxCredit> GetAllWTCData();
        WorkingTaxCredit WTCDetails();
    }
}