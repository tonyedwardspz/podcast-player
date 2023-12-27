﻿using System.Diagnostics;
using PodcastPlayer.Models;
using System.Collections.ObjectModel;

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
		PageTitle = $"Welcome to the {Podcast.Title} series page";

		string[] files = getFiles(Podcast.Folder);
		Episodes = new ObservableCollection<Episode>(
			files.Select(file => new Episode(file, file))
		);
		Debug.WriteLine($"Episodes: {Episodes.Count}");
	}

	public string[] getFiles(string folder)
	{
		Debug.WriteLine("Getting Files");
		string[] results = new string[1];
		try
		{
			results = Directory.GetFiles(folder);
			// print files to debug
			foreach (string file in results)
			{
				Debug.Write(file + " - ");
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		return results;
	}

	public async void Episode_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var episode = e.CurrentSelection.FirstOrDefault() as Episode;
		Debug.WriteLine($"Episode Selected: {episode?.Title}");
	}
}
