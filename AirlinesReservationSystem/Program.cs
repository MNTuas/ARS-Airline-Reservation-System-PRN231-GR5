using BusinessObjects.ResponseModels.Airlines;
using BusinessObjects.ResponseModels.Airplane;
using BusinessObjects.ResponseModels.Airport;
using BusinessObjects.ResponseModels.Flight;
using BusinessObjects.ResponseModels.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Repository.Repositories.AirlineRepositories;
using Repository.Repositories.AirplaneRepositories;
using Repository.Repositories.AirporRepositories;
using Repository.Repositories.AuthRepositories;
using Repository.Repositories.BookingRepositories;
using Repository.Repositories.FlightRepositories;
using Repository.Repositories.PassengerRepositories;
using Repository.Repositories.RankRepositories;
using Repository.Repositories.SeatClassRepositories;
using Repository.Repositories.TicketRepositories;
using Repository.Repositories.TransactionRepositories;
using Repository.Repositories.UserRepositories;
using Service.Mapper;
using Service.Services.AirlineServices;
using Service.Services.AirplaneServices;
using Service.Services.AirportService;
using Service.Services.AuthService;
using Service.Services.BackgroundServices;
using Service.Services.BookingServices;
using Service.Services.EmailServices;
using Service.Services.FlightServices;
using Service.Services.PassengerServices;
using Service.Services.RankServices;
using Service.Services.SeatClassServices;
using Service.Services.TicketServices;
using Service.Services.TransactionServices;
using Service.Services.UserServices;
using Service.Services.VNPayServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
var modelBuilder = new ODataConventionModelBuilder();

// Configure EntitySets for OData
modelBuilder.EntitySet<FlightResponseModel>("flights");
modelBuilder.EntityType<FlightResponseModel>().HasKey(n => n.Id);

modelBuilder.EntitySet<AllAirlinesResponseModel>("airlines");

modelBuilder.EntitySet<AirplaneResponseModel>("airplanes");
modelBuilder.EntitySet<AirplaneSeatResponse>("airplaneseats");

modelBuilder.EntitySet<AirportResponseModel>("airports");

modelBuilder.EntitySet<UserInfoResponseModel>("users");
modelBuilder.EntityType<UserInfoResponseModel>().HasKey(n => n.Id);

// Add OData configuration with Select, Filter, OrderBy, Expand, etc.
builder.Services.AddControllers().AddOData(option => option.Select().Filter()
                .Count().OrderBy().Expand().SetMaxTop(100)
                .AddRouteComponents("odata", modelBuilder.GetEdmModel()));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<EntityUpdateBackgroundService>();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "Jwt",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

//========================================== REPOSITORY ===========================================
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IAirlineRepository, AirlineRepository>();
builder.Services.AddScoped<IAirportRepository, AirportRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IRankRepository, RankRepository>();
builder.Services.AddScoped<IAirplaneRepository, AirplaneRepository>();
builder.Services.AddScoped<ISeatClassRepository, SeatClassRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

//=========================================== SERVICE =============================================
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IAirlineService, AirlineService>();
builder.Services.AddScoped<IAirportService, AirportService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IRankService, RankService>();
builder.Services.AddScoped<IAirplaneService, AirplaneService>();
builder.Services.AddScoped<ISeatClassService, SeatClassService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();

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
app.UseODataBatching();
app.UseRouting();
app.UseAuthentication();

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
