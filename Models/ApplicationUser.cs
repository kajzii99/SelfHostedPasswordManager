//using Microsoft.AspNet.Identity;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Identity;
//using System.Security.Claims;

//namespace SelfHostedPasswordManager.Models
//{
//    public class ApplicationUser : IdentityUser, IUser
//    {
//        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(Microsoft.AspNet.Identity.UserManager<ApplicationUser> manager)
//        {
//            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
//            // Add custom user claims here
//            return userIdentity;
//        }

//        public virtual ICollection<Credential> Credentials { get; set; }    

//        public ApplicationUser()
//        {
//            this.Credentials = new List<Credential>();
//        }
//    }
//}
