using System;
using System.Collections.Generic;
using System.Text;

namespace CashinGame.Quiz.Entity.Models
{
    public class Question : BaseEntity
    {
        public string QuestionText { get; set; }
        public string Level { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Option> Options { get; set; }
        = new List<Option>();
    }
}

 