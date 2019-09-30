using System.ComponentModel.DataAnnotations;

namespace CashinGame.Quiz.Api.Models
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Name input is required.")]
        [MaxLength(200, ErrorMessage = "Name shouldn't have more than 200 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required..")]
        [MaxLength(300, ErrorMessage = "Description shouldn't have more than 300 characters")]
        public string Description { get; set; }

        public string Avatar { get; set; }

        [Required(ErrorMessage = "Color input is required.")]
        [MaxLength(20, ErrorMessage = "Color shouldn't have more than 20 characters")]
        public string Color { get; set; }
    }
}
