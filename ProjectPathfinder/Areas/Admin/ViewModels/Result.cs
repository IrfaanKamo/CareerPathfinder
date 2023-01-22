using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectPathfinder.Areas.Admin.ViewModels
{
    //-------------------------------------------------------------------------------------------
    [Serializable]
    public class ResultFeedback
    {
        public int memberId { get; set; }

        [Required]
        public string personality_01 { get; set; }
        [Required]
        public string personality_02 { get; set; }
        [Required]
        public string personality_03 { get; set; }
        [Required]
        public string personality_04 { get; set; }
        [Required]
        public string personality_05 { get; set; }
        [Required]
        public string personality_06 { get; set; }

        [Required]
        public string interest_01 { get; set; }
        [Required]
        public string interest_02 { get; set; }

        [Required]
        public string abilities_01 { get; set; }
        [Required]
        public string abilities_02 { get; set; }

        [Required]
        public string careerValues_01 { get; set; }
        [Required]
        public string careerValues_02 { get; set; }

        [Required]
        public string preferredSubject_01 { get; set; }
        [Required]
        public string preferredSubject_02 { get; set; }

        [Required]
        public string careerGroup_01 { get; set; }
        [Required]
        public string careerGroup_02 { get; set; }

        [Required]
        public string suggestedCareer_01 { get; set; }
        [Required]
        public string suggestedCareer_02 { get; set; }

        [Required]
        public string interestAbilitiesMatch_01 { get; set; }
        [Required]
        public string interestAbilitiesMatch_02 { get; set; }

        public string comments { get; set; }
    }
}