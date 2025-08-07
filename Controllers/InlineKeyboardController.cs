using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot_11_module.Configuration;
using TelegramBot_11_module.Services;
using TelegramBot_11_module.Utilities;

namespace TelegramBot_11_module.Controllers
{
    class InlineKeyboardController
    {
        ITelegramBotClient _telegramBotClient;
        IStorage _memoryStorage;
        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage) 
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            Console.WriteLine($"Нажата конпка {callbackQuery.Data}");
            //await _telegramBotClient.SendMessage(callbackQuery.From.Id, $"Обнаружено нажатие на кнопку {callbackQuery.Data}", cancellationToken: ct);
            //Сохраняем в переменную ТО действие, которое необходимо выполнить над строкой
            _memoryStorage.Action = callbackQuery.Data;
        }
    }
}
