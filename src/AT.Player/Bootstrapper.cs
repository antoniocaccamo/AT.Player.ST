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
    public interface IMonitorSettingViewModelFactory
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        MonitorSettingViewModel CreateMonitorSettingViewModel(string channel, Monitor monitor);

        #endregion Public Methods
    }

    public interface IMonitorViewModelFactory
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        MonitorViewModel CreateMonitorViewModel(string channel, Monitor monitor);

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

            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "logs/file.log" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config
            NLog.LogManager.Configuration = config;

            Unosquare.FFME.Library.FFmpegDirectory =
                @"ffmpeg";
            // Unosquare.FFME.Library.EnableWpfMultiThreadedVideo = true;
            Logger.Info("Configure Unosquare.FFME.Library.FFmpegDirectory) [{0}] - Exists [{1}]",
                Unosquare.FFME.Library.FFmpegDirectory,
                System.IO.Directory.Exists(Unosquare.FFME.Library.FFmpegDirectory)
            );

            Unosquare.FFME.Library.LoadFFmpeg();
            Unosquare.FFME.Library.EnableWpfMultiThreadedVideo = true;
        }

        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            base.ConfigureIoC(builder);
            builder.Autobind();
            //builder.Bind<IMonitorViewModelFactory>().ToAbstractFactory();
            //builder.Bind<IMonitorSettingViewModelFactory>().ToAbstractFactory();
            //builder.Bind<IPreferenceService>().ToInstance(new PreferenceService());
            builder.Bind<IContext>().ToInstance(new Context());
        }

        protected override void OnStart()
        {
            Stylet.Logging.LogManager.Enabled = true;
        }

        #endregion Protected Methods
    }
}