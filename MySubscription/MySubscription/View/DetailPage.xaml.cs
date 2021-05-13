using MySubscription.Model;
using MySubscription.ViewModel;
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
    public partial class DetailPage : ContentPage
    {
        DetailVM vm;
        public DetailPage()
        {
            InitializeComponent();

            vm = Resources["dme"] as DetailVM;
        }

        public DetailPage(Subscription selectedsubscription1)
        {
            InitializeComponent();

            var assembly = typeof(DetailPage);

            savetoolbar2.IconImageSource = ImageSource.FromResource("MySubscription.Assets.Images.save2.png", assembly);

            vm = Resources["dme"] as DetailVM;

            vm.Subscription1 = selectedsubscription1;
        }
    }
}