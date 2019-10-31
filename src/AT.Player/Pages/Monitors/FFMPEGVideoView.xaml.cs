namespace AT.Player.Pages.Monitors
{
    using System;
    using System.IO;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for VideoView.xaml
    /// </summary>
    public partial class FFMEPGVideoView : UserControl
    {
        //public FFMEPGVideoView()
        //{
        //    InitializeComponent();
        //    //var vlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

        //    //var options = new string[]
        //    //{
        //    //    // VLC options can be given here.
        //    //    // Please refer to the VLC command line documentation.
        //    //    "--file-logging",
        //    //    "-vvv",
        //    //    "--logfile=logs/vlc.log"
        //    //};

        //    //this.vlcControl.SourceProvider.CreatePlayer(vlcLibDirectory, options);
        //}

        //private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        //{
        //(DataContext as VideoViewModel).VlcControl = vlcControl;
        //}

        //private void MediaElement_MediaEnded(object sender, EventArgs e)
        //{
        //}

        //private void MediaElement_MediaFailed(object sender, Unosquare.FFME.Common.MediaFailedEventArgs e)
        //{
        //}

        //private void MediaElement_MediaOpened(object sender, Unosquare.FFME.Common.MediaOpenedEventArgs e)
        //{
        //}

        //private void MediaElement_PositionChanged(object sender, Unosquare.FFME.Common.PositionChangedEventArgs e)
        //{
        //}
    }
}