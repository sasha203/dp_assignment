using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Common;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using DataAccess;

namespace dpBackEnd
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        //Is used to validate client device.
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context != null)
            {

                UsersRepository ur = new UsersRepository();
                List<User> users = ur.getUsers().ToList();
                User user = new User();

                foreach (User u in users)
                {
                    if (u.Username.Equals(context.UserName))
                    {
                        user = ur.getUser(u.Username);

                        if (user.Password.Equals(context.Password))
                        {
                            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                            identity.AddClaim(new Claim("Username", user.Username));
                            identity.AddClaim(new Claim("Email", user.Email));
                            identity.AddClaim(new Claim("Name", user.Name));
                            identity.AddClaim(new Claim("Surname", user.Surname));
                            identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                            context.Validated(identity);
                        }
                    }
                }

            }   
        }

    }
}