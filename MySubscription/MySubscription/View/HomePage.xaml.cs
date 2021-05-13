using MySubscription.ViewModel;
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
    public partial class HomePage : ContentPage
    {
        SubscriptionVM vm;
        public HomePage()
        {
            InitializeComponent();

            vm = Resources["vme"] as SubscriptionVM;

            var assembly = typeof(HomePage);

            addtoolbar.IconImageSource = ImageSource.FromResource("MySubscription.Assets.Images.solidplus.png", assembly);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!Auth.IsAuthenticated())
            {
                await Task.Delay(300);
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                vm.ReadCollection();
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewSubscriptionPage());
        }
    }
}