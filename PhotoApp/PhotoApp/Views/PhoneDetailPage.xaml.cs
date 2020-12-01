using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PhotoApp.Models;
using PhotoApp.ViewModels;

namespace PhotoApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PhotoDetailPage : ContentPage
    {
        PhotoDetailViewModel viewModel;

        public PhotoDetailPage(PhotoDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public PhotoDetailPage()
        {
            InitializeComponent();

            var item = new Photo
            {
                Id = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new PhotoDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}