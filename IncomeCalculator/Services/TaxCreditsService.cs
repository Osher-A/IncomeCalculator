﻿using IncomeCalculator.DAL;
using IncomeCalculator.Data;
using IncomeCalculator.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace IncomeCalculator.Services
{

    public class TaxCreditsService
    {
        private TaxCreditsPersistence _tcPersistence;
        private FinancialDetails _financialDetails;
        private ChildTaxCredit _ctcDetails;
        private WorkingTaxCredit _wtcDetails;
        private string _message;
        private int _numberOfParents;
        private decimal _wtcThreshold;
        private decimal _ctcThreshold;
        public TaxCreditsService(FinancialDetails financialDetails)
        {
           _financialDetails = financialDetails;
          _tcPersistence = new TaxCreditsPersistence(new BenefitsContext(), _financialDetails.TaxYear);
            _numberOfParents = (financialDetails.SingleParent) ? 1 : 2;
            try
            {
                _wtcDetails = _tcPersistence.WTCDetails();
                _ctcDetails = _tcPersistence.CTCDetails();
                _wtcThreshold = _wtcDetails.Threshold * _numberOfParents;
                _ctcThreshold = _ctcDetails.Threshold * _numberOfParents;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public (decimal, string) GetTaxCreditsTotal()
        {
            var totalIncome = GetTotalIncome();
            var children = _financialDetails.ChildrenAmount;

            decimal maxTaxCredits = MaxCredits(children);
            decimal withdrawalAmount = WithdrawalAmount(totalIncome);

            var totalCredits = maxTaxCredits - withdrawalAmount;
            return (CreditsEntitlement(totalCredits, children), _message);
        }
        private decimal GetTotalIncome()
        {
            return _financialDetails.CombindedIncome;
        }
        private decimal MaxCredits(int children)  // When only one Adult       ##To do
        {
            var maxCTC = _ctcDetails.FamilyElement + (_ctcDetails.ChildElement * children);
            var maxWTC = WTCEntitlement();
            var maxTaxCredits = maxCTC + maxWTC;
            return maxTaxCredits;
        }

        private decimal WTCEntitlement()
        {
            decimal parent1HoursPW = _financialDetails.Parent1WorkDetails.HoursPW;
            decimal parent2HoursPW = (_financialDetails.Parent2WorkDetails != null)? _financialDetails.Parent2WorkDetails.HoursPW: 0;
            if (WTC() < 0)
                _message = "You are not entitled to Working Tax Credit, one parent has to be working at least 16 hrs pw, and for two parent's it's also required that both parents work a min of 24 hours pw combined";
            return WTC();
        }
        private decimal WTC()
		{
            decimal parent1HoursPW = _financialDetails.Parent1WorkDetails.HoursPW;
            decimal parent2HoursPW = (_financialDetails.Parent2WorkDetails != null) ? _financialDetails.Parent2WorkDetails.HoursPW : 0;
            if (_financialDetails.SingleParent)
            {
                if (parent1HoursPW >= 30)
                    return _wtcDetails.BasicElement + _wtcDetails.ThirtyHourElement;
                else if (parent1HoursPW >= 16)
                    return _wtcDetails.BasicElement;
            }
            else
			{
              if ((parent1HoursPW >= 16 || parent2HoursPW >= 16) && parent1HoursPW + parent2HoursPW >= 24)
                if (parent1HoursPW + parent2HoursPW >= 30)
                    return _wtcDetails.BasicElement + _wtcDetails.SecondAdult + _wtcDetails.ThirtyHourElement;
                else if (parent1HoursPW + parent2HoursPW >= 24)
                    return _wtcDetails.BasicElement + _wtcDetails.SecondAdult;
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
            var wtcOverThreshold = (totalIncome - ctcOverThreshold) - _wtcThreshold;
            var overThreshold = ctcOverThreshold + wtcOverThreshold;
            return (decimal)overThreshold;
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
    }
}

