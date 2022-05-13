using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Console;
using File = System.IO.File;

namespace Telegram_bot
{
    internal class MessageController
    {
        internal static ITelegramBotClient Bot { get; } = new TelegramBotClient(Configuration.TelegramToken);
        private static readonly string Path = PathFinder.SetPath();
        private static string _json = string.Empty;

        internal static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Log to console
            _json = JsonConvert.SerializeObject(update);
            WriteLine(_json);

            switch (update.Type)
            {
                case UpdateType.Message when update.Message?.Text != null:
                    await HandleMessage(botClient, update.Message);
                    return;
                case UpdateType.CallbackQuery:
                    await HandleCallbackQuery(botClient, update.CallbackQuery);
                    return;
            }
        }

        private static async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            // Files
            InputOnlineFile inputOnlineFile;

            if (callbackQuery.Data!.Contains("Audio"))
            {
                await using var stream = File.OpenRead("src_docs_voice-nfl_commentary.ogg");
                inputOnlineFile = new InputOnlineFile(stream, $"record-{DateTime.Now:dd.MM.yyyy}.mp3");
                await botClient.SendDocumentAsync(callbackQuery.Message!.Chat.Id, inputOnlineFile);
                return;
            }

            if (callbackQuery.Data!.Contains("Image"))
            {
                inputOnlineFile = new InputOnlineFile("https://www.freecatphotoapp.com/your-image.jpg");
                await botClient.SendPhotoAsync(callbackQuery.Message!.Chat.Id,
                    inputOnlineFile,
                    "<b>Cat</b>. <i>Source</i>: <a href=\"https://www.freecatphotoapp.com\">FreeCodeCamp</a>",
                    ParseMode.Html);
                return;
            }

            if (callbackQuery.Data!.Contains("Document"))
            {
                inputOnlineFile = new InputOnlineFile("https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg");
                await botClient.SendDocumentAsync(callbackQuery.Message!.Chat.Id,
                    inputOnlineFile,
                    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
                    parseMode: ParseMode.Html);
                return;
            }

            if (callbackQuery.Data!.Contains("List"))
            {
                if (Directory.GetFiles(Path).Length != 0)
                {
                    var files = ProcessDirectory(Path);
                    var repM = GetInlineKeyboard(files);

                    await botClient.SendTextMessageAsync(callbackQuery.Message!.Chat.Id,
                        "<i>List of downloaded files:</i>".ToUpper(), ParseMode.Html, replyMarkup: repM);
                    return;
                }

                await botClient.SendTextMessageAsync(callbackQuery.Message!.Chat.Id, $"{Path} is not a valid directory.");
                return;
            }

            if (callbackQuery.Data!.StartsWith('#'))
            {
                await using var stream = File.OpenRead(GetFile(callbackQuery.Data));
                inputOnlineFile = new InputOnlineFile(stream);
                await botClient.SendDocumentAsync(callbackQuery.Message!.Chat.Id, inputOnlineFile);
                return;
            }

            // Weather
            if (callbackQuery.Data!.Contains(Cities.Magnitogorsk))
            {

                var summary = WeatherController.GetInfoFrom(Cities.Magnitogorsk);
                await botClient.SendTextMessageAsync(callbackQuery.Message!.Chat.Id, summary.Result);
                return;
            }

            if (callbackQuery.Data!.Contains(Cities.Chelyabinsk))
            {

                var summary = WeatherController.GetInfoFrom(Cities.Chelyabinsk);
                await botClient.SendTextMessageAsync(callbackQuery.Message!.Chat.Id, summary.Result);
                return;
            }

            if (callbackQuery.Data!.Contains(Cities.Yekaterinburg))
            {

                var summary = WeatherController.GetInfoFrom(Cities.Yekaterinburg);
                await botClient.SendTextMessageAsync(callbackQuery.Message!.Chat.Id, summary.Result);
                return;
            }

            await botClient.SendTextMessageAsync(callbackQuery.Message!.Chat.Id,
            $"You choose with data: {callbackQuery.Data}");
        }

        private static async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {
            switch (message.Text)
            {
                case "/start":
                    await botClient.SendTextMessageAsync(message.Chat.Id,
                        "Welcome aboard! Click to the 'Menu' button to display a list with commands.");
                    return;
                case "/weather":
                    {
                        InlineKeyboardMarkup inlineKeyboard = new(new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(Cities.Magnitogorsk, Cities.Magnitogorsk),
                                InlineKeyboardButton.WithCallbackData(Cities.Chelyabinsk, Cities.Chelyabinsk),
                                InlineKeyboardButton.WithCallbackData(Cities.Yekaterinburg, Cities.Yekaterinburg)
                            }
                        });

                        await botClient.SendTextMessageAsync(message.Chat.Id,
                            "Choose the city to know the weather:",
                            replyMarkup: inlineKeyboard);
                        return;
                    }
                case "/files":
                    {
                        InlineKeyboardMarkup inlineKeyboard = new(new[]
                        {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "Audio", callbackData: "Audio"),
                            InlineKeyboardButton.WithCallbackData(text: "Image", callbackData: "Image"),
                            InlineKeyboardButton.WithCallbackData(text: "Document", callbackData: "Document")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "List of files", callbackData: "List")
                        }
                    });

                        await botClient.SendTextMessageAsync(message.Chat.Id, "Choose a file:",
                            replyMarkup: inlineKeyboard);
                        return;
                    }
            }

            await botClient.SendTextMessageAsync(message.Chat.Id, $"You said: {message.Text}");
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

            WriteLine(errorMessage);

            return Task.CompletedTask;
        }
        // Return filename in directory
        private static IReadOnlyList<string> ProcessDirectory(string targetDirectory)
            => Directory.GetFiles(targetDirectory).Select(System.IO.Path.GetFileName).ToList();
        // Transform the list of files to buttons
        private static InlineKeyboardMarkup GetInlineKeyboard(IReadOnlyList<string> list)
        {
            var keyboardInline = new InlineKeyboardButton[list.Count][];
            var keyboardButtons = new InlineKeyboardButton[list.Count];

            for (var i = 0; i < list.Count; i++)
            {
                keyboardButtons[i] = new InlineKeyboardButton(string.Empty)
                {
                    Text = list[i],
                    CallbackData = $"#{list[i]}"
                };
            }

            for (var j = 1; j <= list.Count; j++)
            {
                keyboardInline[j - 1] = keyboardButtons.Take(1).ToArray();
                keyboardButtons = keyboardButtons.Skip(1).ToArray();
            }

            return keyboardInline;
        }

        public static string GetFile(string fileName) => @$"{Path}\{fileName.Trim('#')}";
    }
}
