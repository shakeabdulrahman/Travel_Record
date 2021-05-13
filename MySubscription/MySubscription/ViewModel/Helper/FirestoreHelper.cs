using MySubscription.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MySubscription.ViewModel.Helper
{
    public interface IFirestore
    {
        bool InsertSubscription(Subscription subscription);
        Task<bool> DeleteSubscription(Subscription subscription);
        bool UpdateSubscription(Subscription subscription);
        Task<IList<Subscription>> ReadSubscription();
    }
    public class FirestoreHelper
    {
        public static IFirestore firestore = DependencyService.Get<IFirestore>();
        public static Task<bool> DeleteSubscription(Subscription subscription)
        {
            return firestore.DeleteSubscription(subscription);
        }

        public static bool InsertSubscription(Subscription subscription)
        {
            return firestore.InsertSubscription(subscription);
        }

        public static Task<IList<Subscription>> ReadSubscription()
        {
            return firestore.ReadSubscription();
        }

        public static bool UpdateSubscription(Subscription subscription)
        {
            return firestore.UpdateSubscription(subscription);
        }
    }
}
