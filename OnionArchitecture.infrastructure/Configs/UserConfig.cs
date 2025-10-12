using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.infrastructure.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_User");//建立该表
            builder.OwnsOne(x => x.PhoneNumber, nb =>
            {
                nb.Property(b => b.RegionNumber).HasMaxLength(8).IsUnicode(false);
                nb.Property(b => b.number).HasMaxLength(20).IsUnicode(false);
            });
            //配置一对一的关系
            builder.HasOne(b => b.UserAccessFail).WithOne(x => x.user)
                .HasForeignKey<UserAccessFail>(f => f.UserId);
            builder.Property("passwordHash").HasMaxLength(100).IsUnicode(false);
        }
    }
}