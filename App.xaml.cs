using PodcastPlayer.Views;

namespace PodcastPlayer;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
		
		Routing.RegisterRoute("tonys-podcasts", typeof(LibraryPage));
		Routing.RegisterRoute("wellbeing-podcasts", typeof(LibraryPage));
		Routing.RegisterRoute("sleep-podcasts", typeof(LibraryPage));
		Routing.RegisterRoute("archived-podcasts", typeof(LibraryPage));

		Routing.RegisterRoute(nameof(PodcastPage), typeof(PodcastPage));
	}
}
