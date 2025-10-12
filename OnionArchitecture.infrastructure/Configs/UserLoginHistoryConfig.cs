using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.infrastructure.Configs
{
    public class UserLoginHistoryConfig : IEntityTypeConfiguration<UserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
        {
            builder.ToTable("T_UserLoginHistory");
            //物理上不建立他的外键，因为他的表是相互独立的
            //也有可能会在不同的数据库中，也就是在不同的聚合中（即时在同一个聚合中其实也可以没有外键，除非关系很紧密）。
            //方便做拆分，如果不小心建立了外键，使用sql脚本来删除外键，可以在网上搜索资料
            builder.OwnsOne(b => b.phoneNumber, q =>
            {
                q.Property(b => b.RegionNumber).HasMaxLength(8).IsUnicode(false);
                q.Property(b => b.number).HasMaxLength(20).IsUnicode(false);
            });

        }
    }
}
