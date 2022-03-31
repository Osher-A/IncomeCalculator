using AutoMapper;
using IncomeCalculator.Shared;
namespace IncomeCalculator.Helper
{
    public class DTOConverter : Profile
    {
        public DTOConverter()
        {
            CreateMap<Shared.DTO.MinWage, Data.MinWage>().ReverseMap();
            CreateMap<Shared.DTO.WorkingTaxCredit, Data.WorkingTaxCredit>().ReverseMap();
            CreateMap<Shared.DTO.ChildTaxCredit, Data.ChildTaxCredit>().ReverseMap();
        }
    }
}
