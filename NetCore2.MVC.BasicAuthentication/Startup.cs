using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Newtonsoft.Json.Serialization;
using System.Runtime;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Microsoft.ApplicationInsights.Extensibility;
using NetCore2.MVC.BasicAuthentication.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using NetCore2.MVC.BasicAuthentication.Events;

namespace NetCore2.MVC.BasicAuthentication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
               .AddBasic(options =>
               {
                
                   options.Events = new BasicAuthenticationEvents
                   {
                       OnValidateCredentials = context =>
                       {
                           var user = Program.AdminUserList.FirstOrDefault(c => c.name == context.Username && c.password == context.Password);
                           if (user != null)
                           {
                               var claims = new[]
                               {
                                    new Claim(ClaimTypes.NameIdentifier, context.Username, ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                    new Claim(ClaimTypes.Name, context.Username, ClaimValueTypes.String, context.Options.ClaimsIssuer)
                               };

                               context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                               context.Success();
                           }

                           return Task.CompletedTask;
                       }
                   };
               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AlwaysFail", policy => policy.Requirements.Add(new AlwaysFailRequirement()));

            });

            services.AddMvcCore().
                     AddDataAnnotations().
                     AddJsonFormatters();

            services.Configure<GzipCompressionProviderOptions>
                  (options => options.Level = CompressionLevel.Fastest);
            services.AddResponseCompression(options =>
               {
                   options.Providers.Add<GzipCompressionProvider>();
               });

            services.AddMvc((options) =>
            {
                options.CacheProfiles.Add("default", new CacheProfile()
                {
                    Duration = 0,
                    Location = ResponseCacheLocation.None
                });
                options.CacheProfiles.Add("MyCache", new CacheProfile()
                {
                    Duration = 0,
                    Location = ResponseCacheLocation.None
                });
            }).AddJsonOptions(
o =>
{
    CamelCasePropertyNamesContractResolver resorver = new CamelCasePropertyNamesContractResolver();
    resorver.NamingStrategy = null;
    o.SerializerSettings.ContractResolver = resorver;
    o.SerializerSettings.Converters.Add(new StringEnumConverter());
    o.SerializerSettings.Formatting = Formatting.None;
    o.SerializerSettings.NullValueHandling = NullValueHandling.Include;
    o.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
   });

        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {

            //        loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //         loggerFactory.AddConsole();

            app.UseAuthentication();

            var logger = loggerFactory.CreateLogger("Start");
            logger.LogInformation($"- Application started  ");

            string strGName = "";
            if (GCSettings.IsServerGC == true)
                strGName = "server";
            else
                strGName = "workstation";
            logger.LogInformation($"The {strGName} garbage collector is running.");

            app.UseResponseCompression();
            app.UseStaticFiles();

         //   var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
         //   configuration.DisableTelemetry = true;

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
