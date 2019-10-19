using System;
using Stylet;
using StyletIoC;
using AT.Player.Pages;

namespace AT.Player
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        protected override void Configure()
        {
            // Perform any other configuration before the application starts

            Unosquare.FFME.Library.FFmpegDirectory =
                @"C:\development\tools\ffmpeg\ffmpeg-4.2.1-win32-shared\bin";
            // Unosquare.FFME.Library.EnableWpfMultiThreadedVideo = true;
            Logger.Info("Configure Unosquare.FFME.Library.FFmpegDirectory) [{0}]" +
                " - Exists [{1}]",
                Unosquare.FFME.Library.FFmpegDirectory,
                System.IO.Directory.Exists(Unosquare.FFME.Library.FFmpegDirectory));

            Unosquare.FFME.Library.LoadFFmpeg();
        }

        protected override void OnStart()
        {
            Stylet.Logging.LogManager.Enabled = true;
        }
    }
}