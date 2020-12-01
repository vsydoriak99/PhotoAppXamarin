using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PhotoApp.Models;
using PhotoApp.Views;
using PhotoApp.ViewModels;

namespace PhotoApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PhotosPage : ContentPage
    {
        PhotosViewModel viewModel;

        public PhotosPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new PhotosViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Photo)layout.BindingContext;
            await Navigation.PushAsync(new PhotoDetailPage(new PhotoDetailViewModel(item)));
        }



        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}