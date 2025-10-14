using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OnionArchitecture.Domain.Entities.ValueObject;
using StackExchange.Redis;

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
            ////确认一下环境
            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            ////构建一下配置
            //IConfiguration configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
            //    .AddJsonFile($"appsetting.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToString()}.json", optional: true, reloadOnChange: true)
            //    .AddUserSecrets("19f8a100-1dac-446d-a934-7cf6150caf12")
            //    .Build();
            ////获取一下字符串


            //string? connectionString = configuration.GetConnectionString("DefaultConnection")?.ToString();
            //if (string.IsNullOrEmpty(connectionString))
            //{
            //    throw new InvalidOperationException("No ConnectString:未发现连接字符串");
            //}

            string testconnect = "Server=192.168.31.118;Database=OnionArchitecture;Uid=root;Pwd=Panchengqi521#;TrustServerCertificate=true;";
            var build = new DbContextOptionsBuilder<UserDBContext>();
            build.UseMySql(testconnect, ServerVersion.AutoDetect(testconnect));
            return new UserDBContext(build.Options);
        }
    }
}
