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
    public partial class RegisterPage : ContentPage
    {
        private readonly RegisterViewModel _registerViewModel = new RegisterViewModel();
        public RegisterPage()
        {
            BindingContext = _registerViewModel;
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}