using ProjectPathfinder.Test.TestObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ProjectPathfinder.Areas.Member.ViewModels
{
    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepTwo
    {
        [Required]
        public bool? q1 { get; set; }
        [Required]
        public bool? q2 { get; set; }
        [Required]
        public bool? q3 { get; set; }
        [Required]
        public bool? q4 { get; set; }
        [Required]
        public bool? q5 { get; set; }
        [Required]
        public bool? q6 { get; set; }
        [Required]
        public bool? q7 { get; set; }
        [Required]
        public bool? q8 { get; set; }
        [Required]
        public bool? q9 { get; set; }
        [Required]
        public bool? q10 { get; set; }
        [Required]
        public bool? q11 { get; set; }
        [Required]
        public bool? q12 { get; set; }

        public TestStepTwo(bool autoFill)
        {
            if (autoFill)
                q1 = q2 = q3 = q4 = q5 = q6 = q7 = q8 = q9 = q10 = q11 = q12 = true;
        }
        public TestStepTwo() { }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepThree
    {
        [Required]
        public bool? q1_1 { get; set; }
        [Required]
        public bool? q1_2 { get; set; }
        [Required]
        public bool? q1_3 { get; set; }
        [Required]
        public bool? q1_4 { get; set; }
        [Required]
        public bool? q2_1 { get; set; }
        [Required]
        public bool? q2_2 { get; set; }
        [Required]
        public bool? q2_3 { get; set; }
        [Required]
        public bool? q2_4 { get; set; }
        [Required]
        public bool? q3_1 { get; set; }
        [Required]
        public bool? q3_2 { get; set; }
        [Required]
        public bool? q3_3 { get; set; }
        [Required]
        public bool? q3_4 { get; set; }
        [Required]
        public bool? q4_1 { get; set; }
        [Required]
        public bool? q4_2 { get; set; }
        [Required]
        public bool? q4_3 { get; set; }
        [Required]
        public bool? q4_4 { get; set; }
        [Required]
        public bool? q5_1 { get; set; }
        [Required]
        public bool? q5_2 { get; set; }
        [Required]
        public bool? q5_3 { get; set; }
        [Required]
        public bool? q5_4 { get; set; }
        [Required]
        public bool? q6_1 { get; set; }
        [Required]
        public bool? q6_2 { get; set; }
        [Required]
        public bool? q6_3 { get; set; }
        [Required]
        public bool? q6_4 { get; set; }

        public TestStepThree(bool autoFill)
        {
            if (autoFill)
            {
                q1_1 = q1_2 = q1_3 = q1_4 = q2_1 = q2_2 = q2_3 = q2_4 = q3_1 = q3_2 = q3_3 = q3_4 = true;
                q4_1 = q4_2 = q4_3 = q4_4 = q5_1 = q5_2 = q5_3 = q5_4 = q6_1 = q6_2 = q6_3 = q6_4 = true;
            }

        }
        public TestStepThree() { }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepFour
    {
        public int? Grade { get; set; }
        [Required]
        public bool? ChosenCareer { get; set; }
        [Required]
        public LikertScale? CorrectChoice { get; set; }
        public bool? ChosenGrade10Sub { get; set; }
        public LikertScale? CorrectGrade10Choice { get; set; }
        [StringLength(160)]
        public string ProblemInSubjects { get; set; }

        //Subjects
        [Required]
        public string subject1 { get; set; }
        [Required]
        public string subject2 { get; set; }
        [Required]
        public string subject3 { get; set; }
        [Required]
        public string subject4 { get; set; }
        [Required]
        public string subject5 { get; set; }
        [Required]
        public string subject6 { get; set; }
        public string subject7 { get; set; }
        public string subject8 { get; set; }
        public string subject9 { get; set; }

        //Most Liked
        [Required]
        public string mostLikedSubject1 { get; set; }
        [Required]
        public string mostLikedSubject2 { get; set; }

        //Most Disliked
        [Required]
        public string mostDislikedSubject1 { get; set; }
        [Required]
        public string mostDislikedSubject2 { get; set; }

        //Results
        [Required]
        public SubjectResult result1 { get; set; }
        [Required]
        public SubjectResult result2 { get; set; }
        [Required]
        public SubjectResult result3 { get; set; }
        [Required]
        public SubjectResult result4 { get; set; }
        [Required]
        public SubjectResult result5 { get; set; }
        [Required]
        public SubjectResult result6 { get; set; }
        public SubjectResult result7 { get; set; }
        public SubjectResult result8 { get; set; }
        public SubjectResult result9 { get; set; }

        public TestStepFour()
        {
            result1 = new SubjectResult();
            result2 = new SubjectResult();
            result3 = new SubjectResult();
            result4 = new SubjectResult();
            result5 = new SubjectResult();
            result6 = new SubjectResult();
            result7 = new SubjectResult();
            result8 = new SubjectResult();
            result9 = new SubjectResult();
        }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepFive
    {
        public List<bool> Personality_Realistic { get; set; }
        public List<bool> Personality_Investigative { get; set; }
        public List<bool> Personality_Aesthetic { get; set; }
        public List<bool> Personality_Social { get; set; }
        public List<bool> Personality_Enterprising { get; set; }
        public List<bool> Personality_Conventional { get; set; }

        public List<bool> Job_Realistic { get; set; }
        public List<bool> Job_Investigative { get; set; }
        public List<bool> Job_Aesthetic { get; set; }
        public List<bool> Job_Social { get; set; }
        public List<bool> Job_Enterprising { get; set; }
        public List<bool> Job_Conventional { get; set; }

        [Required]
        public string Word_Realistic { get; set; }
        [Required]
        public string Word_Investigative { get; set; }
        [Required]
        public string Word_Aesthetic { get; set; }
        [Required]
        public string Word_Social { get; set; }
        [Required]
        public string Word_Enterprising { get; set; }
        [Required]
        public string Word_Conventional { get; set; }

        public TestStepFive()
        {
            Personality_Realistic = new List<bool>(new bool[8]);
            Personality_Investigative = new List<bool>(new bool[8]);
            Personality_Aesthetic = new List<bool>(new bool[8]);
            Personality_Social = new List<bool>(new bool[8]);
            Personality_Enterprising = new List<bool>(new bool[12]);
            Personality_Conventional = new List<bool>(new bool[15]);

            Job_Realistic = new List<bool>(new bool[15]);
            Job_Investigative = new List<bool>(new bool[15]);
            Job_Aesthetic = new List<bool>(new bool[15]);
            Job_Social = new List<bool>(new bool[15]);
            Job_Enterprising = new List<bool>(new bool[12]);
            Job_Conventional = new List<bool>(new bool[15]);
        }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepFiveContinuation
    {
        [Required]
        public Impact? Impact_Choice { get; set; }
        [Required]
        public Dominance? Dominance_Choice { get; set; }
        [Required]
        public Leadership? Leadership_Choice { get; set; }
        [Required]
        public Drive? Drive_Choice { get; set; }
        [Required]
        public Reliability? Reliability_Choice { get; set; }
        [Required]
        public Courage? Courage_Choice { get; set; }
        [Required]
        public Friendliness? Friendliness_Choice { get; set; }
        [Required]
        public Sensitivity? Sensitivity_Choice { get; set; }
        [Required]
        public Flexibility? Flexibility_Choice { get; set; }
        [Required]
        public Stability? Stability_Choice { get; set; }
        [Required]
        public Humour? Humour_Choice { get; set; }
        [Required]
        public Patience? Patience_Choice { get; set; }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepSix
    {
        [Required]
        public string Interest_1 { get; set; }
        [Required]
        public string Interest_2 { get; set; }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepSeven
    {
        public string Interest_1 { get; set; }
        public string Interest_2 { get; set; }

        [Required]
        public string Ability_1 { get; set; }
        [Required]
        public string Ability_2 { get; set; }
        [Required]
        public string CareerForInterest_1 { get; set; }
        [Required]
        public string CareerForInterest_2 { get; set; }
        [Required]
        public string AbilityForInterest_1 { get; set; }
        [Required]
        public string AbilityForInterest_2 { get; set; }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepEight
    {
        [Required]
        public string CareerValue_1 { get; set; }
        [Required]
        public string CareerValue_2 { get; set; }
        [Required]
        public string CareerGroup_1 { get; set; }
        [Required]
        public string CareerGroup_2 { get; set; }
        [Required]
        public string CareerChoice_1 { get; set; }
        [Required]
        public string CareerChoice_2 { get; set; }
        [Required]
        public string CareerChoice_First { get; set; }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepNine
    {
        [Required]
        public bool? q1 { get; set; }
        [Required]
        public bool? q2 { get; set; }
        [Required]
        public bool? q3 { get; set; }
        [Required]
        public bool? q4 { get; set; }
        [Required]
        public bool? q5 { get; set; }
        [Required]
        public bool? q6 { get; set; }
        [Required]
        public bool? q7 { get; set; }
        [Required]
        public bool? q8 { get; set; }
        [Required]
        public bool? q9 { get; set; }
        [Required]
        public bool? q10 { get; set; }
        [Required]
        public bool? q11 { get; set; }
        [Required]
        public bool? q12 { get; set; }
        [Required]
        public bool? q13 { get; set; }
        [Required]
        public bool? q14 { get; set; }
        [Required]
        public bool? q15 { get; set; }
        [Required]
        public bool? q16 { get; set; }
        [Required]
        public bool? q17 { get; set; }
        [Required]
        public bool? q18 { get; set; }
        [Required]
        public bool? q19 { get; set; }
        [Required]
        public bool? q20 { get; set; }
        [Required]
        public bool? q21 { get; set; }
        [Required]
        public bool? q22 { get; set; }
        [Required]
        public bool? q23 { get; set; }
        [Required]
        public bool? q24 { get; set; }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepTen
    {
        [Required]
        public string q1 { get; set; }
        [Required]
        public string q2 { get; set; }
        [Required]
        public string q3 { get; set; }
        [Required]
        public string q4 { get; set; }
        [Required]
        public string q5 { get; set; }
        [Required]
        public string q6 { get; set; }
        [Required]
        public string q7 { get; set; }
        [Required]
        public string q8 { get; set; }
        [Required]
        public string q9 { get; set; }
        [Required]
        public string q10 { get; set; }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepEleven_1
    {
        [Required]
        public bool? q1 { get; set; }
        [Required]
        public bool? q2 { get; set; }
        [Required]
        public bool? q3 { get; set; }
        [Required]
        public bool? q4 { get; set; }
        [Required]
        public bool? q5 { get; set; }
        [Required]
        public bool? q6 { get; set; }
        [Required]
        public bool? q7 { get; set; }
        [Required]
        public bool? q8 { get; set; }
        [Required]
        public bool? q9 { get; set; }
        [Required]
        public bool? q10 { get; set; }
        [Required]
        public bool? q11 { get; set; }
        [Required]
        public bool? q12 { get; set; }
        [Required]
        public bool? q13 { get; set; }
        [Required]
        public bool? q14 { get; set; }
        [Required]
        public bool? q15 { get; set; }
        [Required]
        public bool? q16 { get; set; }
        [Required]
        public bool? q17 { get; set; }
        [Required]
        public bool? q18 { get; set; }
        [Required]
        public bool? q19 { get; set; }
        [Required]
        public bool? q20 { get; set; }
        [Required]
        public bool? q21 { get; set; }
        [Required]
        public bool? q22 { get; set; }
        [Required]
        public bool? q23 { get; set; }
        [Required]
        public bool? q24 { get; set; }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepEleven_2
    {
        [Required]
        public bool? q1 { get; set; }
        [Required]
        public bool? q2 { get; set; }
        [Required]
        public bool? q3 { get; set; }
        [Required]
        public bool? q4 { get; set; }
        [Required]
        public bool? q5 { get; set; }
        [Required]
        public bool? q6 { get; set; }
        [Required]
        public bool? q7 { get; set; }
        [Required]
        public bool? q8 { get; set; }
        [Required]
        public bool? q9 { get; set; }
        [Required]
        public bool? q10 { get; set; }
        [Required]
        public bool? q11 { get; set; }
        [Required]
        public bool? q12 { get; set; }
        [Required]
        public bool? q13 { get; set; }
        [Required]
        public bool? q14 { get; set; }
        [Required]
        public bool? q15 { get; set; }
        [Required]
        public bool? q16 { get; set; }
        [Required]
        public bool? q17 { get; set; }
        [Required]
        public bool? q18 { get; set; }
        [Required]
        public bool? q19 { get; set; }
        [Required]
        public bool? q20 { get; set; }
        [Required]
        public bool? q21 { get; set; }
        [Required]
        public bool? q22 { get; set; }
        [Required]
        public bool? q23 { get; set; }
        [Required]
        public bool? q24 { get; set; }
    }

    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class TestStepEleven_3
    {
        [Required]
        public bool? q1 { get; set; }
        [Required]
        public bool? q2 { get; set; }
        [Required]
        public bool? q3 { get; set; }
        [Required]
        public bool? q4 { get; set; }
        [Required]
        public bool? q5 { get; set; }
        [Required]
        public bool? q6 { get; set; }
        [Required]
        public bool? q7 { get; set; }
        [Required]
        public bool? q8 { get; set; }
        [Required]
        public bool? q9 { get; set; }
        [Required]
        public bool? q10 { get; set; }
        [Required]
        public bool? q11 { get; set; }
        [Required]
        public bool? q12 { get; set; }
        [Required]
        public bool? q13 { get; set; }
        [Required]
        public bool? q14 { get; set; }
        [Required]
        public bool? q15 { get; set; }
        [Required]
        public bool? q16 { get; set; }
        [Required]
        public bool? q17 { get; set; }
        [Required]
        public bool? q18 { get; set; }
        [Required]
        public bool? q19 { get; set; }
        [Required]
        public bool? q20 { get; set; }
        [Required]
        public bool? q21 { get; set; }
        [Required]
        public bool? q22 { get; set; }
        [Required]
        public bool? q23 { get; set; }
        [Required]
        public bool? q24 { get; set; }
    }
}