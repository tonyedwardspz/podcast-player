using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PodcastPlayer;

public partial class AppShell : Shell
{
    public AppShell()
	{
		InitializeComponent();
    }

    protected override async void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);
        Debug.WriteLine($"Navigation to {args.Target.Location} started");
    }
}
