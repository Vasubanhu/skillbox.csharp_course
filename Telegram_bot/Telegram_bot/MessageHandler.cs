using System;
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
    internal class MessageHandler
    {
        public static ITelegramBotClient Bot { get; } = new TelegramBotClient(Configuration.Token);

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            WriteLine(JsonConvert.SerializeObject(update));

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

            await botClient.SendTextMessageAsync(callbackQuery.Message!.Chat.Id, $"You choose with data: {callbackQuery.Data}");
        }

        private static async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {
            switch (message.Text)
            {
                case "/start":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Welcome aboard! Choose commands: /files | /weather");
                    return;
                case "/weather":
                    {
                        InlineKeyboardMarkup inlineKeyboard = new(new[]
                        {
                            InlineKeyboardButton.WithUrl("Magnitogorsk", "https://www.gismeteo.ru/weather-magnitogorsk-4613/"),
                            InlineKeyboardButton.WithUrl("Chelyabinsk", "https://www.gismeteo.ru/weather-chelyabinsk-4565/"),
                            InlineKeyboardButton.WithUrl("Yekaterinburg", "https://www.gismeteo.ru/weather-yekaterinburg-4517/")

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
                                InlineKeyboardButton.WithCallbackData(text: "Uploaded files", callbackData: "#")
                            }
                        });

                        await botClient.SendTextMessageAsync(message.Chat.Id, "Choose a file:", replyMarkup: inlineKeyboard);
                        return;
                    }
            }

            await botClient.SendTextMessageAsync(message.Chat.Id, $"You said: {message.Text}");
        }

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                    $"Telegram's API exception: \n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
                JsonSerializationException jsonException =>
                    $"JSON serialization exception \n{jsonException.LineNumber}\n{jsonException.LinePosition}\n{jsonException.Message}",
                _ => exception.ToString()
            };

            WriteLine(errorMessage);

            return Task.CompletedTask;
        }
    }
}
