using AutoMapper;
using IncomeCalculator.DAL;
using IncomeCalculator.Data;
using IncomeCalculator.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncomeCalculator.Services
{
    public class MinWageService
    {
        private IMinWageRepository _minWageRepository;
        private IMapper _mapper;
        private IMessageService _messageService;
        public List<DTO.MinWage> MinWages { get; set; }
        public MinWageService(IMinWageRepository minWageRepository, IMapper mapper, IMessageService messageService)
        {
            _minWageRepository = minWageRepository;
            _messageService = messageService;
            _mapper = mapper;
            SetMinWages();
        }

        public async Task<DTO.MinWage> GetMinWage(int age, DateTime taxYear)
        {
            return await Task.Run(() => MinWages.Where(mw => mw.TaxYear.Year == taxYear.Year && mw.Age <= age)
                .OrderByDescending(mw => mw.Wage)
                .First());
        }
        public bool CanAddMinWage(DTO.MinWage dtoMinWage)
        {
            var existing = MinWages.Any(mw => mw.TaxYear == dtoMinWage.TaxYear && mw.Age == dtoMinWage.Age);
            if (!existing)
            {
                dtoMinWage.TaxYear = new DateTime(dtoMinWage.TaxYear.Year, 04, 06);
                var dataMW = _mapper.Map<DTO.MinWage, Data.MinWage>(dtoMinWage);
                _minWageRepository.AddMinWage(dataMW);
                SetMinWages();
                return true;
            }
            else
                _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
            return false;
        }

        private void SetMinWages()
        {
            MinWages = _mapper.Map<IEnumerable<Data.MinWage>, IEnumerable<DTO.MinWage>>(_minWageRepository.GetMinWages()).ToList();
        }
    }  
        
}