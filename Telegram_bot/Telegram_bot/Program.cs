using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram_bot;
using static System.Console;
using static Telegram_bot.MessageController;

var cts = new CancellationTokenSource();

Bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, new ReceiverOptions(), cts.Token);
WriteLine($"{Bot.GetMeAsync(cts.Token).Result.FirstName}Bot was launched.\n");
// Test
WeatherController.GetInfoFrom("Magnitogorsk");

ReadLine();
cts.Cancel();