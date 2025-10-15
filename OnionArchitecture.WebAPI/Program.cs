using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain;
using OnionArchitecture.Domain.Entities.ValueObject;
using OnionArchitecture.infrastructure;
using OnionArchitecture.WebAPI;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MvcOptions>(o =>
{
    o.Filters.Add<UnitOfWorkFilter>();//注册unitofworkfilter
});

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<UserAccessResultEventHandler>();
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISmsCodeSend, MockSmsCodeSend>();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

//获取连接字符串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<UserDBContext>(dbcontect =>
    {
        dbcontect.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });
}
else
{
    throw new InvalidOperationException("No ConnectionString");
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
