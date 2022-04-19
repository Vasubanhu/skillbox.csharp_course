using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using static System.Console;
using static Telegram_bot.MessageHandler;

namespace Telegram_bot
{
    internal class Program
    {
        private static void Main()
        {
            using var cts = new CancellationTokenSource();

            Bot.StartReceiving(HandleUpdateAsync,
                               HandleErrorAsync,
                               new ReceiverOptions(),
                               cts.Token);

            WriteLine($"{Bot.GetMeAsync(cts.Token).Result.FirstName}Bot was launched.\n");

            ReadLine();

            cts.Cancel();
        }
    }
}
