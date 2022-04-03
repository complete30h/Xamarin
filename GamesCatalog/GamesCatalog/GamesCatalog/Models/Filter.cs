using System;
using System.Collections.Generic;
using System.Text;

namespace GamesCatalog.Models
{
   public class Filter
    {
        public string Name { get; set; }

        public string Mode { get; set; }

        public string Publisher { get; set; }

        public string Developer { get; set; }

        public string Genre { get; set; }

        public string Platforms { get; set; }

        public string FromYear { get; set; }

        public string ToYear { get; set; }


        public void Clear()
        {
            Name = "";
            Mode = "";
            Publisher = "";
            Developer = "";
            Genre = "";
            Platforms = "";
            FromYear = "";
            ToYear = "";
        }

    }
}
