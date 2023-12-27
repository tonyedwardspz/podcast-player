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

		public Podcast(string folder)
		{
			Debug.WriteLine($"New Podcast: {folder}");
			Folder = folder;
		}
	}
}

