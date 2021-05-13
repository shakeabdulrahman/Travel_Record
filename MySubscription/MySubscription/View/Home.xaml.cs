using MySubscription.ViewModel.Helper;
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
    public partial class Home : TabbedPage
    {
        public Home()
        {
            InitializeComponent();

            var assembly = typeof(Home);

            home.IconImageSource = ImageSource.FromResource("MySubscription.Assets.Images.history2.png", assembly);
            map.IconImageSource = ImageSource.FromResource("MySubscription.Assets.Images.mappin.png", assembly);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(!Auth.IsAuthenticated())
            {
                Task.Delay(300);
                Navigation.PushAsync(new LoginPage());
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}