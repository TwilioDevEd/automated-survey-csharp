using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatedSurvey.Web.Models
{
    public class Survey
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        public virtual IList<Question> Questions { get; set; }
    }
}