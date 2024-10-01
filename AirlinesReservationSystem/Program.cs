using Repository.Repositories.AirlineRepositories;
using Repository.Repositories.AirporRepositories;
using Repository.Repositories.FlightRepositories;
using Repository.Repositories.RankRepositories;
using Service.Services.AIrlineServices;
using Service.Services.AirportService;
using Service.Services.EmailServices;
using Service.Services.FlightServices;
using Service.Services.RankServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//========================================== REPOSITORY ===========================================
builder.Services.AddTransient<IFlightRepository, FlightRepository>();
builder.Services.AddTransient<IAirlineRepository, AirlineRepository>();
builder.Services.AddScoped<IAirportRepository, AirportRepository>();
builder.Services.AddScoped<IRankRepository, RankRepository>();

//=========================================== SERVICE =============================================
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IAirlineService, AirlineService>();
builder.Services.AddScoped<IAirportService, AirportService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IRankService, RankService>();


//=========================================== CORS ================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                      policy =>
                      {
                          policy
                          //.WithOrigins("http://localhost:3000")
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                          //.AllowCredentials();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
