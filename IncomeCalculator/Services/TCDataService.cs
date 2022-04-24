using AutoMapper;
using BootstrapBlazor.Components;
using IncomeCalculator.DAL;
using IncomeCalculator.Data;
using IncomeCalculator.Shared.Enums;
using IncomeCalculator.Shared.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace IncomeCalculator.Services
{
    public class TCDataService
    {
        private ITaxCreditsRepository _taxCreditsRepo;
        private IMapper _mapper;
        private IMessageService _messageService;
        public TCDataService(ITaxCreditsRepository tcRepo, IMapper mapper, IMessageService messageService)
        {
            _taxCreditsRepo = tcRepo;
            _mapper = mapper;
            _messageService = messageService;
        }
        public async Task AddWTCData(Shared.DTO.WorkingTaxCredit dtoWTC)
        {
            var existing = _taxCreditsRepo.GetAllWTCData().Any(wtc => wtc.TaxYear.Year == dtoWTC.TaxYear.Year);
            if(!existing)
            {
                var dataWtc = _mapper.Map<Shared.DTO.WorkingTaxCredit, Data.WorkingTaxCredit>(dtoWTC);
                _taxCreditsRepo.AddWTCData(dataWtc);
                await _messageService.TostrAlert(MessageType.Success, "Operation Successful!");
            }
             else
               await _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
        }

        public async Task AddCTCData(Shared.DTO.ChildTaxCredit dtoCTC)
        {
            var existing = _taxCreditsRepo.GetAllCTCData().Any(ctc => ctc.TaxYear.Year == dtoCTC.TaxYear.Year);
            if(!existing)
            {
                var dataCtc = _mapper.Map<Shared.DTO.ChildTaxCredit, Data.ChildTaxCredit>(dtoCTC);
                _taxCreditsRepo.AddCTCData(dataCtc);
                await _messageService.TostrAlert(MessageType.Success, "Operation Successful!");
            }
            else
               await _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
        }
    }
}
