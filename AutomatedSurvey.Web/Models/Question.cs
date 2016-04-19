using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

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
        [JsonIgnore]
        public virtual Survey Survey { get; set; }
        [JsonIgnore]
        public virtual IList<Answer> Answers { get; set; } 
    }
}