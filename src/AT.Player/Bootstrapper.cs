using System;
using Stylet;
using StyletIoC;
using AT.Player.Pages;
using AT.Player.Pages.Settings;
using AT.Player.Pages.Monitors;

//using AT.Player.Service;
using AT.Player.Configuration;
using NLog;

namespace AT.Player
{
    public interface IMonitorViewModelFactory
    {
        #region Public Methods

        MonitorSettingViewModel CreateMonitorSettingViewModel();

        MonitorViewModel CreateMonitorViewModel();

        #endregion Public Methods
    }

    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        #region Private Fields

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        #endregion Private Fields

        #region Protected Methods

        protected override void Configure()
        {
            // Perform any other configuration before the application starts

            Unosquare.FFME.Library.FFmpegDirectory =
                @"ffmpeg";
            // Unosquare.FFME.Library.EnableWpfMultiThreadedVideo = true;
            Logger.Info("Configure Unosquare.FFME.Library.FFmpegDirectory) [{0}] - Exists [{1}]",
                Unosquare.FFME.Library.FFmpegDirectory,
                System.IO.Directory.Exists(Unosquare.FFME.Library.FFmpegDirectory)
            );

            Unosquare.FFME.Library.EnableWpfMultiThreadedVideo = true;
            Unosquare.FFME.Library.LoadFFmpeg();
        }

        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            base.ConfigureIoC(builder);
            builder.Autobind();
            builder.Bind<IMonitorViewModelFactory>().ToAbstractFactory();
            builder.Bind<IContext>().ToInstance(new Context());
        }

        protected override void OnStart()
        {
            Stylet.Logging.LogManager.Enabled = true;
        }

        #endregion Protected Methods
    }
}