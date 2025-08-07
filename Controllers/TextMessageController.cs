using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using TelegramBot_11_module.Services;
using TelegramBot_11_module.Utilities;

namespace TelegramBot_11_module.Controllers
{
    class TextMessageController
    {
        ITelegramBotClient _telegramBotClient;
        IStorage _memoryStorage;
        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text) 
            {
                case "/start":
                    var keyboardButton = new List<InlineKeyboardButton[]>();
                    keyboardButton.Add(new[] { 
                        InlineKeyboardButton.WithCallbackData("Подсчёт символов в строке", "count"), 
                        InlineKeyboardButton.WithCallbackData("Подсчёт суммы чисел", "sum")
                    });

                    await _telegramBotClient.SendMessage(
                        message.Chat.Id,
                        "что необходимо сделать?",
                        cancellationToken:ct,
                        parseMode: ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(keyboardButton)
                    );
                    break;
                case "/count":
                    _memoryStorage.Action = message.Text.Substring(1);
                    break;
                case "/sum":
                    _memoryStorage.Action = message.Text.Substring(1);
                    break;
                default:
                    switch (_memoryStorage.Action) 
                    {
                        case "count":
                            
                            int strLength = ProcessString.GetLength(message.Text);
                            
                            if (strLength != 0)
                            {
                                await _telegramBotClient.SendMessage(message.Chat.Id, $"В веденой строке {strLength} символов", cancellationToken: ct);
                                return;
                            }

                            await _telegramBotClient.SendMessage(message.Chat.Id, $"Не удалось подсчитать символы", cancellationToken: ct);
                            break;
                        case "sum":
                            
                            int strSum = ProcessString.GetSum(message.Text);

                            if (strSum != 0)
                            {
                                await _telegramBotClient.SendMessage(message.Chat.Id, $"Сумма символов в веденой стороке равна {strSum}", cancellationToken: ct);
                                return;
                            }
                            
                            await _telegramBotClient.SendMessage(message.Chat.Id, $"Введенная строка содержит не верные символы", cancellationToken: ct);
                            break;
                        default:
                            await _telegramBotClient.SendMessage(message.Chat.Id, $"Введенна не верная команда", cancellationToken: ct);
                            break;
                    }
                    break;
            }
        }
    }
}
