using System;
using System.IO;
using System.Linq;

namespace Telegram_bot
{
    internal class PathFinder
    {
        private static readonly string Template = @$"Users\{Environment.UserName}\Downloads\Telegram Desktop";

        internal static string SetPath() => DriveInfo.GetDrives()
            .Where(d => d.IsReady)
            .Select(d => Path.Combine(d.Name, Template))
            .Select(path => Directory.Exists(path) ? path : "Path not exist.").FirstOrDefault();
    }
}
