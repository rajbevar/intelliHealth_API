using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using PatientEngTranscription.DataAccess;
using PatientEngTranscription.Service;
using AutoWrapper;
using Amazon.ComprehendMedical;
using PatientEngTranscription.Api.Extensions;
using PatientEngTranscription.Shared.Configurations;
using PatientEngTranscription.Api.Middleware;
using Microsoft.AspNetCore.Http.Internal;
using PatientEngTranscription.Api.HubConfig;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Internal;
using System.Linq;
using System;

namespace PatientEngTranscription.Api
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; }
        /// <summary>
        /// current activated environment
        /// </summary>
        public IHostingEnvironment Environment { get; }

        //HttpConfiguration config = new HttpConfiguration();

        string WebUrl = "*";
        string CorsMethod = "GET,PUT,POST,DELETE,OPTIONS";


        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Environment = env;

            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();

            Configuration = builder.Build();




        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    //.AllowAnyOrigin()
                    .WithOrigins("http://localhost:8000",
                    "https://patientengweb.azurewebsites.net")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                   .AllowCredentials()
                    );
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IConfigurationRoot>(Configuration);


            //load all configuration settings from the applications.json
            services.ConfigureRootConfiguration(Configuration, Environment);
            var rootConfiguration = services.BuildServiceProvider().GetService<RootConfiguration>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddCors();



            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "PatientEngTranscriptionApi", Version = "v1" });
            });

          //  services.AddOptions();

            // Add our Config object so it can be injected


            
            services.AddMvc()
               .AddJsonOptions(
                   options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
               );

            //AWS configuation settings
            //var awsOptions = Configuration.GetAWSOptions();
            //services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            //services.AddAWSService<IAmazonComprehendMedical>();
            //  var textractTextService = new TextractTextDetectionService(awsOptions.CreateServiceClient<IAmazonTextract>());
            //var comprehendMedicalService = new ComprehendService(awsOptions.CreateServiceClient<IAmazonComprehendMedical>());
            // AmazonComprehendMedicalClient




            // Add useful interface for accessing the IUrlHelper outside a controller.
            services.AddScoped<IUrlHelper>(x => x
                    .GetRequiredService<IUrlHelperFactory>()
                    .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));

            services.AddMvcCore();

            services.AddDbContext<PatientEngTranscriptionContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PatientEngTranscriptionConnection")))
                .AddUnitOfWork<PatientEngTranscriptionContext>();

            var Mapperconfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = Mapperconfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddServices();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //  app.UseHsts();
            }
            //app.UseMiddleware<CustomExceptionMiddleware>();
            //app.Use(async (context, next) =>
            //{
            //    context.Request.EnableRewind();
            //    await next();
            //});
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
           
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WearableApi V1");
            });
            //app.UseApiResponseAndExceptionWrapper();
            app.UseHttpsRedirection();


            //app.UseCors(options =>
            //{
            //    options
            //    .AllowAnyOrigin()
            //    //.WithOrigins("http://localhost:8000") 
            //    //.WithOrigins("https://patientengweb.azurewebsites.net")
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    //.AllowCredentials()                
            //    ;
            //});

            //app.UseExceptionHandler(config =>
            //{
            //    config.Run(async context =>
            //    {
            //        var error = context.Features.Get<IExceptionHandlerFeature>();

            //        context.Response.StatusCode = 500;
            //        context.Response.ContentType = "application/json";


            //        if (error != null)
            //        {
            //            var ex = error.Error;
            //            var err = new ApiError(ex.Message)
            //            {
            //                Details = env.IsDevelopment() ? ex.ToString() : null
            //            };

            //            var jsonError = JsonConvert.SerializeObject(err);

            //            await context.Response.WriteAsync(jsonError);
            //        }
            //    });
            //});


            app.UseSignalR(routes =>
            {
                routes.MapHub<ChartHub>("/chart");

            });
         
            //app.Map("/chart", map =>
            //{
            //    var corsOption = new CorsOptions
            //    {
            //        DefaultPolicyName= "CorsPolicy1"
            //    };

                
            //    corsOption.AddPolicy("CorsPolicy1", new CorsPolicy()
            //    {
            //        SupportsCredentials = true,
                    
                    
            //    });
            //    map.UseCors(corsOption.DefaultPolicyName);

            //});

            app.UseMvc();
        }
    }
}
