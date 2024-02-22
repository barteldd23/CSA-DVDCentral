using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BDF.Bingo.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ServiceProvider serviceProvider;
        public static IConfiguration Configuration;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // Bring the appsettings.json
            var configSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Configuration = configSettings;

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configSettings)
            //    .CreateLogger();

            services.AddSingleton<BingoCard>();
            //    .AddLogging(c => c.AddConsole())
            //    .AddLogging(c => c.AddDebug());

        }

        private void OnStartUp(object sender, StartupEventArgs e)
        {
            var BingoCard = serviceProvider.GetService<BingoCard>();
            BingoCard.Show();
        }
    }
}
