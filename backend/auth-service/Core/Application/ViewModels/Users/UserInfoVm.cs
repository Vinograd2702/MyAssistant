using auth_servise.Core.Application.Common.Mappings;
using auth_servise.Core.Domain;
using AutoMapper;

namespace auth_servise.Core.Application.ViewModels.Users
{
    public class UserInfoVm : IMapWith<User>
    {
        public Guid Id { get; set; }
        public string? Login { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserInfoVm>()
                .ForMember(uvm => uvm.Id,
                opt => opt.MapFrom(u => u.Id))
                .ForMember(uvm => uvm.Login,
                opt => opt.MapFrom(u => u.Login))
                .ForMember(uvm => uvm.FirstName,
                opt => opt.MapFrom(u => u.FirstName))
                .ForMember(uvm => uvm.LastName,
                opt => opt.MapFrom(u => u.LastName))
                .ForMember(uvm => uvm.Patronymic,
                opt => opt.MapFrom(u => u.Patronymic))
                .ForMember(uvm => uvm.PhoneNumber,
                opt => opt.MapFrom(u => u.PhoneNumber));
        }
    }
}
