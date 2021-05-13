using MySubscription.View;
using MySubscription.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySubscription.ViewModel
{
    public class SignOutVM
    {
        public ICommand SignoutCommand { get; set; }


        public SignOutVM()
        {
            SignoutCommand = new Command(Signout);
        }

        private async  void Signout(object parameter)
        {
            bool response = await App.Current.MainPage.DisplayAlert("", "Do you want to Logout?", "Yes", "No");
            if (response)
            {
                Auth.SignOut();
                await App.Current.MainPage.Navigation.PushAsync(new Home());
            }
            else
                return;
        }
    }
}
