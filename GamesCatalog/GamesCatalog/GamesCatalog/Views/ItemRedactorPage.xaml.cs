using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GamesCatalog.ViewModels;

namespace GamesCatalog.Views
{
    [QueryProperty(nameof(id), "id")]
    public partial class ItemRedactorPage : ContentPage
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        public ItemRedactorPage()
        {
           // var portaitAddLaptopViewModel = new ItemRedactorViewModel();
         //   BindingContext = portaitAddLaptopViewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new ItemRedactorViewModel(id);

        }
    }
}