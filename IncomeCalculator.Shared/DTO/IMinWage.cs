
namespace IncomeCalculator.Shared.DTO
{
    public interface IMinWage
    {
        int? Age { get; set; }
        int Id { get; set; }
        DateTime TaxYear { get; set; }
        decimal? Wage { get; set; }
    }
}