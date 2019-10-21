using System;
using Stylet;
using StyletIoC;
using AT.Player.Pages;
using AT.Player.Pages.Settings;
using AT.Player.Pages.Monitors;

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

        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            base.ConfigureIoC(builder);

            builder.Bind<IMonitorViewModelFactory>().ToAbstractFactory();
            builder.Bind<IMonitorSettingViewModelFactory>().ToAbstractFactory();
        }
    }

    public interface IMonitorSettingViewModelFactory
    {
        MonitorSettingViewModel CreateMonitorSettingViewModel();
    }

    public interface IMonitorViewModelFactory
    {
        MonitorViewModel CreateMonitorViewModel();
    }
}