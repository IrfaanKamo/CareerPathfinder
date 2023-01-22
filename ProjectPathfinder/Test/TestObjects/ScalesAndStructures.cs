using System;
using System.Collections.Generic;

namespace ProjectPathfinder.Test.TestObjects
{
    ///<summary>
    ///Returns values corresponding to items in the Likert Scale
    ///</summary>
    public enum LikertScale { VerySure, Sure, Unsure, VeryUnsure }

    //-------------------------------------------------------------------------------------------

    ///<summary>
    ///Stores the first and second year results for each school subject
    ///</summary>
    [Serializable]
    public class SubjectResult
    {
        public string SubjectName { get; set; }
        public int? YearBeforeLastMark { get; set; }
        public int? PreviousYearMark { get; set; }

        public SubjectResult() { }
    }

    //-------------------------------------------------------------------------------------------

    [Serializable]
    public sealed class Interests
    {
        public List<string> interestsList;
        static readonly Interests _instance = new Interests();

        public static Interests Instance
        {
            get { return _instance; }            
        }

        private Interests()
        {
            interestsList = new List<string>();
            interestsList.Add("Technical/Skilled");
            interestsList.Add("Nature");
            interestsList.Add("Science");
            interestsList.Add("Language");
            interestsList.Add("Art");
            interestsList.Add("People");
            interestsList.Add("Business");
            interestsList.Add("Clerical/Administrative");
        } 
    }

    //-------------------------------------------------------------------------------------------

    [Serializable]
    public sealed class Abilities
    {
        public List<string> abilitiesList;
        static readonly Abilities _instance = new Abilities();

        public static Abilities Instance
        {
            get { return _instance; }
        }

        private Abilities()
        {
            abilitiesList = new List<string>();
            abilitiesList.Add("Practical");
            abilitiesList.Add("Numerical");
            abilitiesList.Add("Artistic");
            abilitiesList.Add("Language");
            abilitiesList.Add("Social");
            abilitiesList.Add("Methodical");
        }
    }

    //-------------------------------------------------------------------------------------------

    [Serializable]
    public sealed class CareerGroups
    {
        public List<string> careerList;
        static readonly CareerGroups _instance = new CareerGroups();

        public static CareerGroups Instance
        {
            get { return _instance; }
        }

        private CareerGroups()
        {
            careerList = new List<string>();
            careerList.Add("Realistic");
            careerList.Add("Investigative");
            careerList.Add("Aesthetic/Artistic");
            careerList.Add("Social");
            careerList.Add("Enterprising");
            careerList.Add("Conventional");
        }
    }

    //-------------------------------------------------------------------------------------------

    [Serializable]
    public sealed class CareerValues
    {
        public List<string> careerList;
        static readonly CareerValues _instance = new CareerValues();

        public static CareerValues Instance
        {
            get { return _instance; }
        }

        private CareerValues()
        {
            careerList = new List<string>();
            careerList.Add("Materialistic Prosperity (money/profit)");
            careerList.Add("Rendering services (working with people)");
            careerList.Add("Status (esteem)");
            careerList.Add("Aesthetics (artistic)");
            careerList.Add("Security");
            careerList.Add("Independence (working on your own)");
        }
    }
}