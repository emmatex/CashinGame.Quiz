﻿using CashinGame.Quiz.Entity.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashinGame.Quiz.Api.Models
{
    public class CreateQuestionDto
    {
        [Required(ErrorMessage = "Question text is required.")]
        [MaxLength(300, ErrorMessage = "QuestionText shouldn't have more than 300 characters")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Level is required.")]
        [MaxLength(4, ErrorMessage = "Level shouldn't have more than 4 characters")]
        public string Level { get; set; }

        public ICollection<Option> Options { get; set; }
          = new List<Option>();

    }
}
