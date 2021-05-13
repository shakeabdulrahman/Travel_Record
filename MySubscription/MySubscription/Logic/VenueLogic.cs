using MySubscription.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MySubscription.Logic
{
    public class VenueLogic
    {
        public static async Task<List<Venue>> Getvenues(double latitude, double longitude)
        {
            List<Venue> venues = new List<Venue>();

            var url = Venues.GenerateUrl(latitude, longitude);
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueresult = JsonConvert.DeserializeObject<Venues>(json);

                venues = venueresult.response.venues as List<Venue>;
            }
            return venues;
        }
    }
}
