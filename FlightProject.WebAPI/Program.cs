using System.Security.Claims;
using System.Text;
using FlightProject.Business.Extensions;
using FlightProject.Business.MappingProfiles;
using FlightProject.Business.ValidationRules.FluentValidation;
using FlightProject.Core.Extensions;
using FlightProject.DataAccess.Context;
using FlightProject.DataAccess.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FlightProject.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add  Services
            builder.Services.AddDataAccessServices();
            builder.Services.AddBusinessServices();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Add DbContext
            //builder.Services.AddDbContext<FlightDbContext>(opt =>
            //    opt.UseSqlServer(builder.Configuration.GetConnectionString("FlightDb")));
            builder.Services.AddDbContext<FlightDbContext>();

            builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
                policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            builder.Services.AddControllers();

            //Veritabanýndan çekilen verilerde include edilen veriler sonsuz döngü hatasýna giriyor, bu kod parçasý sonsuz döngüyü engelliyor.
            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            //Add AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            //FluentValidation
            builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<UserRegisterValidation>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer ...token')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            }); //swagger arayüzünü ve token isteyen istekler için jwt implementasyonu yapabileceðimiz config

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //JWT implementasyonu
                .AddJwtBearer(options => options.TokenValidationParameters = new()
                {
                    ValidateAudience =
                true, //oluþturulacak token deðerini kimlerin/hangi originlerin/sitelerin kullanacagýný belirlediðimiz deðerdir. Örn www.ugurcimen.com
                    ValidateIssuer = true, //oluþturulan tokeni kimin daðýttýðýný bildirdiðimiz alandýr. Örn www.myapi.com
                    ValidateLifetime = true, //oluþturulan tokenin süresini kontrol edecek olan alandir.
                    ValidateIssuerSigningKey =
                true, //Üretilecek token deðerinin uygulamamýza ait olduðunu bildiren bir deðer olduðunu ifade eden securityKey verisinin doðrulamasýdýr.

                    ValidAudience = builder.Configuration["TokenOptions:Audience"],
                    ValidIssuer = builder.Configuration["TokenOptions:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenOptions:SecurityKey"])),
                    NameClaimType = ClaimTypes.Name
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}