using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using static System.Console;
using static Telegram_bot.Handlers;

namespace Telegram_bot
{
    internal class Program
    {
        private static void Main()
        {
            WriteLine("Запущен бот " + Bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions(); // receive all update types

            Bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            ReadLine();
        }
    }
}
