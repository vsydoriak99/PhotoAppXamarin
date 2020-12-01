using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using PhotoApp.Models;
using PhotoApp.Services;
using Xamarin.Forms;

namespace PhotoApp.ViewModels
{
    public class PhotoDetailViewModel : BaseViewModel
    {        
        private Photo _item;
        public Photo Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public PhotoDetailViewModel(Photo item = null)
        {
            Update(item);
        }

        private async void Update(Photo photo)
        {
            Item = await DataStore.GetItemAsync(photo?.Id);
        }

    }
}
