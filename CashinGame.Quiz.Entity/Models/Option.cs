using System;
using System.Collections.Generic;
using System.Text;

namespace CashinGame.Quiz.Entity.Models
{
    public class Option : BaseEntity
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}