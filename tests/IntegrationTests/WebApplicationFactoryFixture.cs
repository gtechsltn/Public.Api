﻿using Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Serilog;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using CrossCutting;
using Persistence;
using IntegrationTests.Extensions;

namespace IntegrationTests
{
    public class WebApplicationFactoryFixture(IMessageSink messageSink) : WebApplicationFactory<Program>, IAsyncLifetime
    {
        const bool EnableSqlLogging = false;

        public ITestOutputHelper TestOutputHelper { get; set; } = default!;
        Database Database = default!;

        public async Task InitializeAsync()
        {
            Database = await Database.Create(messageSink);
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseSerilog((context, services, configuration) =>
            {
                Program.ApplySerilogConfiguration(context, services, configuration);
                configuration.WriteTo.TestOutput(TestOutputHelper);
            });

            builder.ConfigureServices(services =>
            {
                services.Configure<Settings>(settings =>
                {
                    settings.SqlServerConnectionString = Database.ConnectionString;
                    settings.Url = "http://localhost";
                });

                #pragma warning disable CS0162 // Unreachable code detected
                if (EnableSqlLogging)
                {
                    services.RemoveDbContextOptions<AppDbContext>();
                    services.AddDbContext<AppDbContext>(options => options.EnableSensitiveDataLogging());
                }
                #pragma warning restore CS0162 // Unreachable code detected
            });

            return base.CreateHost(builder);
        }

        public async new Task DisposeAsync()
        {
            await Database.DisposeAsync();
            await base.DisposeAsync();
        }
    }
}
