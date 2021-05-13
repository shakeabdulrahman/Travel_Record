using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MySubscription.Model
{
    public class Subscription
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime SubscribedDate { get; set; }


        private bool isactive;
        public bool IsActive 
        { 
            get { return isactive; }
            set
            {
                isactive = value;
                if (isactive)
                {
                    ActiveTrue = "Yes";
                }
                else
                {
                    ActiveTrue = "No";
                }
            }
        }

        public string ActiveTrue { get; set; }
        public string ActiveFalse { get; set; }
        public string VenueName { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int Distance { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
    }
}
