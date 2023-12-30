using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using PodcastPlayer.Models;

namespace PodcastPlayer;

public partial class AppShell : Shell
{
    string EpisodePath = "https://pdst.fm/e/chrt.fm/track/A27C8C/traffic.megaphone.fm/GLT8329172184.mp3";

    public Episode CurrentEpisode { get; set; }
    public ObservableCollection<Episode> CurrentEpisodeList { get; set; } = new ObservableCollection<Episode>();
    public ObservableCollection<Episode> Playlist { get; set; } = new ObservableCollection<Episode>();

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
            DurationLabel.Text = Player.Duration.ToString(@"hh\:mm\:ss");
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
        PositionLabel.Text = Player.Position.ToString(@"hh\:mm\:ss");
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

    void OnJumpClicked(object sender, EventArgs e)
    {
        Player.SeekTo(Player.Position.Add(TimeSpan.FromSeconds(20)), CancellationToken.None);
    }

    void OnNextClicked(object sender, EventArgs e)
    {
        var index = CurrentEpisodeList.IndexOf(CurrentEpisode);
       
        Debug.WriteLine($"Current Index: {index}");
        if (index < CurrentEpisodeList.Count - 1)
        {
            CurrentEpisode = CurrentEpisodeList[index + 1];
            Player.Source = CurrentEpisode.Path;
            Player.Play();
            UpdatePlayList();
        }
    }

    void OnMediaOpened(object? sender, EventArgs e)
    {
        Debug.WriteLine("Media opened.");

        PlayPause.IsEnabled = true;
        Stop.IsEnabled = true;
        JumpForward.IsEnabled = true;
        Next.IsEnabled = true;
        PlayPause.Text = "Pause";

        PositionLabel.Text = Player.Position.ToString(@"hh\:mm\:ss");
        DurationLabel.Text = Player.Duration.ToString(@"hh\:mm\:ss");

        UpdatePlayList();
    }

    void UpdatePlayList()
    {
        Playlist.Clear();

        foreach (var episode in CurrentEpisodeList)
        {
            // if the episode is the current episode, add it to the playlist
            if (episode == CurrentEpisode)
            {
                Playlist.Add(episode);
                continue;
            }
            if (CurrentEpisodeList.IndexOf(episode) > CurrentEpisodeList.IndexOf(CurrentEpisode) && CurrentEpisodeList.IndexOf(episode) < CurrentEpisodeList.IndexOf(CurrentEpisode) + 5)
            {
                Playlist.Add(episode);
                continue;
            }
        }

        PlaylistArea.Clear();
        foreach(var episode in Playlist)
        {
            Label lbl = new Label();
            lbl.Text = episode.Title;
            lbl.TextColor = Color.FromArgb("#000000");
            lbl.Margin = new Thickness(0, 0, 0, 10);

            // make the current episode bold
            if (episode == CurrentEpisode)
            {
                lbl.FontAttributes = FontAttributes.Bold;
            }

            // alternate the background color between different shades of light grey
            if (Playlist.IndexOf(episode) % 2 == 0)
            {
                lbl.BackgroundColor = Color.FromArgb("#F0F0F0");
            } else
            {
                lbl.BackgroundColor = Color.FromArgb("#D0D0D0");
            }

            PlaylistArea.Add(lbl);
        }
        Debug.WriteLine($"Playlist length: {Playlist.Count}");
    }
}
