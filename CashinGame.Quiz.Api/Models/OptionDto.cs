using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Api.Models
{
    public class OptionDto
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
    }
}
