using IncomeCalculator.DAL;
using IncomeCalculator.Data;
namespace MyWebApi.DAL
{
    public class MinWageProxyRepo : IMinWageRepository
    {
        private IMinWageRepository _repository;
        public MinWageProxyRepo(IMinWageRepository minWageRepository)
        {
            _repository = minWageRepository;
        }
        public void AddMinWage(MinWage minWage)
        {
            _repository.AddMinWage(minWage);
        }

        public List<MinWage> GetAllMinWages()
        {
           return _repository.GetAllMinWages();
        }

        public async Task<List<MinWage>> GetAllMinWagesAsync()
        {
           return await _repository.GetAllMinWagesAsync();
        }
    }
}
