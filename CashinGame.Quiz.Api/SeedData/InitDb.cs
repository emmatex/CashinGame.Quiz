using CashinGame.Quiz.Entity.Infrastructure;
using CashinGame.Quiz.Entity.Models;
using System;
using System.Collections.Generic;

namespace CashinGame.Quiz.Api.SeedData
{
    public static class InitDb
    {
        public static void EnsureSeedDataForContext(this ApplicationDbContext context)
        {
            // first, clear the database. This ensures we can always start fresh with each demo. 

            //context.Remove(context.Categories);
            context.Questions.RemoveRange(context.Questions);
            context.SaveChanges();

            //var category = new Category()
            //{
            //    Id = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bcc"),
            //    Name = "Programming",
            //    Description = "Question for c# developer",
            //    CreatedBy = "Ifeanyi",
            //    ModifiedBy = "Ifeanyi",
            //    CreatedOn = new DateTime(2018, 9, 16),
            //    ModifiedOn = new DateTime(2018, 9, 16),
            //};
            //context.Add(category);
            

            // init seed data
            var questions = new List<Question>()
            {
                new Question()
                {
                     Id = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                     CategoryId = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bcc"),
                     QuestionText = "Who wrote the first C# program?",
                     Level = "0ne",
                     CreatedBy = "Ifeanyi",
                     ModifiedBy = "Ifeanyi",
                     CreatedOn = new DateTime(2018, 9, 16),
                     ModifiedOn = new DateTime(2018, 9, 16),
                     Options = new List<Option>()
                     {
                         new Option()
                         {
                             Id = new Guid("c7ba6add-09c4-45f8-8dd0-eaca221e5d93"),
                             QuestionId = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                             Label = "A",
                             Value = "Anders Hejlsberg",
                             CreatedBy = "Ifeanyi",
                             ModifiedBy = "Ifeanyi",
                             CreatedOn = new DateTime(2018, 9, 16),
                             ModifiedOn = new DateTime(2018, 9, 16),
                             IsCorrect = true
                         },
                         new Option()
                         {
                             Id = new Guid("a3749477-f823-4124-aa4a-fc9ad5e79cd6"),
                             QuestionId = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                             Label = "B",
                             Value = "John Papa",
                             CreatedBy = "Ifeanyi",
                             ModifiedBy = "Ifeanyi",
                             CreatedOn = new DateTime(2018, 9, 16),
                             ModifiedOn = new DateTime(2018, 9, 16),
                             IsCorrect = false
                         },
                         new Option()
                         {
                             Id = new Guid("70a1f9b9-0a37-4c1a-99b1-c7709fc64167"),
                             QuestionId = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                             Label = "C",
                             Value = "Kevin Dockx",
                             CreatedBy = "Ifeanyi",
                             ModifiedBy = "Ifeanyi",
                             CreatedOn = new DateTime(2018, 9, 16),
                             ModifiedOn = new DateTime(2018, 9, 16),
                             IsCorrect = false
                         },
                         new Option()
                         {
                             Id = new Guid("60188a2b-2784-4fc4-8df8-8919ff838b0b"),
                             QuestionId = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                             Label = "D",
                             Value = "Mosh Hamedani",
                             CreatedBy = "Ifeanyi",
                             ModifiedBy = "Ifeanyi",
                             CreatedOn = new DateTime(2018, 9, 16),
                             ModifiedOn = new DateTime(2018, 9, 16),
                             IsCorrect = false
                         }
                     }
                }
            };

            context.Questions.AddRange(questions);
            context.SaveChanges();
        }
    }
}
