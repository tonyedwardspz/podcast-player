using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

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

        Player.PropertyChanged += Player_PropertyChanged;
    }

    public MediaElement GetPlayer()
    {
        return Player;
    }

    public Label GetPodcastDetails()
    {
        return PodcastDetails;
    }

    public Label GetEpisodeDetails()
    {
        return EpisodeDetails;
    }

    void Player_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == MediaElement.DurationProperty.PropertyName)
        {
            Debug.WriteLine($"Duration: {Player.Duration}" );
            PositionSlider.Maximum = Player.Duration.TotalSeconds;
        }
    }

	async void Slider_DragCompleted(object? sender, EventArgs e)
	{
		ArgumentNullException.ThrowIfNull(sender);

		var newValue = ((Slider)sender).Value;
		await Player.SeekTo(TimeSpan.FromSeconds(newValue), CancellationToken.None);

		Player.Play();
	}

	void Slider_DragStarted(object sender, EventArgs e)
	{
		Player.Pause();
	}

    void OnPositionChanged(object? sender, MediaPositionChangedEventArgs e)
	{
		PositionSlider.Value = e.Position.TotalSeconds;
	}

    void OnPlayPauseClicked(object sender, EventArgs e)
    {
        if (Player.CurrentState == MediaElementState.Playing)
        {
            Player.Pause();
            PlayPause.Text = "Play";
        } else
        {
            Player.Play();
            PlayPause.Text = "Pause";
        }
    }

    void OnStopClicked(object sender, EventArgs e)
    {
        Player.Stop();
    }

    void OnMediaOpened(object? sender, EventArgs e)
    {
        Debug.WriteLine("Media opened.");

        PlayPause.IsEnabled = true;
        Stop.IsEnabled = true;
        PlayPause.Text = "Pause";
    }
}
