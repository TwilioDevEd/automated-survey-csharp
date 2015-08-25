using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatedSurvey.Web.Models
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public QuestionType Type { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual IList<Answer> Answers { get; set; } 
    }
}