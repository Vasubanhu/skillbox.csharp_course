using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace Telegram_bot
{
    internal class Program
    {
        private static TelegramBotClient _botClient;

        private static async Task Main(string[] args)
        {
            _botClient = new TelegramBotClient(Configuration.Token);

            // Test connection
            User user = await _botClient.GetMeAsync();
            Title = user.Username ?? "My awesome Bot";

            using CancellationTokenSource cts = new CancellationTokenSource();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } }; // receive all update types
            _botClient.StartReceiving(Handlers.HandleUpdateAsync, 
                                      Handlers.HandleErrorAsync, 
                                      receiverOptions, 
                                      cancellationToken: cts.Token);

            WriteLine($"Start listening for @{user.Username}");
            ReadKey();

            // Send cancellation request to stop bot
            cts.Cancel();
        }
    }
}
