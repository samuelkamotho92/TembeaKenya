using BookingService.Data;
using BookingService.Extensions;
using BookingService.Services;
using BookingService.Services.IService;
using BookingService.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.AddAuth();
builder.AddSwaggenGenExtension();

builder.Services.AddDbContext<BookingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IBookingService, BookingServices>();
builder.Services.AddScoped<ICoupon, CouponService>();
builder.Services.AddScoped<ITour,TourService>();
builder.Services.AddScoped<IHotel, HotelService>();
builder.Services.AddScoped<IUser,UserService>();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddHttpClient("Tours", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:TourService"))).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
builder.Services.AddHttpClient("Coupons", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:CouponService"))).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
builder.Services.AddHttpClient("Hotels", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:HotelService"))).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
//builder.Services.AddHttpClient("Users", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:UserService")));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
