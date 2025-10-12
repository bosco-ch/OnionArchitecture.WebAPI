using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain;
using OnionArchitecture.infrastructure;
using OnionArchitecture.WebAPI;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDBContext>(dbcontect =>
{
    dbcontect.UseSqlServer("Á¬½Ó×Ö·û´®");
});
builder.Services.Configure<MvcOptions>(o =>
{
    o.Filters.Add<UnitOfWorkFilter>();//×¢²áunitofworkfilter
});
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<UserAccessResultEventHandler>();
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISmsCodeSend,MockSmsCodeSend>();
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
