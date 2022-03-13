using IncomeCalculator.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncomeCalculator.DAL
{
    public interface IMinWageRepository
    {
        List<MinWage> GetMinWages();
    }
}