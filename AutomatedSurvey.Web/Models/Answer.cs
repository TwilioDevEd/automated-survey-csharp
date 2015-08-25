using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomatedSurvey.Web.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string RecordingUrl { get; set; }
        public string Digits { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Index]
        public string CallSid { get; set; }
        public string From { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}