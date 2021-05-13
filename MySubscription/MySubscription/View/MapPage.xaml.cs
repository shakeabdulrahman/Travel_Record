using MySubscription.ViewModel;
using MySubscription.ViewModel.Helper;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySubscription.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        

        bool haslocationpermission = false;
        public MapPage()
        {
            InitializeComponent();


            var assembly = typeof(MapPage);
            logouttoolbar.IconImageSource = ImageSource.FromResource("MySubscription.Assets.Images.logout.png", assembly);

            GetPermission();
        }

        private async void GetPermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
            if(status != PermissionStatus.Granted)
            {
                if(await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                {
                    await DisplayAlert("Sorry", "We need Location's permission", "Okay");
                }

                var result = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                if(result == PermissionStatus.Granted)
                {
                    status = result;
                }
            }
            if(status == PermissionStatus.Granted)
            {
                locationmaps.IsShowingUser = true;
                haslocationpermission = true;
                GetLocation();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (haslocationpermission)
            {
                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
                locator.StartListeningAsync(TimeSpan.Zero, 100);
            }

            GetLocation();
            DisplayInMap();
        }

        private async void DisplayInMap()
        {
            var subscription = await FirestoreHelper.ReadSubscription();

            foreach (var item in subscription)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(item.Lat, item.Lng);

                    var pin = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = position,
                        Label = item.VenueName,
                        Address = item.Address
                    };
                    locationmaps.Pins.Add(pin);
                }
                catch(Exception ex)
                {
                    await DisplayAlert("Message", ex.Message, "Okay");
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged;
            CrossGeolocator.Current.StopListeningAsync();
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private async void GetLocation()
        {
            if (haslocationpermission)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                MoveMap(position);
            }
            else
            {
                await DisplayAlert("Error", "Need Location's Permission", "Okay");
            }
        }

        private void MoveMap(Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var mapspan = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            locationmaps.MoveToRegion(mapspan);
        }
    }
}