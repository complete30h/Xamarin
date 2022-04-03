using GamesCatalog.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using GamesCatalog.Services;

namespace GamesCatalog.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public  IFirebaseAuthentication _firebaseAuthentication;
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginViewModel()
        {
           _firebaseAuthentication = DependencyService.Get<IFirebaseAuthentication>();
            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnSignUpClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            bool isAuthSuccessful = await _firebaseAuthentication.LoginAsync(Email, Password);
            //await Shell.Current.GoToAsync($"ItemsPage?flag={false}");
             if (isAuthSuccessful)
             {
 
                await Shell.Current.GoToAsync($"ItemsPage?flag={false}");

            }
            else
             {
                 await Shell.Current.DisplayAlert("Login error",
                     "Logout Failed", "OK");
             }
            
        }

        private async void OnSignUpClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

            await Shell.Current.GoToAsync($"RegisterPage");


        }
    }
}
