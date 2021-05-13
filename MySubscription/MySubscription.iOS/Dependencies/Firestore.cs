using Foundation;
using MySubscription.Model;
using MySubscription.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MySubscription.iOS.Dependencies.Firestore))]
namespace MySubscription.iOS.Dependencies
{
    class Firestore : IFirestore
    {
        public async Task<bool> DeleteSubscription(Subscription subscription)
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("Subscription");
                 await collection.GetDocument(subscription.Id).DeleteDocumentAsync();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool InsertSubscription(Subscription subscription)
        {
            try
            {
                var keys = new[]
                {
                new NSString("author"),
                new NSString("Title"),
                new NSString("Visited"),
                new NSString("Subscribed Date"),
                new NSString("Place"),
                new NSString("Address"),
                new NSString("Distance from current location (meters)"),
                new NSString("Category Id"),
                new NSString("Category Name"),
                new NSString("Latitude"),
                new NSString("Longitude")
                };

                var values = new NSObject[]
                {
                new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid),
                new NSString(subscription.Name),
                new NSNumber(subscription.IsActive),
                DateToNSDate(subscription.SubscribedDate),
                new NSString(subscription.VenueName),
                new NSString(subscription.Address),
                new NSNumber(subscription.Distance),
                new NSString(subscription.CategoryId),
                new NSString(subscription.CategoryName),
                new NSNumber(subscription.Lat),
                new NSNumber(subscription.Lng)
                };

                var document = new NSDictionary<NSString, NSObject>(keys, values);
                Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("Subscription").AddDocument(document);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<Subscription>> ReadSubscription()
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("Subscription");
                var query = collection.WhereEqualsTo("author", Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid);
                var documents = await query.GetDocumentsAsync();

                List<Subscription> subscriptions = new List<Subscription>();
                foreach (var doc in documents.Documents)
                {
                    var subscriptiondictionary = doc.Data;
                    var subscription = new Subscription
                    {
                        IsActive = (bool)(subscriptiondictionary.ValueForKey(new NSString("Visited")) as NSNumber),
                        Name = subscriptiondictionary.ValueForKey(new NSString("name")) as NSString,
                        UserId = subscriptiondictionary.ValueForKey(new NSString("author")) as NSString,
                        SubscribedDate = FIRtimestamptoDatetime(subscriptiondictionary.ValueForKey(new NSString("subscribeddate")) as Firebase.CloudFirestore.Timestamp),
                        VenueName = subscriptiondictionary.ValueForKey(new NSString("Place")) as NSString,
                        Address = subscriptiondictionary.ValueForKey(new NSString("Address")) as NSString,
                        Distance = (int) (subscriptiondictionary.ValueForKey(new NSString("Distance from current location (meters)")) as NSNumber),
                        CategoryId = subscriptiondictionary.ValueForKey(new NSString("Category Id")) as NSString,
                        CategoryName = subscriptiondictionary.ValueForKey(new NSString("Category Name")) as NSString,
                        Lat = (double) (subscriptiondictionary.ValueForKey(new NSString("Latitude")) as NSNumber),
                        Lng = (double)(subscriptiondictionary.ValueForKey(new NSString("Longitude")) as NSNumber),
                        Id = doc.Id
                    };
                    subscriptions.Add(subscription);
                }

                return subscriptions;
            }
            catch(Exception ex)
            {
                return new List<Subscription>();
            }
        }

        public bool UpdateSubscription(Subscription subscription)
        {
            try
            {
                var keys = new[]
                    {
                new NSString("name"),
                new NSString("Visited")
                };

                var values = new NSObject[]
                {
                new NSString(subscription.Name),
                new NSNumber(subscription.IsActive)
                };
                var document = new NSDictionary<NSObject, NSObject>(keys, values);

                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("Subscription");
                collection.GetDocument(subscription.Id).UpdateDataAsync(document);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static NSDate DateToNSDate(DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);

            return (NSDate)date;
        }

        public static DateTime FIRtimestamptoDatetime(Firebase.CloudFirestore.Timestamp date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.Seconds);
        }
    }
}