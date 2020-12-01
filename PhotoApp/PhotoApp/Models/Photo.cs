using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PhotoApp.Models
{
    public class Photo
    {
        //private int id;
        //private DateTime created_at;
        //private DateTime updated_at;
        //private string description;
        //private string alt_description;
        public string Id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Description { get; set; }
        public string Alt_description { get; set; }
        public int Likes { get; set; }
        public Dictionary<string, Uri> Urls { get; set; }

        public ImageSource MainImage { get; set; }
    }
}
