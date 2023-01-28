using Cybersoft.ApplicationCore.Common.Mapping;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Services;
using Cybersoft.Authentication;
using Cybersoft.Infrastructure.Data;
using Cybersoft.Infrastructure.Logging;
using Cybersoft.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Cybersoft
{
    public class Startup
    {
        private readonly IHostEnvironment _currentEnvironment;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            _currentEnvironment = env;
        }



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            // Indicate how ClientConfiguration options should be constructed
            var clientConfigurationSection = Configuration.GetSection("ClientConfiguration");
            var clientConfiguration = clientConfigurationSection.Get<ClientConfigurationModel>();
            services.Configure<ClientConfigurationModel>(clientConfigurationSection);
            services.PostConfigure<ClientConfigurationModel>(config =>
            {
                config.EnvironmentName = _currentEnvironment.EnvironmentName;
            });

            ConfigureAuth(services, clientConfiguration);
            ConfigureDatabase(services);
            services.AddAuthorization();
            var key = Configuration.GetValue<string>("Jwt:SecretKey");
            services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddSingleton<IJwtAuthenticationManager>(x => new JwtAuthenticationManager(key, x.GetService<IRefreshTokenGenerator>()));
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<ICartService, CartService>();

            services.AddMemoryCache();

            services.AddMappings(); // this is for mapster

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            });

            services.AddControllers();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "WebApp/cybersoft/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (_currentEnvironment.IsDevelopment() || _currentEnvironment.IsEnvironment("Test"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseDeveloperExceptionPage();

            }

            app.ConfigureExceptionMiddleware();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".webmanifest"] = "application/manifest+json";

            app.UseSpaStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = provider
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("X-Xss-Protection", "1");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next().ConfigureAwait(false);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (!_currentEnvironment.IsEnvironment("Test"))
            {
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "WebApp/cybersoft";

                    if (_currentEnvironment.IsDevelopment())
                    {
                        const string baseUri = "http://localhost:4200/";
                        spa.UseProxyToSpaDevelopmentServer(baseUri);
                    }
                });
            }
        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            if (_currentEnvironment.IsEnvironment("Test"))
            {
                return;
            }
            else
            {
                services.AddSqlServer<CyberSoftContext>(Configuration.GetConnectionString("CybersoftDatabase"));

            }
        }

        protected virtual void ConfigureAuth(IServiceCollection services, ClientConfigurationModel clientConfiguration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Jwt:SecretKey"))),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = Configuration.GetValue<string>("Jwt:ValidIssuer"),
                    ValidAudience = Configuration.GetValue<string>("Jwt:ValidAudience"),

                };
            });
        }
    }


}
