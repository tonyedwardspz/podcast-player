namespace PodcastPlayer.Models;

public class Episode
{
    public string Title { get; set; }
    public string Path { get; set; }
    public string Series { get; set; }
    public int EpisodeNumber { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime Published { get; set; }

    public Episode(string path)
    {
        Path = path;
    }
}
