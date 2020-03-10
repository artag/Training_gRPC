using AutoMapper;

namespace GrpcServer.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Common.Customer, CustomerModel>()
                .ForMember(m => m.EmailAddress, m => m.MapFrom(c => c.Email))
                .ReverseMap();
        }
    }
}
