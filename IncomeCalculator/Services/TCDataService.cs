using AutoMapper;
using BootstrapBlazor.Components;
using IncomeCalculator.DAL;
using IncomeCalculator.Data;
using System.Linq;

namespace IncomeCalculator.Services
{
    public class TCDataService
    {
        private ITaxCreditsRepository _taxCreditsRepo;
        private IMapper _mapper;
        private IMessageService _messageService;
        public TCDataService(ITaxCreditsRepository tcRepo, IMapper mapper, IMessageService messageSerice)
        {
            _taxCreditsRepo = tcRepo;
            _mapper = mapper;
            _messageService = messageSerice;
        }
        public bool CanAddWTCData(DTO.WorkingTaxCredit dtoWTC)
        {
            var existing = _taxCreditsRepo.GetAllWTCData().Any(wtc => wtc.TaxYear.Year == dtoWTC.TaxYear.Year);
            if(!existing)
            {
                var dataWtc = _mapper.Map<DTO.WorkingTaxCredit, Data.WorkingTaxCredit>(dtoWTC);
                _taxCreditsRepo.AddWTCData(dataWtc);
                return true;
            }
             else
                _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
            return false;
        }

        public bool CanAddCTCData(DTO.ChildTaxCredit dtoCTC)
        {
            var existing = _taxCreditsRepo.GetAllCTCData().Any(ctc => ctc.TaxYear.Year == dtoCTC.TaxYear.Year);
            if(!existing)
            {
                var dataCtc = _mapper.Map<DTO.ChildTaxCredit, Data.ChildTaxCredit>(dtoCTC);
                _taxCreditsRepo.AddCTCData(dataCtc);
                return true;
            }
            else
                _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
            return false;
        }
    }
}
