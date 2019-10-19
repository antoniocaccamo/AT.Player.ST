using System;
using Stylet;
using StyletIoC;
using AT.Player.Pages;

namespace AT.Player
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void Configure()
        {
            // Perform any other configuration before the application starts

            Unosquare.FFME.Library.FFmpegDirectory =
                @"C:\development\tools\ffmpeg\ffmpeg-4.2.1-win32-static\";
            // Unosquare.FFME.Library.EnableWpfMultiThreadedVideo = true;
            System.Console.WriteLine("Configure..");
        }

        protected override void OnStart()
        {
            Stylet.Logging.LogManager.Enabled = true;
        }
    }
}