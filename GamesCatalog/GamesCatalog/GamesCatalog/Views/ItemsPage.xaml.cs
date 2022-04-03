using GamesCatalog.Models;
using GamesCatalog.ViewModels;
using GamesCatalog.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Newtonsoft.Json;


namespace GamesCatalog.Views
{
    [QueryProperty(nameof(_flag), "flag")]
    [QueryProperty(nameof(_Filter), "Filter")]
    [QueryProperty(nameof(islist), "islist")]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        private Filter Filter;
        Command Tap;
        private bool flag;
        private bool _islist;
        string _filter;
        public string _Filter
        {
            get => _filter;
            set
            {
                _filter = Uri.UnescapeDataString(value ?? string.Empty);
                

            }
        }

        public bool islist
        {
            get { return _islist; }
            set { _islist = value; }
        }
        public bool _flag
        {
            get { return flag; }
            set { flag = value; }
        }

        public ItemsPage()
        {
            InitializeComponent();            
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            PerformOperation(_Filter);
            BindingContext = _viewModel = new ItemsViewModel(this, islist, _flag, Filter);
            _viewModel.OnAppearing();

        }
        private void PerformOperation(string getfilter)
        {
            if (getfilter != null)
            {
                Filter = new Filter();
                Filter = JsonConvert.DeserializeObject<Filter>(getfilter);
            }
            //Filter.Name = "Witcher 3";
        }

        private void TapGesture_Tapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}