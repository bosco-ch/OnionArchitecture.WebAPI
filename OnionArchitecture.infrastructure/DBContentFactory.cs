using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OnionArchitecture.Domain.Entities.ValueObject;

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
            //确认一下环境
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            //构建一下配置
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToString()}.json", optional: true, reloadOnChange: true)
                .AddUserSecrets("19f8a100-1dac-446d-a934-7cf6150caf12")
                .Build();
            //获取一下字符串
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(connectionString);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("数据库字符串未配置");
            }
            var build = new DbContextOptionsBuilder<UserDBContext>();
            //build.UseMySql(testconnect, ServerVersion.AutoDetect(testconnect));//mysql 连接
            build.UseSqlServer(connectionString);//sqlserver 连接
            return new UserDBContext(build.Options);
        }
    }
}
