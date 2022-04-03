using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GamesCatalog.Models;
using Xamarin.Forms;
using GamesCatalog.Views;

namespace GamesCatalog.ViewModels
{
    public class FilterItemView : BaseViewModel
    {
        private bool isList;
        public Command OnBack { get; }

        public FilterItemView(bool isList)
        {
           this.isList = isList;
            OnBack = new Command(Back);
        }

        private async void Back(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?islist={isList}");
        }
    }
}