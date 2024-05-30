using FilmoSearch.Data;
using Microsoft.EntityFrameworkCore;
using FilmoSearch.Services.ActorService;
using FilmoSearch.Middlewares;
using FilmoSearch.Services.FilmService;
using FilmoSearch.Services.ReviewService;
using FilmoSearch.Services.AuthService;
using FilmoSearch.Services.GenreService;
using FilmoSearch.Services.ManageImageService;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;

namespace FilmoSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    builder.Configuration.GetSection("AppSettings:Token").Value!))
                };
            });

            builder.Services.AddHttpContextAccessor();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration).CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiler).Assembly);

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=true";

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    x => x.UseDateOnlyTimeOnly());
            });

            builder.Services.AddScoped<IActorService, ActorService>();
            builder.Services.AddScoped<IFilmService, FilmService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddTransient<IManageImageService, ManageImageService>();
            builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

            var app = builder.Build();

            app.UseSerilogRequestLogging();

            app.UseAuthentication();

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
