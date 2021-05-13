using Foundation;
using MySubscription.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MySubscription.iOS.Dependencies.Auth))]
namespace MySubscription.iOS.Dependencies
{
    class Auth : IAuth
    {
        public async Task<bool> AuthenticateUser(string email, string password)
        {
            try
            {
                await Firebase.Auth.Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public string GetUserId()
        {
            return Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid;
        }

        public bool IsAuthenticated()
        {
            return Firebase.Auth.Auth.DefaultInstance.CurrentUser != null;
        }

        public async Task<bool> RegisterUser(string name, string email, string password)
        {
            try
            {
                await Firebase.Auth.Auth.DefaultInstance.CreateUserAsync(email, password);
                var changerequest = Firebase.Auth.Auth.DefaultInstance.CurrentUser.ProfileChangeRequest();
                changerequest.DisplayName = name;
                await changerequest.CommitChangesAsync();

                return true;
            }
            catch(Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public bool SignOut()
        {
            try
            {
                Firebase.Auth.Auth.DefaultInstance.SignOut(out NSError error);
                return error == null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}