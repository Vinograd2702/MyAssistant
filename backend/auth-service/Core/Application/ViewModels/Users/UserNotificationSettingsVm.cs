using auth_servise.Core.Application.Common.CommonObjects;
using auth_servise.Core.Application.Common.Mappings;
using auth_servise.Core.Domain;
using AutoMapper;

namespace auth_servise.Core.Application.ViewModels.Users
{
    public class UserNotificationSettingsVm : IMapWith<UserSettings>
    {
        public bool IsAcceptEmailNotification { get; init; }
        public bool IsAcceptPushNotification { get; init; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserSettings,
                UserNotificationSettingsVm>()
                .ForMember(uvm => uvm.IsAcceptEmailNotification,
                opt => opt.MapFrom(u => u.IsUseEmailToNotificate))
                .ForMember(uvm => uvm.IsAcceptPushNotification,
                opt => opt.MapFrom(u => u.IsUsePushToNotificate));
        }
    }
}
