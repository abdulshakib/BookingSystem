using BookingSystem.Application.Commands.Booking;
using BookingSystem.Application.Commands.InventoryItem;
using BookingSystem.Application.Commands.Member;
using BookingSystem.Application.Handler.Booking;
using BookingSystem.Application.Handler.InventoryItem;
using BookingSystem.Application.Handler.Member;
using BookingSystem.Application.Interfaces;
using BookingSystem.Application.Query;
using BookingSystem.Application.Services;
using BookingSystem.Common;
using BookingSystem.Domain.Data;
using BookingSystem.Domain.Repositories;
using BookingSystem.Infrastructure.IRepositories;
using BookingSystem.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


builder.Services.AddOpenApi();


var connectionString = builder.Configuration.GetConnectionString("SQLiteConnection");
builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Register IRequestHandler implementations
builder.Services.AddScoped<IRequestHandler<BookItemCommand, BookItemResult>, BookItemCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CancelBookingCommand, Unit>, CancelBookingCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UploadInventoryCsvCommand, Unit>, UploadInventoryCsvCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UploadMemberCsvCommand, Unit>, UploadMemberCsvCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllMembersQuery, List<Member>>, GetAllMembersQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetBookingsByMemberQuery, List<Booking>>, GetBookingsByMemberQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllInventoryQuery, List<InventoryItem>>, GetAllInventoryQueryHandler>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Booking API",
        Version = "v1",
        Description = "API for Booking System"
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

