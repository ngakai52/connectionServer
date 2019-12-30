using System;
using System.Collections.Generic;
using System.Text;
using ConnectionServer.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectionServer
{
    public class Startup
    {
        private readonly string myAllowSpecificOrigins = "MyAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddSignalR();
                services.AddCors(options =>
                    {
                        options.AddPolicy(myAllowSpecificOrigins, builder =>
                        {
                            builder.WithOrigins("http://localhost:14999/", "http://192.168.10.73:14999")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                        });
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            try
            {
                app.UseRouting();
                app.UseAuthorization();
                app.UseCors(myAllowSpecificOrigins);
                app.UseEndpoints(endpoints => { endpoints.MapHub<ChatHub>("/chatHub"); });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
