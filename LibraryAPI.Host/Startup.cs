using LibraryAPI.BusinessLogic.Contracts;
using LibraryAPI.BusinessLogic.Options;
using LibraryAPI.BusinessLogic.Services;
using LibraryAPI.BusinessLogic.Services.Auth;
using LibraryAPI.DataAccess;
using LibraryAPI.DataAccess.Contracts;
using LibraryAPI.DataAccess.EFCore;
using LibraryAPI.Presentation.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryAPI.Host
{
    public class Startup
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpContextAccessor();

            // Register services
            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("SqlConnection")));

            services.AddOptions<AuthOptions>()
                .Bind(_configuration.GetSection(nameof(AuthOptions)))
                .ValidateDataAnnotations();

            services.AddSingleton<StartupFinalizer>();

            services.AddScoped<ITokenReader, BusinessLogic.Services.Auth.TokenReader>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookService, BooksService>();
            services.AddScoped<ICSVGenerator, CSVGenerator>();

            // Add mapper
            services.AddAutoMapper(config => config.AddMaps(typeof(BusinessLogic.Mapping.AuthProfile).Assembly,
                                                            typeof(Presentation.Mapping.AuthProfile).Assembly));

            services.AddControllers()
                    .AddApplicationPart(typeof(AuthController).Assembly)
                    .AddControllersAsServices()
                    .AddNewtonsoftJson()
                    .AddMvcOptions(options => options.Filters.Add<ExceptionFilter>());

            services.AddSwaggerGen();

            // Add token
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var authOptions = new AuthOptions();
                _configuration.Bind(nameof(AuthOptions), authOptions);

                options.RequireHttpsMetadata = _environment.IsDevelopment();
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.Secret)),
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StartupFinalizer startupFinalizer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors(builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });

                app.UseSwagger();
                app.UseSwaggerUI(options=> 
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty; 
                });
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            startupFinalizer.SeedDatabaseData();
        }
    }
}
