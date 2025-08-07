using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot_11_module.Controllers
{
    class DefaultMessageController
    {
        ITelegramBotClient _telegramBotClient;
        public DefaultMessageController(ITelegramBotClient telegramBotClient) 
        {
            _telegramBotClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            await _telegramBotClient.SendMessage(message.Chat.Id, "Неизестный тип сообщения", cancellationToken:ct);
            
        }
    }
}
