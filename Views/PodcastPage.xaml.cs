using System.Diagnostics;
using PodcastPlayer.Models;
using System.Collections.ObjectModel;

using CommunityToolkit.Maui.Views;

namespace PodcastPlayer.Views;

[QueryProperty(nameof(Podcast), nameof(Podcast))]
public partial class PodcastPage : ContentPage
{
	private ObservableCollection<Episode> episodes;
	public ObservableCollection<Episode> Episodes
	{
		get { return episodes; }
		set
		{
			episodes = value;
			OnPropertyChanged(nameof(Episodes));
		}
	}

	public Podcast Podcast { get; set; }
	
	public PodcastPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Debug.WriteLine("Podcast Page Appearing");
		Debug.WriteLine($"Podcast: {Podcast.Title}");
		this.Title = $"{Podcast.Title} series page";

		string[] files = getFiles(Podcast.Folder);
		Episodes = new ObservableCollection<Episode>(
			files.Select(path => new Episode(path))
		);
		Episodes = UpdateEpisodes(Episodes).Result;
		Debug.WriteLine($"Episodes: {Episodes.Count}");
	}

	public string[] getFiles(string folder)
	{
		Debug.WriteLine("Getting Files");
		string[] results = new string[1];
		try
		{
			results = Directory.GetFiles(folder);
			results = results.Where(file => file.EndsWith(".mp3")).ToArray();
			Debug.WriteLine(results.ToString());
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		return results;
	}

	public async Task<ObservableCollection<Episode>> UpdateEpisodes(ObservableCollection<Episode> episodes) 
	{
		foreach (Episode episode in episodes)
		{
			var file = TagLib.File.Create(episode.Path);
			episode.Title = file.Tag.Title;
			episode.Series = file.Tag.Album;
			episode.EpisodeNumber = (int)file.Tag.Track;
			episode.Duration = file.Properties.Duration;
			episode.Published = File.GetCreationTime(episode.Path);
		}
		return episodes;
	}

	public void Episode_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var episode = e.CurrentSelection.FirstOrDefault() as Episode;
		Debug.WriteLine($"Episode Selected: {episode?.Title}");

		if (Shell.Current is AppShell shell)
        {
            MediaElement player = shell.GetPlayer();
			shell.CurrentEpisodeList = Episodes;
			shell.CurrentEpisode = episode;
			player.Source = episode?.Path;
			player.Play();

			Label details = shell.GetPodcastDetails();
			details.Text = $"Series: {Podcast.Title}";

			Label episodeDetails = shell.GetEpisodeDetails();
			episodeDetails.Text = $"Episode: {episode?.Title}";
        }
	}
}
