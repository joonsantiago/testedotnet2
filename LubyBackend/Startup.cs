using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositorys.Context;
using Repositorys.Core;
using Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LubyBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //configuration database connection
            var connection = Configuration["mysql:url"];
            services.AddDbContext<DatabaseContext>(options => options.UseMySql(connection));

            //configuration JWT token
            var key = Encoding.ASCII.GetBytes(Security.Secret.key);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Injeção de dependência no ASP.NET
            services.AddScoped<DatabaseContext, DatabaseContext>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProjectRepository, ProjectsRepository>();
            services.AddTransient<IProjectUserRepository, ProjectUserRepository>();
            services.AddTransient<IWorkHourRepository, WorkHourRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();


            // configuration Swagger
            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Luby ASPNET.CORE",
                        Version = "v1",
                        Description = "Teste de ASPNET.CORE API configurado com o swagger",
                        Contact = new OpenApiContact
                        {
                            Name = "Jonatas Santiago",
                            Url = new Uri("https://github.com/joonsantiago")
                        }
                    });
                // add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Luby Teste ASPNET.CORE V1");
            });
        }

    }
}
