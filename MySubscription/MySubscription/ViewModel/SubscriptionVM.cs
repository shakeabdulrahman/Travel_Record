using MySubscription.Model;
using MySubscription.View;
using MySubscription.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MySubscription.ViewModel
{
    public class SubscriptionVM : INotifyPropertyChanged
    {
        private Subscription selectedsubscription;

        public Subscription SelectedSubscription
        {
            get { return selectedsubscription; }
            set 
            { 
                selectedsubscription = value;
                if(SelectedSubscription != null)
                {
                    App.Current.MainPage.Navigation.PushAsync(new DetailPage(selectedsubscription));
                }
            }
        }

        public ObservableCollection<Subscription> Subscriptions { get; set; }

        public SubscriptionVM()
        {
            Subscriptions = new ObservableCollection<Subscription>();
        }

        

        public async void ReadCollection()
        {
            var subscription = await FirestoreHelper.ReadSubscription();

            Subscriptions.Clear();
            foreach (var s in subscription)
            {
                Subscriptions.Add(s);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
