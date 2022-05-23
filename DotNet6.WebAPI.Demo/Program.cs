using Database.EFCore;
using Database.Repository.Implement.EF;
using Database.Repository.Interface;
using Database.UnitOfWork.Implement;
using Database.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Service.Service.Implement;
using Service.Service.Interface;
using Service.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Infrastructrue.Implement;
using Infrastructrue.Interface;
using DotNet6.WebAPI.Demo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Service
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>(); 
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#endregion
#region CORS
string[] corsOrigins = builder.Configuration["AllowedHosts"].Split(',', StringSplitOptions.RemoveEmptyEntries);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            if (corsOrigins.Contains("*"))
            {
                builder.SetIsOriginAllowed(_ => true);
            }
            else
            {
                builder.WithOrigins(corsOrigins);
            }
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
            builder.AllowCredentials();
        });
});
#endregion
#region EFCore
builder.Services.AddDbContext<DemoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion
#region Repository
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion
#region AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<AutoMapperProfile>();
});
#endregion
#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("horizonhorizonhorizon2022"))
    };
});
#endregion
#region OpenAPI Swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Demo API",
        Version = "v1",
        Description = "會員功能測試API",
        Contact = new OpenApiContact()
        {
            Name = "Gino"
        },
        License = new OpenApiLicense()
        {
            Name ="Gino"
        }
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
string isDevelopment = Environment.GetEnvironmentVariable("isDevelopment");
Console.WriteLine($"isDevelopment={isDevelopment}");
string test1 = Environment.GetEnvironmentVariable("test1");
Console.WriteLine($"test1={test1}");
string test2 = Environment.GetEnvironmentVariable("test2");
Console.WriteLine($"test2={test2}");
string test3 = Environment.GetEnvironmentVariable("test3");
Console.WriteLine($"test3={test3}");
if (isDevelopment=="1")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();

app.MigrateDatabase<DemoContext>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
