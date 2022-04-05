using IncomeCalculator.WASM.ViewModel;
using IncomeCalculator.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Linq;

namespace IncomeCalculator.WASM.Services
{

    public class TCCalculatorApiService
    {
        private HttpClient _httpClient;
        private TCApiService _tCApiService;
        private FinancialDetails _financialDetails;
        private List<WorkingTaxCredit> _workingTaxCreditData;
        private List<ChildTaxCredit> _childTaxCreditData;
        private ChildTaxCredit _ctcDetails;
        private WorkingTaxCredit _wtcDetails;
        private string _message;
        private int _numberOfParents;
        private decimal _wtcThreshold;
        private decimal _ctcThreshold;
        public TCCalculatorApiService(HttpClient httpClient, TCApiService tCApiService, FinancialDetails financialDetails)
        {
            _httpClient = httpClient;
            _tCApiService = tCApiService;
            _financialDetails = financialDetails;
            _numberOfParents = financialDetails.SingleParent ? 1 : 2;
        }
        public async Task<(decimal, string)> GetTaxCreditsTotal()
        {
            await LoadData(_financialDetails.TaxYear);

            var totalIncome = await GetTotalIncome();
            var children = _financialDetails.ChildrenAmount;

            decimal maxTaxCredits = MaxCredits(children);
            decimal withdrawalAmount =  WithdrawalAmount(totalIncome);

            var totalCredits = maxTaxCredits - withdrawalAmount;
            return (CreditsEntitlement(totalCredits, children), _message);
        }
        private async Task<decimal> GetTotalIncome()
        {
            return await _financialDetails.CombindedIncome(); 
        }
        private decimal MaxCredits(int children)  // When only one Adult       ##To do
        {
            var maxCTC = (decimal)_ctcDetails.FamilyElement + (decimal)_ctcDetails.ChildElement * children;
            var maxWTC = WTCEntitlement();
            var maxTaxCredits = maxCTC + maxWTC;
            return maxTaxCredits;
        }

        private decimal WTCEntitlement()
        {
            decimal parent1HoursPW = _financialDetails.Parent1WorkDetails.HoursPW;
            decimal parent2HoursPW = _financialDetails.Parent2WorkDetails != null ? _financialDetails.Parent2WorkDetails.HoursPW : 0;
            if (WTC() < 0)
                _message = "You are not entitled to Working Tax Credit, one parent has to be working at least 16 hrs pw, and for two parent's it's also required that both parents work a min of 24 hours pw combined";
            return WTC();
        }
        private decimal WTC()
        {
            decimal parent1HoursPW = _financialDetails.Parent1WorkDetails.HoursPW;
            decimal parent2HoursPW = _financialDetails.Parent2WorkDetails != null ? _financialDetails.Parent2WorkDetails.HoursPW : 0;
            if (_financialDetails.SingleParent)
            {
                if (parent1HoursPW >= 30)
                    return (decimal)_wtcDetails.BasicElement + (decimal)_wtcDetails.ThirtyHourElement;
                else if (parent1HoursPW >= 16)
                    return (decimal)_wtcDetails.BasicElement;
            }
            else
            {
                if ((parent1HoursPW >= 16 || parent2HoursPW >= 16) && parent1HoursPW + parent2HoursPW >= 24)
                    if (parent1HoursPW + parent2HoursPW >= 30)
                        return (decimal)_wtcDetails.BasicElement + (decimal)_wtcDetails.SecondAdult + (decimal) _wtcDetails.ThirtyHourElement;
                    else if (parent1HoursPW + parent2HoursPW >= 24)
                        return (decimal)_wtcDetails.BasicElement + (decimal)_wtcDetails.SecondAdult;
            }
            return 0;
        }

        private decimal WithdrawalAmount(decimal totalIncome)
        {
            decimal overThreshold = OverThresholdAmount(totalIncome);

            var withdrawalAmount = overThreshold * (decimal)_wtcDetails.WithdrawalRate;
            return withdrawalAmount;
        }

        private decimal OverThresholdAmount(decimal totalIncome)
        {
            decimal overThreshold = 0;

            if (totalIncome > _wtcThreshold && totalIncome < _ctcThreshold)
                overThreshold = totalIncome - _wtcThreshold;
            else if (totalIncome > _ctcThreshold)
                overThreshold = CtcOverThreshold(totalIncome);


            return overThreshold;
        }

        private decimal CtcOverThreshold(decimal totalIncome)
        {
            var ctcOverThreshold = totalIncome - _ctcThreshold;
            var wtcOverThreshold = totalIncome - ctcOverThreshold - _wtcThreshold;
            var overThreshold = ctcOverThreshold + wtcOverThreshold;
            return overThreshold;
        }

        private decimal CreditsEntitlement(decimal totalCredits, int children)
        {
            if (totalCredits <= 0)
            {
                _message = "You are not entitled to any tax crdits, your income is too high!";
                return 0;
            }
            else if (children < 1)
            {
                _message = "You need to have at least one child to be entitled for tax credits!";
                return 0;
            }
            else
                return totalCredits;
        }

        private async Task LoadData(DateTime taxYear)
        {
            try
            {
                _workingTaxCreditData = await _tCApiService.GetWTCData();
                _childTaxCreditData = await _tCApiService.GetCTCData();
                _wtcDetails = GetWTCDetails(taxYear);
                _ctcDetails = GetCTCDetails(taxYear);
                _wtcThreshold = (decimal)_wtcDetails.Threshold * _numberOfParents;
                _ctcThreshold = (decimal)_ctcDetails.Threshold * _numberOfParents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private WorkingTaxCredit GetWTCDetails(DateTime taxYear)
        {
            try
            {
                return _workingTaxCreditData.Single(wtc => wtc.TaxYear.Year == taxYear.Year);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("No records found!");
            }
            catch (Exception) { throw; }
        }
        private ChildTaxCredit GetCTCDetails(DateTime taxYear)
        {
            try
            {
                return _childTaxCreditData.Single(ctc => ctc.TaxYear.Year == taxYear.Year);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("No records found!");
            }
            catch (Exception) { throw; }
        }

       
    }
}

