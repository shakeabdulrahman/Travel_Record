using MySubscription.View;
using MySubscription.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySubscription.ViewModel
{
    class LoginVM : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("CanLogin");
                OnPropertyChanged("CanRegister");
            }
        }



        private string email;

        public string Email
        {
            get { return email; }
            set 
            { 
                email = value;
                OnPropertyChanged("Email");
                OnPropertyChanged("CanLogin");
                OnPropertyChanged("CanRegister");
            }
        }



        private string password;

        public string Password
        {
            get { return password; }
            set 
            { 
                password = value;
                OnPropertyChanged("Password");
                OnPropertyChanged("CanLogin");
                OnPropertyChanged("CanRegister");
            }
        }



        private string confirmpassword;

        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set 
            { 
                confirmpassword = value;
                OnPropertyChanged("ConfirmPassword");
                OnPropertyChanged("CanLogin");
                OnPropertyChanged("CanRegister");
            }
        }

        
        public bool CanLogin
        {
            get 
            {
                return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
            }
        }


        public bool CanRegister
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(ConfirmPassword);
            }
        }


        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public LoginVM()
        {
            LoginCommand = new Command(Login, CanloginExecute);
            RegisterCommand = new Command(Register, CanRegisterExecute);
        }

        private bool CanRegisterExecute(object parameter)
        {
            return CanRegister;
        }

        private async void Register(object parameter)
        {
            if (ConfirmPassword != Password)
                await App.Current.MainPage.DisplayAlert("Error", "Password does not match", "Okay");
            else
            {
                bool result = await Auth.RegisterUser(Name, Email, Password);
                if (result)
                    await App.Current.MainPage.Navigation.PopAsync();
            }
        }

        private bool CanloginExecute(object parameter)
        {
            return CanLogin;
        }

        private async void Login(object parameter)
        {
            bool result = await Auth.AuthenticateUser(Email, Password);
            if (result)
                await App.Current.MainPage.Navigation.PushAsync(new Home());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
