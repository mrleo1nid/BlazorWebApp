using System;
using System.Threading.Tasks;
using BlazorWebApp.Server.Data;
using BlazorWebApp.Shared.Auth;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorWebApp.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                    var dbcontext = services.GetRequiredService<ApplicationDbContext>();
                    await RoleInitializer.InitializeAsync(userManager, rolesManager, dbcontext);
                }
                catch (Exception ex)
                { 
                    logger.LogError(ex, "An error occurred while creating roles the database.");
                }
                try
                {
                    var namecontext = services.GetRequiredService<NamesDbContext>();
                    await RoleInitializer.InitNamesContext(namecontext);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while creating names the database.");
                }
            }
            await  host.RunAsync();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                .UseStartup<Startup>()
                .Build();
    }
}
