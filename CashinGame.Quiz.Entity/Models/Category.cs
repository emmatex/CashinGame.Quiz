using System;
using System.Collections.Generic;
using System.Text;

namespace CashinGame.Quiz.Entity.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string Color { get; set; }
        public ICollection<Question> Questions { get; set; }
          = new List<Question>();
    }
}

 