using AutoMapper;
using CashinGame.Quiz.Api.Models;
using CashinGame.Quiz.Entity.Models;

namespace CashinGame.Quiz.Api.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>();
            CreateMap<CreateQuestionDto, Question>();
            CreateMap<UpdateQuestionDto, Question>();
            CreateMap<Question, UpdateQuestionDto>();
        }
    }
}
