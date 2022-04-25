using System.Net.Http;
using Newtonsoft.Json;
using IncomeCalculator.Shared.DTO;
using IncomeCalculator.Shared.Interfaces;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;
using IncomeCalculator.Shared.Enums;

namespace IncomeCalculator.WASM.Services
{
    public class MinWageApiService : IMinWageService
    {
        private readonly HttpClient _httpClient;
        private readonly IMessageService _messageService;
        private List<MinWage> _minWages;
        public MinWageApiService(HttpClient httpClient, IMessageService messageService)
        {
            _httpClient = httpClient;
            _messageService = messageService;
            _minWages= new List<MinWage>();
            LoadData();
        }

        public List<MinWage> GetAllMinWages()
        {
            return _minWages;
        }
        public async Task<MinWage> GetMinWageAsync(int age, DateTime taxYear)
        {
            try
            {
                return _minWages.Where(mw => mw.TaxYear.Year == taxYear.Year && mw.Age <= age)
                    .OrderByDescending(mw => mw.Wage)
                    .First();
            }
            catch (InvalidOperationException ex)
            {
                return new MinWage { Wage = 0 };
                throw new Exception($"There doesn't seem to be any min wage data for the year: {taxYear.Year} !");
            }
            catch (Exception ex) { return new MinWage { Wage = 0 }; throw; }
        }
        public async Task AddMinWageAsync(MinWage dtoMinWage)
        {
            var existing = _minWages.Any(mw => mw.TaxYear == dtoMinWage.TaxYear && mw.Age == dtoMinWage.Age);
            if (!existing)
            {
                dtoMinWage.TaxYear = new DateTime(dtoMinWage.TaxYear.Year, 04, 06);
                await AddMinWage(dtoMinWage);
                await _messageService.TostrAlert(MessageType.Success, "Operation successful!");
                LoadData();
            }
             else
                await _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");
        }
        private async void LoadData()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/minwage");
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    _minWages = JsonConvert.DeserializeObject<List<MinWage>>(content);
            }
            catch (Exception ex)
            {
                await _messageService.TostrAlert(IncomeCalculator.Shared.Enums.MessageType.Error, ex.Message);
            }
        }

        private async Task AddMinWage(MinWage dtoMinWage)
        {
                var content = JsonConvert.SerializeObject(dtoMinWage);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync("/api/minwage", bodyContent);
        }

    }

}
