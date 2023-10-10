using Multiplex.Business.Services;
using Multiplex.Business.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityServer4.Validation;
using FluentValidation.Results;
using Multiplex.IdentityServer;
using Multiplex.Domain.Contexts.AutoGenerated;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Http.Features;
using Multiplex.Business.DTOs;

namespace Multiplex
{
    public class Startup
    {
        readonly string AllowSpecificOrigins = "corsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            InjectServices(services);
            // servidor de correos
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailSender, EmailSender>();
            //
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<MultiplexContext>(options => options.UseSqlServer(Configuration.GetConnectionString("default"), sqlServerOptions => sqlServerOptions.CommandTimeout(120)), ServiceLifetime.Transient);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Invoice API", Version = "v1" });
            });

            var origins = Configuration.GetSection("AllowedOrigins").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            services.AddHttpClient();
            var authority = Configuration.GetValue<string>("Authority");
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer("Bearer", options =>
               {
                   options.Authority = authority;
                   options.RequireHttpsMetadata = false;
                   options.Audience = "multiplexAPI";
               });

            services.AddSignalR();

            var builderIS = services.AddIdentityServer(options =>
            {
                options.IssuerUri = authority;
            });

            builderIS.AddDeveloperSigningCredential(true);

            builderIS.AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApis())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            services.AddMvc();
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = 1000 * 1048576; 
                x.MultipartBodyLengthLimit = 1000 * 1048576;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice API V1");
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(AllowSpecificOrigins);
            });

        }
        private void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IUsuariosService, UsuariosService>();
            services.AddTransient<IPeliculasService, PeliculasService>();
            services.AddTransient<ISeriesService, SeriesService>();
            services.AddTransient<ITaxonomyService, TaxonomyService>();
            services.AddTransient<IHistorialPeliculasService, HistorialPeliculasService>();
        }
    }
}
