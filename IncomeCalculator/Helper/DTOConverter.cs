using AutoMapper;
namespace IncomeCalculator.Helper
{
    public class DTOConverter : Profile
    {
        public DTOConverter()
        {
            CreateMap<DTO.MinWage, Data.MinWage>().ReverseMap();
            CreateMap<DTO.WorkingTaxCredit, Data.WorkingTaxCredit>().ReverseMap();
            CreateMap<DTO.ChildTaxCredit, Data.ChildTaxCredit>().ReverseMap();
        }
    }
}
