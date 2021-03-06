﻿using System;

namespace CashinGame.Quiz.Api.Models
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string Color { get; set; }
    }
}
