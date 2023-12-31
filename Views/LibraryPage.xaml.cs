using System.Collections.ObjectModel;
using System.Diagnostics;
using PodcastPlayer.Models;

namespace PodcastPlayer.Views;

public partial class LibraryPage : ContentPage
{

    internal Dictionary<string, string> libraries = new Dictionary<string, string>
	{
		{"tonys-podcasts", "Tony's Podcasts"},
		{"wellbeing-podcasts", "Wellbeing Podcasts"},
		{"sleep-podcasts", "Sleep Podcasts"},
		{"archived-podcasts", "Archived Podcasts"}
	};

#if MACCATALYST
	internal string basePath = "/Users/tonyedwardspz/Desktop/podcasts/";
#else
	internal string basePath = @"D:\Podcasts\";
#endif

    internal Dictionary<string, string> folders = new Dictionary<string, string>
	{
        {"tonys-podcasts", "tonys-podcasts"},
        {"wellbeing-podcasts", "wellbeing-podcasts"},
        {"sleep-podcasts", "sleep-podcasts"},
        {"archived-podcasts", "archived-podcasts"}
    };

	private ObservableCollection<Podcast> podcasts = new ObservableCollection<Podcast>();
	public ObservableCollection<Podcast> Podcasts
	{
		get { return podcasts; }
		set { 
			podcasts = value; 
			OnPropertyChanged(nameof(Podcasts));
		}
	}

	internal Podcast selectedPodcast;

    public LibraryPage()
	{
		InitializeComponent();
		BindingContext = this;
		Debug.WriteLine("New Library Page Initialised");
    }

	public string[] getFolders(string library)
	{
        Debug.WriteLine("Getting Folders");
        string[] results = new string[1];
        try
        {
            string fullPath = String.Concat(basePath, folders[GetLibraryFromRoute()]);
            Debug.WriteLine($"Path: {fullPath}");
            results = Directory.GetDirectories(fullPath, "*", SearchOption.AllDirectories);
            Debug.WriteLine(String.Join(", ", results));
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }
        return results;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		Debug.WriteLine("Library Page Appearing");
		var library = GetLibraryFromRoute();

		if (libraries.ContainsKey(library))
		{
			var libraryValue = libraries[library];
			Debug.WriteLine($"Generating title for route '{library}': {libraryValue}");
			this.Title = $"{libraryValue} library.";

			string[] results = getFolders(libraryValue);
			Podcasts = new ObservableCollection<Podcast>(results.Select(result => new Podcast(result)));
			Debug.WriteLine($"Podcasts: {Podcasts.Count}");
		}
		else
		{
			Debug.WriteLine($"ERROR - No matching value found for key '{library}'");
		}
	}

    private string GetLibraryFromRoute()
    {
        var route = Shell.Current.CurrentState.Location.OriginalString.Split("/").LastOrDefault();
		Debug.WriteLine($"Current Route: {Shell.Current.CurrentState.Location.OriginalString}");
        return route;
    }

	public async void Podcast_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		try
		{
            Debug.WriteLine($"Podcast Selection Changed: {selectedPodcast}");
            var podcast = e.CurrentSelection.FirstOrDefault() as Podcast;
            Debug.WriteLine($"Selected Podcast: {podcast.Title}");

            await Shell.Current.GoToAsync($"{nameof(PodcastPage)}",
                new Dictionary<string, object>
                {
                    ["Podcast"] = podcast
                });
        } catch (Exception err)
		{
			Debug.WriteLine(err.Message);
		}
	}
}