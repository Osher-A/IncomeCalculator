using AutoMapper;
using IncomeCalculator.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IncomeCalculator.Shared.DTO;
using IncomeCalculator.Shared.Interfaces;
using IncomeCalculator.Shared.Enums;
using IncomeCalculator.DAL;

namespace IncomeCalculator.Services
{
    public class MinWageService : IMinWageService
    {
        IMinWageRepository _minWageRepository;
        private IMapper _mapper;
        private IMessageService _messageService;
        private List<Shared.DTO.MinWage> _minWages;
        public MinWageService(IMinWageRepository minWageRepository, IMapper mapper, IMessageService messageService)
        {
            _minWageRepository = minWageRepository;
            _messageService = messageService;
            _mapper = mapper;
            LoadData();
        }
        public List<Shared.DTO.MinWage> GetAllMinWages()
        {
            return _minWages;
        }
        public async Task<Shared.DTO.MinWage> GetMinWageAsync(int age, DateTime taxYear)
        {
            try
            {
                return  _minWages.Where(mw => mw.TaxYear.Year == taxYear.Year && mw.Age <= age)
                        .OrderByDescending(mw => mw.Wage)
                        .First();
            }
            catch (InvalidOperationException ex)
            {
                return new Shared.DTO.MinWage { Wage = 0 };
                throw new Exception($"There doesn't seem to be any min wage data for the year: {taxYear.Year} !");
            }
            catch (Exception ex) { return new Shared.DTO.MinWage { Wage = 0 };  throw; }
        }
        public async Task AddMinWageAsync(Shared.DTO.MinWage dtoMinWage)
        {
            var existing = _minWages.Any(mw => mw.TaxYear == dtoMinWage.TaxYear && mw.Age == dtoMinWage.Age);
            if (!existing)
            {
                    AddMinWage(dtoMinWage);
                    await _messageService.TostrAlert(MessageType.Success, "Operation successful!");
                    await LoadDataAsync();
            }
            else
                await  _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
        }

        private void AddMinWage(Shared.DTO.MinWage dtoMinWage)
        {
            dtoMinWage.TaxYear = new DateTime(dtoMinWage.TaxYear.Year, 04, 06);
            var dataMW = _mapper.Map<Shared.DTO.MinWage, Data.MinWage>(dtoMinWage);
            _minWageRepository.AddMinWage(dataMW);
            LoadDataAsync();
           
        }

        private void LoadData()
        {
            _minWages = _mapper.Map<List<Data.MinWage>, List<Shared.DTO.MinWage>>(_minWageRepository.GetAllMinWages());
        }
        private async Task LoadDataAsync()
        {
            var minWages = await _minWageRepository.GetAllMinWagesAsync();
            _minWages = _mapper.Map<List<Data.MinWage>, List<Shared.DTO.MinWage>>(minWages);
        }
    }

}