using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PodcastPlayer;

public partial class AppShell : Shell
{
    public AppShell()
	{
		InitializeComponent();
    }

    async void OnMenuItemClicked(object sender, EventArgs e)
    {
        var flyoutItem = sender as FlyoutItem;
        if (flyoutItem != null)
        {
            Debug.WriteLine("Item selected");
            string data = "Some Data"; 
            await Shell.Current.GoToAsync($"{flyoutItem.Route}?data={data}");
        }
    }
}
