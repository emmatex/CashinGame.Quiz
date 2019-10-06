using System;
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

        [Required(ErrorMessage = "Category Id is required.")]
        public Guid CategoryId { get; set; }

        public ICollection<CreateOptionDto> Options { get; set; }
          = new List<CreateOptionDto>();

    }
}
