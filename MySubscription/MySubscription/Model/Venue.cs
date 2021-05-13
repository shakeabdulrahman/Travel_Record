using MySubscription.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MySubscription.Model
{
    
    public class Location
    {
        public string address { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int distance { get; set; }
        public string postalCode { get; set; }
        public string cc { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public IList<string> formattedAddress { get; set; }

    }
    public class Categories
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pluralName { get; set; }
        public string shortName { get; set; }
        public bool primary { get; set; }

    }
    public class Venue
    {
        public string UserId { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public IList<Categories> categories { get; set; }
    }
    public class Response
    {
        public IList<Venue> venues { get; set; }
    }

    public class Venues
    {
        public Response response { get; set; }

        public static string GenerateUrl(double latitude, double longitude)
        {
            return string.Format(Constant.Venue_search, latitude, longitude, Constant.Client_Id, Constant.Client_Secret, DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}
