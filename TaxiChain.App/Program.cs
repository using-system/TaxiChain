namespace TaxiChain.App
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using TaxiChain.Extensions;

    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                  .ConfigureAppConfiguration((hostingContext, config) =>
                  {
                      config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                      config.AddJsonFile($"appsettings.override.json", optional: true, reloadOnChange: true);

                  })
                  .ConfigureServices((hostContext, services) =>
                  {
                      services.AddOptions();
                      services.AddSingleton<IHostedService, TaxiChainAppService>();
                      services.AddTaxiChain(hostContext.Configuration["Passphrase"]);

                  })
                  .ConfigureLogging((hostingContext, logging) => {
                      logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                      logging.AddConsole();
                  });

            await builder.RunConsoleAsync();
        }
    }
}
