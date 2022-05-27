using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
//using File = System.IO.File;

namespace WpfAppForTelegramBot
{
    internal class TelegramMessageClient
    {
        internal static MainWindow Window { get; } = new();

        //private TelegramBotClient _bot;
        public static ObservableCollection<MessageLog> BotMessageLog { get; set; }

        internal static ITelegramBotClient Bot { get; } = new TelegramBotClient("token");

        internal static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await HandleMessage(botClient, update.Message);
        }

        private static async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {
            var text = $"{DateTime.Now.ToLongTimeString()}: {message.Chat.FirstName} {message.Chat.Id} {message.Text}";

            Debug.WriteLine($"{text} TypeMessage: {message.Type}", "TEST");

            if (message.Text == null) return;

            var messageText = message.Text;

            Window.Dispatcher.Invoke(() =>
            {
                BotMessageLog.Add(
                    new MessageLog(DateTime.Now.ToLongTimeString(), 
                    messageText, message.Chat.FirstName, 
                    message.Chat.Id));
            });

            await botClient.SendTextMessageAsync(message.Chat.Id, $"Test: {message.Text}");
        }

        internal static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                    $"Telegram's API exception: \n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Debug.WriteLine(errorMessage);

            return Task.CompletedTask;
        }

        //private void MessageListener(object sender, Telegram.Bot.Args.ApiResponseEventArgs e)
        //{
        //    var text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

        //    Debug.WriteLine($"{text} TypeMessage: {e.Message.Type.ToString()}");

        //    if (e.Message.Text == null) return;

        //    var messageText = e.Message.Text;

        //    _mainWindow.Dispatcher.Invoke(() =>
        //    {
        //        BotMessageLog.Add(
        //            new MessageLog(
        //                DateTime.Now.ToLongTimeString(), messageText, e.Message.Chat.FirstName, e.Message.Chat.Id));
        //    });
        //}

        //public TelegramMessageClient(MainWindow window, string pathToken = "5031242663:AAFCwwmC0ih-9Xhzh70bzgOitny5o1DkxMk")
        //{
        //    BotMessageLog = new ObservableCollection<MessageLog>();
        //    _mainWindow = window;

        //    _bot = new TelegramBotClient(File.ReadAllText(pathToken));

        //    //_bot.OnMessage += MessageListener;

        //    //_bot.StartReceiving();
        //}

        //public void SendMessage(string text, string id)
        //{
        //    var currentId = Convert.ToInt64(id);
        //    _bot.SendTextMessageAsync(id, text);
        //}
    }
}
