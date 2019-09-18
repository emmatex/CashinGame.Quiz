using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Api.Models
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public string Level { get; set; }
    }
}
