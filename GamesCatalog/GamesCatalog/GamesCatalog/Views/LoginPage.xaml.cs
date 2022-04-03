using GamesCatalog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GamesCatalog.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel viewModel = new LoginViewModel();
        public LoginPage()
        {
            BindingContext = viewModel;
            InitializeComponent();
        }

        async void bebrus(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"ItemsPage?flag={true}");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Email.Text = "";
            Password.Text = "";
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Email.Text = "";
            Password.Text = "";
        }
    }
}