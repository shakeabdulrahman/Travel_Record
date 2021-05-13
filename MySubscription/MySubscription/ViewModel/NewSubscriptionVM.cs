using MySubscription.Model;
using MySubscription.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySubscription.ViewModel
{
    class NewSubscriptionVM : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private bool isactive;

        public bool IsActive
        {
            get { return isactive; }
            set 
            { 
                isactive = value;
                OnPropertyChanged("IsActive");
            }
        }

        private Venue selectedvenue;

        public Venue SelectedVenue
        {
            get { return selectedvenue; }
            set 
            { 
                selectedvenue = value;
                var firstcategory = selectedvenue.categories.FirstOrDefault();
                Name1 = selectedvenue.name;
                Address = selectedvenue.location.address;
                Distance = selectedvenue.location.distance;
                CategoryId = firstcategory.id;
                CategoryName = firstcategory.name;
                Latitutde = selectedvenue.location.lat;
                Longitude = selectedvenue.location.lng;
                OnPropertyChanged("SelectedVenue");
            }
        }

        private string name1;

        public string Name1
        {
            get { return name1; }
            set 
            { 
                name1 = value;
                OnPropertyChanged("Name1");
            }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set 
            { 
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private int distance;

        public int Distance
        {
            get { return distance; }
            set 
            { 
                distance = value;
                OnPropertyChanged("Distance");
            }
        }

        private string categoryid;

        public string CategoryId
        {
            get { return categoryid; }
            set 
            { 
                categoryid = value;
                OnPropertyChanged("CategoryId");
            }
        }

        private string categoryname;

        public string CategoryName
        {
            get { return categoryname; }
            set 
            { 
                categoryname = value;
                OnPropertyChanged("CategoryName");
            }
        }

        private double latitude;

        public double Latitutde
        {
            get { return latitude; }
            set 
            { 
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }


        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set 
            { 
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }





        public ICommand SaveCommand { get; set; }

        public NewSubscriptionVM()
        {
            SaveCommand = new Command(Save, CanSaveExecute);
        }

        private bool CanSaveExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Name);
        }

        private void Save(object parameter)
        {
            if (SelectedVenue != null)
            {
                bool result = FirestoreHelper.InsertSubscription(new Subscription
                {
                    UserId = Auth.GetUserId(),
                    Name = Name,
                    IsActive = IsActive,
                    SubscribedDate = DateTime.Now,
                    VenueName = Name1,
                    Address = Address,
                    Distance = Distance,
                    CategoryId = CategoryId,
                    CategoryName = CategoryName,
                    Lat = Latitutde,
                    Lng = Longitude
                });
                if (result)
                {
                    App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again", "Okay");
                }
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error", "Please select any place", "Okay");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
