using MySubscription.Logic;
using MySubscription.Model;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySubscription.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewSubscriptionPage : ContentPage
    {
        private IList<Venue> selectedvenue;

        public IList<Venue> SelectedVenue
        {
            get { return selectedvenue; }
            set { selectedvenue = value; }
        }


        public NewSubscriptionPage()
        {
            InitializeComponent();

            var assembly = typeof(NewSubscriptionPage);

            savetoolbar1.IconImageSource = ImageSource.FromResource("MySubscription.Assets.Images.save2.png", assembly);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            loading.IsVisible = true;
            loading.IsRunning = true;

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues  = await VenueLogic.Getvenues(position.Latitude, position.Longitude);
            venuelist.ItemsSource = venues;

            loading.IsRunning = false;
            loading.IsVisible = false;
        }
    }
}