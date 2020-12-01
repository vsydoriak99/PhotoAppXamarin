using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoApp.Models
{
    public enum MenuItemType
    {
        PhotoList,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
