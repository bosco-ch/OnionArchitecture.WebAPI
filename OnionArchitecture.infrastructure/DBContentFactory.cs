using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OnionArchitecture.infrastructure
{
    /// <summary>
    /// 数据库迁移 (使用代码来管理数据库)
    /// 需要安装 Microsof.EntityFrameworkCore.Tools 使用 Migration。需要将根项目设置为启动项目
    /// </summary>
    public class DBContentFactory : IDesignTimeDbContextFactory<UserDBContext>//这边对数据库进行迁移
    {
        public UserDBContext CreateDbContext(string[] args)
        {
            var build
                 = new DbContextOptionsBuilder<UserDBContext>();
            build.UseSqlServer("");//这边放置数据库 链接字符串
            return new UserDBContext(build.Options);

        }
    }
}
