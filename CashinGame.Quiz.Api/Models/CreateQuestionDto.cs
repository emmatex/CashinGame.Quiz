using CashinGame.Quiz.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Api.Models
{
    public class CreateQuestionDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "You should fill out a QuestionText.")]
        [MaxLength(300, ErrorMessage = "QuestionText shouldn't have more than 300 characters")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "You should fill out a Level.")]
        [MaxLength(4, ErrorMessage = "Level shouldn't have more than 4 characters")]
        public string Level { get; set; }

        public ICollection<Option> Options { get; set; }
          = new List<Option>();

       

    }
}
