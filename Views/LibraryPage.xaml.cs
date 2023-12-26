using System.Diagnostics;

namespace PodcastPlayer.Views;

public partial class LibraryPage : ContentPage
{

    private string libraryTitle = "Welcome to the library page.";
    public string LibraryTitle
    {
        get { return libraryTitle; }
        set
        {
            libraryTitle = value;
            OnPropertyChanged(nameof(LibraryTitle));
        }
    }

    internal Dictionary<string, string> libraries = new Dictionary<string, string>
	{
		{"tonys-podcasts", "Tony's Podcasts"},
		{"wellbeing-podcasts", "Wellbeing Podcasts"},
		{"sleep-podcasts", "Sleep Podcasts"},
		{"archived-podcasts", "Archived Podcasts"}
	};

    public LibraryPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		Debug.WriteLine("Library Page Appeared");
		var library = GetLibraryFromRoute();

		if (libraries.ContainsKey(library))
		{
			var libraryValue = libraries[library];
			Debug.WriteLine($"Matching value for key '{library}': {libraryValue}");
			LibraryTitle = $"Welcome to the {libraryValue} library page.";
		}
		else
		{
			Debug.WriteLine($"No matching value found for key '{library}'");
		}
	}

    private string GetLibraryFromRoute()
    {
        var route = Shell.Current.CurrentState.Location.OriginalString.Split("/").LastOrDefault();
        Debug.WriteLine(route);
        return route;
    }
}