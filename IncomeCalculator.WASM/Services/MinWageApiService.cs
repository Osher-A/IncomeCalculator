using System.Net.Http;
using Newtonsoft.Json;
using IncomeCalculator.Shared.DTO;
using IncomeCalculator.Shared.Interfaces;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;

namespace IncomeCalculator.WASM.Services
{
    public class MinWageApiService : IMinWageService
    {
        private readonly HttpClient _httpClient;
        public List<MinWage>? MinWages { get; set; }
        public MinWageApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //Task.Run(() => SetMinWages()).Wait();
        }
        public async Task<MinWage> GetMinWage(int age, DateTime taxYear)
        {
            await SetMinWages();
            return MinWages.Where(mw => mw.TaxYear.Year == taxYear.Year && mw.Age <= age)
                .OrderByDescending(mw => mw.Wage)
                .First();
        }
        public async Task<bool> CanAddMinWage(MinWage dtoMinWage)
        {
             await SetMinWages();
            var existing = MinWages.Any(mw => mw.TaxYear == dtoMinWage.TaxYear && mw.Age == dtoMinWage.Age);
            if (!existing)
            {
                dtoMinWage.TaxYear = new DateTime(dtoMinWage.TaxYear.Year, 04, 06);
               await SetMinWages();
                return true;
            }
            else
            return false;
        }
        private async Task SetMinWages()
        {
            var response = await _httpClient.GetAsync("/api/minwage");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                MinWages = JsonConvert.DeserializeObject<List<MinWage>>(content);
        }

        private async Task AddMinWage(MinWage dtoMinWage)
        {
            var content = JsonConvert.SerializeObject(dtoMinWage);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("/api/minwage", bodyContent);
        }

        bool IMinWageService.CanAddMinWage(MinWage dtoMinWage)
        {
            throw new NotImplementedException();
        }
    }

}
