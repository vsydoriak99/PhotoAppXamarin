using PhotoApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using PhotoApp.Views;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

namespace PhotoApp.ViewModels
{
    class PhotosViewModel : BaseViewModel
    {
        public IValueConverter IntEnum = new IntEnumConverter();
        private Search _search;

        public Search SearchItm
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged(nameof(SearchItm));
            }
        }


        public ObservableCollection<Photo> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public Command SearchCommand { get; set; }

        public PhotosViewModel()
        {
            Title = "Photo's List";
            Items = new ObservableCollection<Photo>();
            SearchItm = new Search();
            LoadItemsCommand = new Command(async () => await ExecuteSearchItemsCommand(SearchItm));


        }

        async Task ExecuteSearchItemsCommand(Search search)
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(search, true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}

