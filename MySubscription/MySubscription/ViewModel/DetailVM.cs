using MySubscription.Model;
using MySubscription.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySubscription.ViewModel
{
    class DetailVM : INotifyPropertyChanged
    {
        private Subscription subscription1;

        public Subscription Subscription1
        {
            get { return subscription1; }
            set
            {
                subscription1 = value;
                Name = Subscription1.Name;
                IsActive = Subscription1.IsActive;
                OnPropertyChanged("Subscription1");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Subscription1.Name = name;
                OnPropertyChanged("Name");
                OnPropertyChanged("Subscription1");
            }
        }

        private bool isactive;

        public bool IsActive
        {
            get { return isactive; }
            set
            {
                isactive = value;
                Subscription1.IsActive = isactive;
                OnPropertyChanged("IsActive");
                OnPropertyChanged("Subscription1");
            }
        }

       


        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public DetailVM()
        {
            UpdateCommand = new Command(Update, CanUpdate);
            DeleteCommand = new Command(Delete);
        }

        private async void Delete(object parameter)
        {
            bool result = await FirestoreHelper.DeleteSubscription(Subscription1);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again", "Okay");
        }

        private bool CanUpdate(object parameter)
        {
            return !string.IsNullOrEmpty(Name);
        }

        private void Update(object parameter)
        {
            bool result = FirestoreHelper.UpdateSubscription(Subscription1);

            if (result)
                App.Current.MainPage.Navigation.PopAsync();
            else
                App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again", "Okay");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
