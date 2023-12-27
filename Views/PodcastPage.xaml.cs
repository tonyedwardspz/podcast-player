using System.Diagnostics;
using PodcastPlayer.Models;

namespace PodcastPlayer.Views;

[QueryProperty(nameof(Podcast), nameof(Podcast))]
public partial class PodcastPage : ContentPage
{
	private string pageTitle = "Welcome to the podcast detail page.";
	public string PageTitle
	{
		get { return pageTitle; }
		set
		{
			pageTitle = value;
			OnPropertyChanged(nameof(PageTitle));
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
		PageTitle = $"Welcome to the {Podcast.Title} series page.";
	}
}
