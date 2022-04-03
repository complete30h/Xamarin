﻿using System;
using System.Collections.Generic;
using System.Text;
using GamesCatalog.Services;
using Xamarin.Forms;

namespace GamesCatalog.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IFirebaseAuthentication _firebaseAuthentication;

        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

        public bool IsLandscape { get; set; }

        public Command Register { get; }

        public RegisterViewModel()
        {
            _firebaseAuthentication = DependencyService.Get<IFirebaseAuthentication>();

            Register = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            if (Password == RePassword && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Email))
            {
                if (Password.Length > 5)
                {
                    bool isRegistrationSuccessful = await _firebaseAuthentication.RegisterAsync(Email, Password);

                    if (isRegistrationSuccessful)
                    {
                        await Shell.Current.GoToAsync($"ItemsPage?flag={false}");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Registration error",
                            "There is already a user with this email", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Registration error",
                            "Password must be more than 6 characters", "OK");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email))
                    await Shell.Current.DisplayAlert("Registration error",
                        "Fields must be filled", "OK");
                if (Password != RePassword && !string.IsNullOrWhiteSpace(Password))
                    await Shell.Current.DisplayAlert("Registration error",
                        "PasswordsError", "OK");
            }
        }
    }
}
