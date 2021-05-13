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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Registerbuttonclicked(object sender, EventArgs e)
        {
            loginstacklayout.IsVisible = false;
            registerstacklayout.IsVisible = true;
        }

        private void Loginbuttonclicked(object sender, EventArgs e)
        {
            registerstacklayout.IsVisible = false;
            loginstacklayout.IsVisible = true;
        }
    }
}