using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot_11_module.Configuration;
using TelegramBot_11_module.Controllers;
using TelegramBot_11_module.Services;
using TelegramBot_11_module.Utilities;

namespace TelegramBot_11_module
{
    class Program
    {
        static async Task Main() 
        {
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();
            Console.WriteLine("Сервис запущен");
            await host.RunAsync();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings() 
            {
                botToken = "8400908319:AAHK9k-OtOs62wvh2G8yMgCijOd_e7r9pjw" 
            };

        }
        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(appSettings);

            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.botToken));
            services.AddSingleton<InlineKeyboardController>();
            services.AddSingleton<TextMessageController>();
            services.AddSingleton<DefaultMessageController>();
            services.AddHostedService<Bot>();
        }
    }
}
