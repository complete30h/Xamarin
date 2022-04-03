using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesCatalog.Models;
using GamesCatalog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
namespace GamesCatalog.Views
{
    [QueryProperty(nameof(islist), "islist")]
    public partial class FilterItem : ContentPage
    {
        FilterItemView _viewModel;
        Filter filter;
        private bool _islist;
        public bool islist
        {
            get { return _islist; }
            set { _islist = value; }
        }
        public FilterItem()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = _viewModel = new FilterItemView(islist);
        }


        private void Button_Clicked(object sender, EventArgs e)
        {

            Text.Text = "";
            //Description.Text = "";
            Mode.Text = "";
            Publisher.Text = "";
            Developer.Text = "";
            Genre.Text = "";
            Platforms.Text = "";
            FromYear.Text = "";
            ToYear.Text = "";
            
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (Text.Text != null || Mode.Text != null || Publisher.Text != null || Developer.Text != null || Genre.Text != null || Platforms.Text != null || FromYear.Text != null || ToYear.Text != null)
            {
                filter = new Filter();
                filter.Name = Text.Text;
                filter.Mode = Mode.Text;
                filter.Publisher = Publisher.Text;
                filter.Developer = Developer.Text;
                filter.Genre = Genre.Text;
                filter.Platforms = Platforms.Text;
                if (int.TryParse(FromYear.Text, out var number))
                filter.FromYear = FromYear.Text;
                if (int.TryParse(ToYear.Text, out var number1))
                    filter.ToYear = ToYear.Text;
                //viewModel._filter = filter;
                //viewModel.Flag = true;
                var jsonStr = JsonConvert.SerializeObject(filter);
                await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?Filter={jsonStr}&flag={true}&islist={islist}");
            }
            else
                await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?flag={false}&islist={islist}");
           // viewModel.UpdateFilter();

        }

        
    }
}