using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace WpfAppForTelegramBot
{
    internal class TelegramMessageClient
    {
        private MainWindow _mainWindow;
        private TelegramBotClient _bot;
        public ObservableCollection<MessageLog> BotMessageLog { get; set; }

        //private void MessageListener(object sender, ApiResponseEventArgs e)
        //{
        //    var text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

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

        public TelegramMessageClient(MainWindow window, string pathToken = "5031242663:AAFCwwmC0ih-9Xhzh70bzgOitny5o1DkxMk")
        {
            BotMessageLog = new ObservableCollection<MessageLog>();
            _mainWindow = window;

            _bot = new TelegramBotClient(File.ReadAllText(pathToken));

            //_bot.OnMessage += MessageListener;

            //_bot.StartReceiving();
        }

        public void SendMessage(string text, string id)
        {
            var currentId = Convert.ToInt64(id);
            _bot.SendTextMessageAsync(id, text);
        }
    }
}
