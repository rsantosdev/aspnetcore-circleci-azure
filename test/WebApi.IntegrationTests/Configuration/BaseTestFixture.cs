using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using WebApi.Models;

namespace WebApi.IntegrationTests.Configuration
{
    public class BaseTestFixture : IDisposable
    {
        public readonly TestServer Server;
        public readonly HttpClient Client;
        public readonly MyDbContext TestDbContext;
        public readonly IConfigurationRoot Configuration;

        public BaseTestFixture()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var opts = new DbContextOptionsBuilder<MyDbContext>();
            opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            TestDbContext = new MyDbContext(opts.Options);
            SetupDatabase();

            Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = Server.CreateClient();
        }

        private void SetupDatabase()
        {
            try
            {
                TestDbContext.Database.EnsureCreated();
                TestDbContext.Database.Migrate();
            }
            catch (Exception)
            {
                //TODO: Add a better logging
                // Does nothing
            }
        }

        public void Dispose()
        {
            TestDbContext.Dispose();
            Client.Dispose();
            Server.Dispose();
        }
    }
}
