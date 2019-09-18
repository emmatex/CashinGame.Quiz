using CashinGame.Quiz.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashinGame.Quiz.Api.Models
{
    public class CreateCategoryDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "You should fill out a Name.")]
        [MaxLength(200, ErrorMessage = "Name shouldn't have more than 200 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You should fill out a Description.")]
        [MaxLength(300, ErrorMessage = "Description shouldn't have more than 300 characters")]
        public string Description { get; set; }

        public ICollection<Question> Questions { get; set; }
          = new List<Question>();

    }
}
