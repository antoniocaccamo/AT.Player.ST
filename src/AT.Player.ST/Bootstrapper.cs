using Company.WpfApplication1.Pages;
using Stylet;
using StyletIoC;

namespace Company.WpfApplication1
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            // Configure the IoC container in here
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts

            Unosquare.FFME.Library.FFmpegDirectory =
                @"C:\development\tools\ffmpeg\ffmpeg-4.2.1-win32-static";
            Unosquare.FFME.Library.EnableWpfMultiThreadedVideo = true;
        }

        protected override void OnStart()
        {
            Stylet.Logging.LogManager.Enabled = true;
        }
    }
}