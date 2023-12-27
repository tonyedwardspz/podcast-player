using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PodcastPlayer;

public partial class AppShell : Shell
{
    string EpisodePath = "https://pdst.fm/e/chrt.fm/track/A27C8C/traffic.megaphone.fm/GLT8329172184.mp3";

    public AppShell()
	{
		InitializeComponent();
    }

    protected override async void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);
        Debug.WriteLine($"Navigation to {args.Target.Location} started");
    }

    public MediaElement GetMediaElement()
    {
        return Player; // mediaElement is the x:Name of your MediaElement in Shell
    }
}
