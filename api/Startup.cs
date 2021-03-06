﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using chat_api.DAL;
using chat_api.Domain.Interfaces;
using System.Reflection;
using System.IO;

namespace chat_api
{
    public class Startup
    {
        private readonly ILogger Logger;
        private string _tipoBD;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {

            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            _tipoBD = Configuration.GetSection("BD")?.GetValue<string>("TIPO")?.ToLower() ?? "mysql";

            if (_tipoBD == "memoria")
            {
                services.AddSingleton(typeof(IRepositorio), typeof(RepositorioEmMemoria));
            }
            else
            {
                var connectionString = CriarConnectionString();
                services.AddSingleton(typeof(IRepositorio), (serviceProvider) => new RepositorioMySql(
                    connectionString,
                    serviceProvider.GetService<ILogger<RepositorioMySql>>()));
            }

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Chat API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            Logger.LogInformation($"Banco de dados: '{_tipoBD}'");

            app.UseCors(opt =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyHeader();
                opt.AllowAnyMethod();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }

        private string CriarConnectionString()
        {
            var configSessao = Configuration.GetSection("BD");

            var servidor = configSessao.GetValue<string>("SERVIDOR");
            var usuario = configSessao.GetValue<string>("USUARIO");
            var senha = configSessao.GetValue<string>("SENHA");
            var database = configSessao.GetValue<string>("DATABASE");

            return $"Server={servidor};Database={database};Uid={usuario};Pwd={senha};";
        }
    }
}
