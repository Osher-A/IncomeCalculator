using IncomeCalculator.Shared.DTO;
using Newtonsoft.Json;

namespace IncomeCalculator.WASM.Services
{
    public class TCApiService
    {
        private HttpClient _httpClient;
        public TCApiService(HttpClient  httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<WorkingTaxCredit>> GetWTCData()
        {
            var response = await _httpClient.GetAsync("/api/taxcredits/wtcdata");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<WorkingTaxCredit>>(content);
            else
                return new List<WorkingTaxCredit>();
        }

        public async Task<List<ChildTaxCredit>> GetCTCData()
        {
            var response = await _httpClient.GetAsync("/api/taxcredits/ctcdata");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<ChildTaxCredit>>(content);
            else
                return new List<ChildTaxCredit>();
        }

        public async Task AddWTC(WorkingTaxCredit wtc)
        {
            var content = JsonConvert.SerializeObject(wtc);
            var bodyContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("/api/taxcredits/addwtc", bodyContent);
        }

        public async Task AddCTC(ChildTaxCredit ctc)
        {
            var content = JsonConvert.SerializeObject(ctc);
            var bodyContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("/api/taxcredits/addctc", bodyContent);
        }
    }
}
