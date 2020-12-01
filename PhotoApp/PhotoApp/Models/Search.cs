using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PhotoApp.Models
{
    public class Search
    {
        public enum Color { def, black_and_white, black, white, yellow, orange, red, purple, magenta, green, teal, blue };
        public enum Orientation { all, landscape, portrait, squarish };
        private string _query;
        public string Query 
        {
            get { return _query; }
            set 
            {
                _query = value.Replace(" ", "-"); 
            } 
        }
        public Color color { get; set; } 
        public Orientation orientation { get; set; }

        public List<string> ColorsNames
        {
            get
            {
                return Enum.GetValues(typeof(Search.Color))
                    .Cast<Color>()
                    .Select(v => v.ToString())
                    .ToList();
            }
        }
        public List<string> OrientationsNames
        {
            get
            {
                return Enum.GetValues(typeof(Search.Orientation))
                    .Cast<Orientation>()
                    .Select(v => v.ToString())
                    .ToList();
            }
        }
    }
}
