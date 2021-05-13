using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Java.Interop;
using Java.Util;
using MySubscription.Model;
using MySubscription.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(MySubscription.Droid.Dependencies.Firestore))]
namespace MySubscription.Droid.Dependencies
{
    class Firestore : Java.Lang.Object, IFirestore, IOnCompleteListener
    {
        public IntPtr Handle => throw new NotImplementedException();

        List<Subscription> subsriptions;
        bool hasreadvalue = false;

        public Firestore()
        {
            subsriptions = new List<Subscription>();
        }


        public async Task<bool> DeleteSubscription(Subscription subscription)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Subscription");
                collection.Document(subscription.Id).Delete();

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
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Subscription");
                var document = new JavaDictionary<string, Java.Lang.Object>
                {
                   {"author", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                   {"Title", subscription.Name },
                   {"Visited", subscription.IsActive },
                   {"Subscribed Date", DateToNative(subscription.SubscribedDate) },
                    {"Place", subscription.VenueName },
                    {"Address", subscription.Address },
                    {"Distance from current location (meters)", subscription.Distance },
                    {"Category Id", subscription.CategoryId },
                    {"Category Name", subscription.CategoryName },
                    {"Latitude", subscription.Lat },
                    {"Longitude", subscription.Lng }
                };
                
                collection.Add(document);

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
                hasreadvalue = false;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Subscription");
                var query = collection.WhereEqualTo("author", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                query.Get().AddOnCompleteListener(this);

                for (int i = 0; i < 25; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    if (hasreadvalue == true)
                        break;
                }
                return subsriptions;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateSubscription(Subscription subscription)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Subscription");
                collection.Document(subscription.Id).Update("Title", subscription.Name, "Visited", subscription.IsActive);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static Date DateToNative(DateTime date)
        {
            long timeutcinmillisecond = (long)date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            return new Date(timeutcinmillisecond);
        }

        /*public static DateTime NativeToDatetime(Date date)
        {
            DateTime reference = System.TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return reference.AddMilliseconds(date.Time);
        }*/

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if(task.IsSuccessful)
            {
                var document = (QuerySnapshot) task.Result;

                subsriptions.Clear();
                foreach (var doc in document.Documents)
                {
                    var subscription = new Subscription
                    {
                        IsActive = (bool)doc.Get("Visited"),
                        Name = doc.Get("Title").ToString(),
                        UserId = doc.Get("author").ToString(),
                        VenueName = doc.Get("Place").ToString(),
                        //Address = doc.Get("Address").ToString(),
                        Distance = (int)doc.Get("Distance from current location (meters)"),
                        CategoryId = doc.Get("Category Id").ToString(),
                        CategoryName = doc.Get("Category Name").ToString(),
                        Lat = (double)doc.Get("Latitude"),
                        Lng = (double)doc.Get("Longitude"),
                        //SubscribedDate = NativeToDatetime(doc.Get("subscribeddate") as Date)
                        Id = doc.Id
                    };
                    subsriptions.Add(subscription);
                }
                hasreadvalue = true;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again", "Okay");
            }
        }
    }
}