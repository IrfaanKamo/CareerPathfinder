using ProjectPathfinder.Models;
using NHibernate.Linq;
using System.Linq;
using System.Web;
using ProjectPathfinder.Infrastructure.Helpers;

namespace ProjectPathfinder
{
    public static class Auth
    {
        private const string UserKey = "ProjectPathfinder.Auth.UserKey";

        public static User User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as User;
                if (user == null)
                {
                    user = Database.Session.Query<User>().FirstOrDefault(u => u.Id == CookieData.GetUserId(HttpContext.Current.User.Identity.Name));

                    if (user == null)
                        return null;

                    HttpContext.Current.Items[UserKey] = user;
                }

                return user;
            }
        }       
    }
}