namespace WaesDiff.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Serialization;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.IO;
    using System.Xml.XPath;
    using WaesDiff.API.Middlewares;
    using WaesDiff.API.Services;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Services;
    using WaesDiff.Domain.Settings;
    using WaesDiff.Infrastructure.Context;
    using WaesDiff.Infrastructure.Repository;

    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnv;

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            _hostingEnv = env;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(
                    opts =>
                        {
                            opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        });

            // Add API Version
            services.AddApiVersioning();

            services.AddSettings(Configuration);

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Waes Diff", Version = "v1" });
                });

            services.ConfigureSwaggerGen(options =>
                {
                    options.DescribeAllEnumsAsStrings();

                    XPathDocument comments = new XPathDocument($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");
                    options.OperationFilter<XmlCommentsOperationFilter>(comments);
                    options.SchemaFilter<XmlCommentsSchemaFilter>(comments);
                });

            services.AddScoped<IDiffApiService, DiffApiService>();
            services.AddScoped<IDiffService, DiffService>();

            services.AddScoped<IMongoContext<JsonEntity>, JsonContext>();
            services.AddScoped<IJsonRepository, JsonRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            //// specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Waes Diff API V1");
                    c.RoutePrefix = string.Empty;
                });

            app.UseSwagger(action =>
                {
                    action.RouteTemplate = "swagger/{documentName}/swagger.json";
                });

            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMvc();
        }
    }
}
