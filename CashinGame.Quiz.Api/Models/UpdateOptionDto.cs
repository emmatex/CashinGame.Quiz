using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Api.Models
{
    public class UpdateOptionDto
    {
        [Required(ErrorMessage = "You should fill out Label.")]
        [MaxLength(100, ErrorMessage = "Label shouldn't have more than 100 characters")]
        public string Label { get; set; }

        [Required(ErrorMessage = "You should fill out Value.")]
        [MaxLength(200, ErrorMessage = "Value shouldn't have more than 200 characters")]
        public string Value { get; set; }

        [Required(ErrorMessage = "Is correct is required.")]
        public bool IsCorrect { get; set; }
    }
}
