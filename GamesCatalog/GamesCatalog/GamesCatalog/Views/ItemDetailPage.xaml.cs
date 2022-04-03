using GamesCatalog.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace GamesCatalog.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        private  ItemDetailViewModel _viewModel;
        /*
        private bool islist;
        public bool isList
        {
            get
            {
                return islist;
            }
            set
            {
                islist = value;
            }
        }
        */
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemDetailViewModel();

        }

        /*
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = _viewModel = new ItemDetailViewModel();

        }
        */

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            player.AutoPlay = false;
            player.Source = _viewModel.VideoUrl;
        }

    }
}