using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_Bot
{

    class Program
    {

        static void Main(string[] args)
        {
            var client = new TelegramBotClient("6008200461:AAEdBKBx2kLt1vMyRIrmIUXpoPWLgtdC2Fc");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var massage = update.Message;
            if (massage.Text != null)
            {
                Console.WriteLine($"{massage.Chat.FirstName} | {massage.Text}");
                if (massage.Text.ToLower().Contains("здорово"))
                {
                    await botClient.SendTextMessageAsync(massage.Chat.Id, "Здоровей видали");
                    return;
                }
            }
            if (massage.Photo != null)
            {
                await botClient.SendTextMessageAsync(massage.Chat.Id, "Норм фото");

                var fileId = update.Message.Photo.Last().FileId;
                var fileInfo = await botClient.GetFileAsync(fileId);
                var filePath = fileInfo.FilePath;

                //const string destinationFilePath = "../downloaded.file";

                const string destinationFilePath = "C:\\Users\\NIKnak\\Desktop\\1/downloaded.png";

                await using Stream fileStream = System.IO.File.Create(destinationFilePath);
                await botClient.DownloadFileAsync(
                filePath: filePath,
                destination: fileStream);
                fileStream.Close();

                return;
            }
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}