using _MeetHoper_ServerDemo.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using _MeetHoper_ServerDemo.Extensions;
using Common;
using _MeetHoper_ServerDemo.Services;
using Common.Abstractions;
using _MeetHoper_ServerDemo.Models;
using _MeetHoper_ServerDemo.Interfaces;
using Common.Models.DTOs;
using Common.Models.Responses;

namespace _MeetHoper_ServerDemo
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("secretData.json");

            _configuration = builder.Build();
        }

        public IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SaltSecret>(_configuration);

            DbContextSeed.AddDbContext(services, _configuration);
            services.AddJwtSwaggerAuthentication(_configuration);
            services.AddSingleton<PasswordHasher>();
            services.AddSingleton<ICashService<Geoposition, UserPublicDataResponse>, GeolocationCache>();
            services.AddSingleton<IDataBaseUserHandler, ApplicationContextService>();
            services.AddSingleton<IUserGeoNavigationService, UserGeoNavigationService>();
            services.AddSingleton<UserCredentialsService>();
            services.AddMemoryCache();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = Constants.SwaggerTitle, Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
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

            });

            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = _configuration.GetConnectionString("Redis");
            //    options.InstanceName = "RedisDemo_";
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Constants.SwaggerTitle} v1"));

            DbContextSeed.Initialize(serviceProvider);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
