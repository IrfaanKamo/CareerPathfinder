namespace ProjectPathfinder.Infrastructure.Helpers
{
    public static class CookieData
    {
        private static char delimiter = '-';

        //sets cookie data separated by delimiters
        public static string SetCookieName(string id, string firstName)
        {
            return id + delimiter + firstName + delimiter;
        }

        //gets the id from the first position
        public static int GetUserId(string cookieUsername)
        {
            return int.Parse(cookieUsername.Split(delimiter)[0]);
        }

        //gets the name from the second position
        public static string GetUserName(string cookieUsername)
        {
            return cookieUsername.Split(delimiter)[1];
        }
    }
}