using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Console.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MvcCore
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            // Registering SignalR
            services.AddSignalR();
            //services.AddCors(
            //    options => options.AddPolicy("AllowCors",
            //        builder =>
            //        {
            //            builder
            //                .AllowAnyOrigin()
            //                .AllowCredentials()
            //                .AllowAnyHeader()
            //                .AllowAnyMethod()
            //                .WithOrigins("http://localhost:4200")
            //                .SetIsOriginAllowed((host) => true);
            //        })
            //);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(host => true)
                //.AllowAnyOrigin() // cannot use together with AllowCredentials(), use WithOrigins("https://example.com") instead
            );

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024
            };
            app.UseWebSockets(webSocketOptions);

            // Than register your hubs here with a url.
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chathub");

            });

            app.UseMvc();

        }
    }
}
