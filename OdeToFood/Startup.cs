using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using OdeToFood.Services;
using OdeToFood.Data;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //register your custome services here
            services.AddSingleton<IGreeter, Greeter>();
            services.AddDbContext<OdeToFoodDbContext>(
                    options => options.UseSqlServer(_configuration.GetConnectionString("OdeToFood")));
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IGreeter greeter, ILogger<Startup> logger)
        {
            //ASPNETCORE_ENVIRONMENT
            if (env.IsDevelopment())
            {                             
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(ConfigureRoutes);


            //Order of middleware is required
            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            //app.UseFileServer();


            #region commented
            /*
             app.Use(next =>
             {
                 return async context =>
                 {
                     logger.LogInformation("Request incoming");
                     if (context.Request.Path.StartsWithSegments("/mym"))
                     {
                         await context.Response.WriteAsync("Hit!!");
                         logger.LogInformation("Request handled");
                     }
                     else
                     {
                         await next(context);
                         logger.LogInformation("Request outgoing");
                     }
                 };
             });

             app.UseWelcomePage(new WelcomePageOptions
             {
             Path = "/wp"
             });
             */
            #endregion

            app.Run(async (context) =>
            {
                //throw new Exception("error !");
                var greeting = greeter.GetMessageOfTheDay();
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Not found");
                //await context.Response.WriteAsync($"{greeting} : {env.EnvironmentName}");
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // /Home/Index/4
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
