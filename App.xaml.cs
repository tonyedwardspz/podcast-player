using PodcastPlayer.Views;

namespace PodcastPlayer;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
		Routing.RegisterRoute("libraries/tonys-podcasts", typeof(LibraryPage));
		Routing.RegisterRoute("libraries/wellbeing-podcasts", typeof(LibraryPage));
		Routing.RegisterRoute("libraries/sleep-podcasts", typeof(LibraryPage));
		Routing.RegisterRoute("libraries/archived-podcasts", typeof(LibraryPage));
		
	}
}
