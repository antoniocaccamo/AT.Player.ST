using System;
using System.Windows;

namespace Company.WpfApplication1.Pages
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();

            MediaElement.BufferingStarted += (sdr, evt) => { Console.WriteLine($"BufferingStarted"); };
            MediaElement.BufferingEnded += (sdr, evt) => { Console.WriteLine($"BufferingEnded"); };

            MediaElement.MediaOpened += (sdr, evt) => { Console.WriteLine($"MediaOpened {evt.Info.Duration}"); };
            MediaElement.MediaEnded += async (sdr, evt) => { Console.WriteLine($"MediaEnded"); await MediaElement.Stop(); };
            MediaElement.MediaFailed += (sdr, evt) => { Console.WriteLine($"MediaFailed {evt.ErrorException}"); };

            MediaElement.Loaded += (sdr, evt) => { Console.WriteLine($"Loaded  {evt}"); };
            //MediaElement.PositionChanged += (sdr, evt) => { Console.WriteLine($"PositionChanged {evt.Position}"); };

            MediaElement.Source = new Uri(
                //"C:\\development\\workspaces\\antoniocaccamo\\at-adv\\videos\\default\\at.video.mov"
                @"C:\development\workspaces\DEVOPS200\17082018 DEVOPS200 1 PandP SetupPUwithVS2017.mp4"
               );

            //MediaElement.Play();
        }
    }
}