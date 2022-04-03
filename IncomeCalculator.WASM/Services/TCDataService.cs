using BootstrapBlazor.Components;
using IncomeCalculator.Shared.Interfaces;
using IncomeCalculator.Shared.DTO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace IncomeCalculator.WASM.Services
{
    public class TCDataService
    {
        private IMessageService _messageService;
        private HttpClient _httpClient;
        private TCApiService _tcApiService;
        public TCDataService(HttpClient httpClient, TCApiService tCApiService, IMessageService messageSerice)
        {
            _httpClient = httpClient;
            _tcApiService = tCApiService;
            _messageService = messageSerice;
        }
        public async Task<bool> CanAddWTCData(WorkingTaxCredit dtoWTC)
        {
            var existing = (await  _tcApiService.GetWTCData()).Any(wtc => wtc.TaxYear.Year == dtoWTC.TaxYear.Year);
            if (!existing)
            {
                await _tcApiService.AddWTC(dtoWTC);
                return true;
            }
            else
                await _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
            return false;
        }

        public async Task<bool> CanAddCTCData(ChildTaxCredit dtoCTC)
        {
            var existing = (await _tcApiService.GetCTCData()).Any(ctc => ctc.TaxYear.Year == dtoCTC.TaxYear.Year);
            if (!existing)
            {
               await _tcApiService.AddCTC(dtoCTC);
                return true;
            }
            else
               await _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
            return false;
        }

        
    }
}
