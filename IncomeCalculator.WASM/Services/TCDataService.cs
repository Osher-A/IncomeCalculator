using BootstrapBlazor.Components;
using IncomeCalculator.Shared.Interfaces;
using IncomeCalculator.Shared.DTO;
using System.Linq;
using System.Runtime.CompilerServices;
using IncomeCalculator.Shared.Enums;

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
        public async Task AddWTCData(WorkingTaxCredit dtoWTC)
        {
            var existing = (await  _tcApiService.GetWTCData()).Any(wtc => wtc.TaxYear.Year == dtoWTC.TaxYear.Year);
            if (!existing)
            {
                await _tcApiService.AddWTC(dtoWTC);
                await _messageService.TostrAlert(MessageType.Success, "Operation Successful!");
            }
            else
                await _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
        }

        public async Task AddCTCData(ChildTaxCredit dtoCTC)
        {
            var existing = (await _tcApiService.GetCTCData()).Any(ctc => ctc.TaxYear.Year == dtoCTC.TaxYear.Year);
            if (!existing)
            {
               await _tcApiService.AddCTC(dtoCTC);
               await _messageService.TostrAlert(MessageType.Success, "Operation Successful!");
            }
            else
               await _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
        }

        
    }
}
