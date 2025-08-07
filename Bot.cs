using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot_11_module.Controllers;
using TelegramBot_11_module.Configuration;

class Bot : BackgroundService
{
    ITelegramBotClient _telegramBotClient;
    TextMessageController _textMessageController;
    InlineKeyboardController _inlineKeyboardController;
    DefaultMessageController _defaultMessageController;
    AppSettings _appSettings;

    public Bot(
        ITelegramBotClient telegramBotClient,
        TextMessageController textMessageController,
        InlineKeyboardController inlineKeyboardController,
        DefaultMessageController defaultMessageController,
        AppSettings appSettings
        )
    {
        _telegramBotClient = telegramBotClient;
        _textMessageController = textMessageController;
        _inlineKeyboardController = inlineKeyboardController;
        _defaultMessageController = defaultMessageController;
        _appSettings = appSettings;
    }

    protected override async Task ExecuteAsync( CancellationToken ct)
    {
        _telegramBotClient.StartReceiving(HandleUpdateSync, HandleErrorAsync, new ReceiverOptions() { AllowedUpdates = { } }, cancellationToken: ct);
        Console.WriteLine("Bot started");
    }

    async Task HandleUpdateSync(ITelegramBotClient telegramBotClent, Update update, CancellationToken ct) 
    {
        
        if (update.Type == UpdateType.CallbackQuery)
        {
            _inlineKeyboardController.Handle(update.CallbackQuery, ct);
            return;
        }

        if (update.Type == UpdateType.Message)
        {
            switch (update.Message!.Type)
            {
                case MessageType.Text:
                    await _textMessageController.Handle(update.Message, ct);
                    return;
                default:
                    await _defaultMessageController.Handle(update.Message, ct);
                    return;
            }   
        }
    }

    Task HandleErrorAsync(ITelegramBotClient telegramBotClient, Exception exception, CancellationToken ct)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
        Thread.Sleep(10000);
        return Task.CompletedTask;
    }
}



//Бот должен иметь две функции: подсчёт количества символов в тексте и вычисление суммы чисел,
//которые вы ему отправляете (одним сообщением через пробел).

//То есть в ответ на условное сообщение «сова летит» он должен прислать что-то вроде «в вашем сообщении 10 символов».
//А в ответ на сообщение «2 3 15» должен прислать «сумма чисел: 20».

//Выбор одной из двух функций должен происходить на старте в «Главном меню». При старте (через /start) бот
//должен присылать клиенту ответное сообщение — меню с кнопками, из которого можно выбрать,
//какое действие пользователь хочет выполнить (по аналогии с тем, как мы выбирали язык в VoiceTexterBot).