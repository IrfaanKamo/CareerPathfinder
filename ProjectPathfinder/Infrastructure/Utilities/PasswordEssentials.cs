using System.Text.RegularExpressions;

namespace ProjectPathfinder.Infrastructure.Utilities
{
    public enum PasswordScore
    {
        Blank = 0,
        VeryWeak = 1,
        Weak = 2,
        Medium = 3,
        Strong = 4,
        VeryStrong = 5
    }    

    public class PasswordEssentials
    {
        ///<summary>
        ///Returns the strength of a password using the PasswordScore enum.
        /// To test RegEx, go to http://www.regexpal.com/
        ///</summary>
        public static PasswordScore CheckStrength(string password)
        {
            int score = 0;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 8)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (Regex.IsMatch(password, @"[0-9]+(\.[0-9][0-9]?)?", RegexOptions.ECMAScript))   //number only //"^\d+$" if need to match more than one digit.
                score++;
            if (Regex.IsMatch(password, @"[A-Za-z]+", RegexOptions.ECMAScript)) //lower or upper case
                score++;
            if (Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z]).+$", RegexOptions.ECMAScript)) //both, lower and upper case
                score++;
            if (Regex.IsMatch(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript)) //^[A-Z]+$
                score++;

            return (PasswordScore)score;
        }

        //-------------------------------------------------------------------------------------------

        ///<summary>
        ///Given password has to be at least Medium Strength.
        ///</summary>
        public static bool IsPasswordRequirementMet(string password)
        {           
            if ((int)CheckStrength(password) < (int)PasswordScore.Medium)
            {
                return false;
            }
            return true;
        }

        //-------------------------------------------------------------------------------------------
    }
}