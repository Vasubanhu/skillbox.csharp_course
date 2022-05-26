using System.Diagnostics;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using static WpfAppForTelegramBot.TelegramMessageClient;

namespace WpfAppForTelegramBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var cts = new CancellationTokenSource();
            Bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, new ReceiverOptions(), cts.Token);
            //TEST
            Debug.WriteLine($@"{Bot.GetMeAsync(cts.Token).Result.FirstName}Bot was launched.", "TEST");

            //cts.Cancel();
        }
    }
}
