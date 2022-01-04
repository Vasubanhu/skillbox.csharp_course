using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_bot
{
    internal class Handlers
    {
        // Handles messages
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type != UpdateType.Message)
                return;
            // Only process text messages
            if (update.Message!.Type != MessageType.Text)// ! - оператор допускающий значение null https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/null-forgiving
                return;

            long chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Введите команду /start, чтобы перейти в меню:\n" + messageText,
                cancellationToken: cancellationToken);

            //// #1 Echo received message text 
            //Message sentMessage = await botClient.SendTextMessageAsync(
            //    chatId: chatId,
            //    text: "You said:\n" + messageText,
            //    cancellationToken: cancellationToken);

            //// #2 Text with button
            //Message message = await botClient.SendTextMessageAsync(
            //    chatId: chatId,
            //    text: "Trying *all the parameters* of `sendMessage` method",
            //    parseMode: ParseMode.MarkdownV2,
            //    disableNotification: true,
            //    replyToMessageId: update.Message.MessageId,
            //    replyMarkup: new InlineKeyboardMarkup(
            //        InlineKeyboardButton.WithUrl(
            //            "Check sendMessage method",
            //            "https://core.telegram.org/bots/api#sendmessage")),
            //    cancellationToken: cancellationToken);

            //WriteLine(
            //    $"{message.From?.FirstName} sent message {message.MessageId} " +
            //    $"to chat {message.Chat.Id} at {message.Date.ToLocalTime()}. " +
            //    $"It is a reply to message {message.ReplyToMessage?.MessageId} " +
            //    $"and has {message.Entities?.Length} message entities."
            //);

            //#3 Image
            //Message message = await botClient.SendPhotoAsync(
            //    chatId: chatId,
            //    photo: "https://www.freecatphotoapp.com/your-image.jpg",
            //    caption: "<b>Cat</b>. <i>Source</i>: <a href=\"https://www.freecatphotoapp.com\">FreeCodeCamp</a>",
            //    parseMode: ParseMode.Html,
            //    cancellationToken: cancellationToken);

            //// #4 Voice
            //await using var stream = System.IO.File.OpenRead("src_docs_voice-nfl_commentary.ogg");
            //message = await botClient.SendVoiceAsync(
            //    chatId: chatId,
            //    voice: stream,
            //    duration: 36,
            //    cancellationToken: cancellationToken);

            //// #5 Other documents
            //message = await botClient.SendDocumentAsync(
            //    chatId: chatId,
            //    document: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
            //    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
            //    parseMode: ParseMode.Html,
            //    cancellationToken: cancellationToken);

            // Основное меню
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Изображение", callbackData: "1"),
                        InlineKeyboardButton.WithCallbackData(text: "Аудиозапись", callbackData: "2"),
                        InlineKeyboardButton.WithCallbackData(text: "Документ", callbackData: "3"),
                    },
                });

            sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбирите пункт меню",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }
        // Handles error
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            string errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString() // _ - переменная-заполнитель https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/discards
            };

            WriteLine(errorMessage);
            return Task.CompletedTask;
        }
    }
}
