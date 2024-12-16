using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using BarBreak.Infrastructure; // Додайте простір імен для вашого сервісу
using Microsoft.EntityFrameworkCore;


namespace BarBreak.Presentation
{
    public partial class App : System.Windows.Application
    {
        // Додайте поле для DI контейнера
        private static IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Налаштування логування
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            try
            {
                // Налаштування DI контейнера
                var serviceCollection = new ServiceCollection();

                // Реєстрація контексту бази даних
                serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql("Host=localhost;Port=5432;Database=BarBreakDb;Username=postgres;Password=1909"));

                // Реєстрація сервісу для тестування бази даних
                serviceCollection.AddScoped<DatabaseTesterService>();

                // Побудова контейнера
                _serviceProvider = serviceCollection.BuildServiceProvider();

                // Виклик методу для перевірки підключення до бази даних
                var databaseTester = _serviceProvider.GetRequiredService<DatabaseTesterService>();
                databaseTester.TestConnectionAsync().GetAwaiter().GetResult();

                // Інформація для логування
                Log.Information("Application Starting Up");

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to start correctly");
                throw;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Information("Application Shutting Down");
            Log.CloseAndFlush();
            base.OnExit(e);
        }
    }
}
