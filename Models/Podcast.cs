using System;
using System.Diagnostics;

namespace PodcastPlayer.Models
{
	public class Podcast
	{
		public string Folder { get; set; }
		public string Title 
		{
			get
			{
				string[] parts = Folder.Split('/');
				string lastPart = parts[parts.Length - 1];
				string formattedTitle = lastPart.Replace("-", " ");
				return formattedTitle.Length > 0 ? formattedTitle.Substring(0, 1).ToUpper() + formattedTitle.Substring(1) : "No title Generated";
			}
		}

		public string ImageUrl
		{
			get
			{
				string[] results = new string[1];
				try
				{
					results = Directory.GetFiles(Folder);
					results = results.Where(file => file.EndsWith(".jpg")).ToArray();
					if (results.Length > 0)
					{
						return results[0];
					}
				} catch (Exception e)
				{
					Debug.WriteLine(e.Message);
				}
				return "https://placehold.co/200X200.png?text=No+Cover\nImage";
			}
		}
		

		public Podcast(string folder)
		{
			Debug.WriteLine($"New Podcast: {folder}");
			Folder = folder;
		}
	}
}

