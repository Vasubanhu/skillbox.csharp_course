using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Console;

namespace Telegram_bot
{
    internal class MessageHandler
    {
        public static ITelegramBotClient Bot { get; } = new TelegramBotClient(Configuration.Token);

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
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
            // Method
            if (callbackQuery.Data!.Contains("Audio"))
            {
                await using var stream = System.IO.File.OpenRead("src_docs_voice-nfl_commentary.ogg");
                await botClient.SendVoiceAsync(callbackQuery.Message!.Chat.Id, voice: stream, duration: 36);
                return;
            }

            await botClient.SendTextMessageAsync(callbackQuery.Message!.Chat.Id, $"You choose with data: {callbackQuery.Data}");
        }

        private static async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {
            switch (message.Text)
            {
                case "/start":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Welcome aboard! Choose commands: /inline | /keyboard");
                    return;
                case "/keyboard":
                    {
                        ReplyKeyboardMarkup keyboard = new(new[]
                            {
                            new KeyboardButton[] {"Audio", "Image", "Document"},
                            new KeyboardButton[] {"Uploaded files"}
                        })
                        { ResizeKeyboard = true };

                        await botClient.SendTextMessageAsync(message.Chat.Id, "Choose", replyMarkup: keyboard);
                        return;
                    }
                case "/inline":
                    {
                        InlineKeyboardMarkup keyboard = new(new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: "Audio", callbackData: "Audio"),
                                InlineKeyboardButton.WithCallbackData(text: "Image", callbackData: "#"),
                                InlineKeyboardButton.WithCallbackData(text: "Document", callbackData: "#")
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: "Uploaded files", callbackData: "#")
                            }
                        });

                        await botClient.SendTextMessageAsync(message.Chat.Id, "Choose inline:", replyMarkup: keyboard);
                        return;
                    }
            }

            await botClient.SendTextMessageAsync(message.Chat.Id, $"You said: {message.Text}");
        }

        //#3 Image
        //Message message = await botClient.SendPhotoAsync(
        //    chatId: chatId,
        //    photo: "https://www.freecatphotoapp.com/your-image.jpg",
        //    caption: "<b>Cat</b>. <i>Source</i>: <a href=\"https://www.freecatphotoapp.com\">FreeCodeCamp</a>",
        //    parseMode: ParseMode.Html,
        //    cancellationToken: cancellationToken);

        //// #5 Other documents
        //message = await botClient.SendDocumentAsync(
        //    chatId: chatId,
        //    document: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
        //    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
        //    parseMode: ParseMode.Html,
        //    cancellationToken: cancellationToken);

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
