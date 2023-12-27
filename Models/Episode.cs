namespace PodcastPlayer.Models;

public class Episode
{
    public string Title 
    { 
        get 
        {
            string[] pathParts = Path.Split('/');
            string fileName = pathParts[pathParts.Length - 1];
            string[] fileNameParts = fileName.Split('-');
            string episodeTitle = string.Join(" ", fileNameParts);
            return episodeTitle;
        }
    }
    public string Path { get; set; }

    public Episode(string title, string path)
    {
        Path = path;
    }
}
