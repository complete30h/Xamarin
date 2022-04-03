using System;

namespace GamesCatalog.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }

        public string Description { get; set; }

        public string Mode { get; set; }

        public string Publisher { get; set; }

        public string Developer  { get; set; }

        public string Genre { get; set; }

        public string Platforms { get; set; }

        public string ReleaseDate { get; set; }

        public string VideoUrl { get; set; }

       public Item()
        {

        }

        public Item(string id, string name, string imagesrc, string description, string mode, string publisher, string developer, string genre, string platforms, string releasedate, string videourl)
        {
            Id = id;
            Name = name;
            ImageSrc = imagesrc;
            Description = description;
            Mode = mode;
            Publisher = publisher;
            Developer = developer;
            Genre = genre;
            Platforms = platforms;
            ReleaseDate = releasedate;
            VideoUrl = videourl;

        }
    }
}