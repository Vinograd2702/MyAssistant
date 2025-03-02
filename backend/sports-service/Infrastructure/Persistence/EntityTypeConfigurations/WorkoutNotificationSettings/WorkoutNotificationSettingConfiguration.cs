using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.WorkoutNotificationSettings;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.WorkoutNotificationSettings
{
    public class WorkoutNotificationSettingConfiguration
        : IEntityTypeConfiguration<WorkoutNotificationSetting>
    {
        public void Configure(EntityTypeBuilder<WorkoutNotificationSetting> buidler)
        {
            buidler.HasKey(e => e.Id);
            buidler.HasIndex(e => e.Id).IsUnique();
            buidler.Property(e => e.UserId).IsRequired();
        }
    }
}
