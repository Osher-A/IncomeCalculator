using AutoMapper;
using IncomeCalculator.DAL;
using IncomeCalculator.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IncomeCalculator.Shared.DTO;
using IncomeCalculator.Shared.Interfaces;

namespace IncomeCalculator.Services
{
    public class MinWageService : IMinWageService
    {
        private IMinWageRepository _minWageRepository;
        private IMapper _mapper;
        private IMessageService _messageService;
        public List<Shared.DTO.MinWage> MinWages { get; set; }
        public MinWageService(IMinWageRepository minWageRepository, IMapper mapper, IMessageService messageService)
        {
            _minWageRepository = minWageRepository;
            _messageService = messageService;
            _mapper = mapper;
            GetAll();
        }

        public async Task<Shared.DTO.MinWage> GetMinWage(int age, DateTime taxYear)
        {
            try
            {
                return await Task.Run(() => MinWages.Where(mw => mw.TaxYear.Year == taxYear.Year && mw.Age <= age)
                        .OrderByDescending(mw => mw.Wage)
                        .First());
            }
            catch (ArgumentNullException ex)
            {
                await _messageService.TostrAlert(IncomeCalculator.Shared.Enums.MessageType.Error, "There doesn't seem to be any data for your query!");
                return new Shared.DTO.MinWage { Wage = 0 };
            }
            catch (Exception ex)
            {
                await _messageService.TostrAlert(IncomeCalculator.Shared.Enums.MessageType.Error, ex.Message);
                return new Shared.DTO.MinWage { Wage = 0 };
            }
        }
        public async Task<bool> CanAddMinWage(Shared.DTO.MinWage dtoMinWage)
        {
            var existing = MinWages.Any(mw => mw.TaxYear == dtoMinWage.TaxYear && mw.Age == dtoMinWage.Age);
            if (!existing)
            {
                dtoMinWage.TaxYear = new DateTime(dtoMinWage.TaxYear.Year, 04, 06);
                var dataMW = _mapper.Map<Shared.DTO.MinWage, Data.MinWage>(dtoMinWage);
                _minWageRepository.AddMinWage(dataMW);
                GetAll();
                return true;
            }
            else
                await  _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
            return false;
        }

        private void GetAll()
        {
            MinWages = _mapper.Map<IEnumerable<Data.MinWage>, IEnumerable<Shared.DTO.MinWage>>(_minWageRepository.GetMinWages()).ToList();
        }
    }

}