using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Owin;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user 
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            var facebookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
            {
                AppId = "450528018407928",
                AppSecret = "b81e567559496a1d49186c9e28065e6e",
                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                        {
                            // All data from facebook in this object. 
                            var rawUserObjectFromFacebookAsJson = context.User;

                            // Only some of the basic details from facebook 
                            // like id, username, email etc are added as claims.
                            // But you can retrieve any other details from this
                            // raw Json object from facebook and add it as claims here.
                            // Subsequently adding a claim here will also send this claim
                            // as part of the cookie set on the browser so you can retrieve
                            // on every successive request. 
                            context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                            context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:gender", ((dynamic)context.User).gender.ToString(), ClaimValueTypes.String, "Facebook"));

                            return Task.FromResult(0);
                        }
                }
            };

            app.UseFacebookAuthentication(facebookOptions);

            //app.UseGoogleAuthentication();
        }
    }
}