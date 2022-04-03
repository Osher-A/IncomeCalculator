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
        private readonly IMessageService _messageService;
        public List<MinWage>? MinWages { get; set; }
        public MinWageApiService(HttpClient httpClient, IMessageService messageService)
        {
            _httpClient = httpClient;
            _messageService = messageService;
            //Task.Run(() => SetMinWages()).Wait();
        }
        public async Task<MinWage> GetMinWage(int age, DateTime taxYear)
        {
            try
            {
                await SetMinWages();
                return MinWages.Where(mw => mw.TaxYear.Year == taxYear.Year && mw.Age <= age)
                    .OrderByDescending(mw => mw.Wage)
                    .First();
            }
            catch (ArgumentNullException ex)
            {
                await _messageService.TostrAlert(IncomeCalculator.Shared.Enums.MessageType.Error, "There doesn't seem to be any data for your query!");
                return new MinWage { Wage = 0 };
            }
            catch (Exception ex )
            {
                await _messageService.TostrAlert(IncomeCalculator.Shared.Enums.MessageType.Error, ex.Message);
                return new MinWage { Wage = 0};
            }
        }
        public async Task<bool> CanAddMinWage(MinWage dtoMinWage)
        {
            await SetMinWages();
            var existing = MinWages.Any(mw => mw.TaxYear == dtoMinWage.TaxYear && mw.Age == dtoMinWage.Age);
            if (!existing)
            {
                dtoMinWage.TaxYear = new DateTime(dtoMinWage.TaxYear.Year, 04, 06);
                return true;
            }
                 else
                await _messageService.SweetAlert("Information", "There already exists a record for the specified tax year and age!");

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

    }

}
