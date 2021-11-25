using IncomeCalculator.DAL;
using IncomeCalculator.Data;
using IncomeCalculator.Models;

namespace IncomeCalculator.Services
{

    public class TaxCreditsService
    {
        private TaxCreditsPersistence _tcPersistence;
        private FinancialDetails _financialDetails;
        private ChildTaxCredit _ctcDetails;
        private WorkingTaxCredit _wtcDetails;
        public TaxCreditsService(FinancialDetails financialDetails)
        {
            _financialDetails = financialDetails;
            _tcPersistence = new TaxCreditsPersistence(new BenefitsContext(), _financialDetails.TaxYear);
            _wtcDetails = _tcPersistence.WTCDetails();
            _ctcDetails = _tcPersistence.CTCDetails();
        }
        public decimal GetTaxCreditsTotal()
        {
            var totalIncome = GetTotalIncome();
            var children = _financialDetails.ChildrenAmount;

            decimal maxTaxCredits = MaxCredits(children);
            decimal? withdrawalAmount = WithdrawalAmount(totalIncome);

            var totalCredits = maxTaxCredits - withdrawalAmount;

            return (decimal)totalCredits;
        }
        private decimal? GetTotalIncome()
        {
            return _financialDetails.Parent1WorkDetails.Total
                        + _financialDetails.Parent2WorkDetails.Total + _financialDetails.OtherIncome;
        }
        private decimal MaxCredits(int children)
        {
            var maxCTC = _ctcDetails.FamilyElement + (_ctcDetails.ChildElement * children);
            var maxWTC = _wtcDetails.BasicElement + _wtcDetails.SecondAdult + _wtcDetails.ThirtyHourElement; // need to calculate hours and if one adult

            var maxTaxCredits = maxCTC + maxWTC;

            return maxTaxCredits;
        }

        private decimal? WithdrawalAmount(decimal? totalIncome)
        {
            decimal? overThreshold = OverThresholdAmount(totalIncome);

            var withdrawalAmount = overThreshold * (decimal)_wtcDetails.WithdrawalRate;
            return withdrawalAmount;
        }

        private decimal? OverThresholdAmount(decimal? totalIncome)
        {
            decimal? overThreshold = 0;

            if (totalIncome > _wtcDetails.Threshold && totalIncome < _ctcDetails.Threshold)
                overThreshold = totalIncome - _wtcDetails.Threshold;
            else if (totalIncome > _ctcDetails.Threshold)
                overThreshold = CtcOverThreshold(totalIncome);

            return overThreshold;
        }

        private decimal CtcOverThreshold(decimal? totalIncome)
        {
            var ctcOverThreshold = totalIncome - _ctcDetails.Threshold;
            var wtcOverThreshold = (totalIncome - ctcOverThreshold) - _wtcDetails.Threshold;
            var overThreshold = ctcOverThreshold + wtcOverThreshold;
            return (decimal)overThreshold;
        }


    }
}

