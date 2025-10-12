using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.infrastructure.Configs
{
    public class UserAccessFailConfig : IEntityTypeConfiguration<UserAccessFail>
    {
        public void Configure(EntityTypeBuilder<UserAccessFail> builder)
        {
            builder.ToTable("T_AccessFail");
            builder.Property("_locked");
        }
    }
}
