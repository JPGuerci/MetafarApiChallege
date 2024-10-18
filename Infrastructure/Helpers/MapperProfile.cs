using AutoMapper;
using MetafarApiChallege.Infrastructure.Dtos;
using MetafarApiChallege.Infrastructure.Repositories.Models;

namespace MetafarApiChallege.Infrastructure.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Account, AccountDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => (src.Name + " " + src.Surname)))
               .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
               .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.CurrentBalance));

            CreateMap<Operation, OperationDto>()
                .ForMember(dest => dest.OperationNumber, opt => opt.MapFrom(src => Math.Abs(src.Id.GetHashCode())))
                .ForMember(dest => dest.OperationType, opt => opt.MapFrom(src => src.OperationType.Name))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.AccountNumber))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.FinalAmount))
                .ReverseMap();

            CreateMap<TransferRequest, TransferDto>().ReverseMap();

            CreateMap<OperationDto, TransferResponse>().ReverseMap();

        }
    }
}
