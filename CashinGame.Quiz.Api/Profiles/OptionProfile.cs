using AutoMapper;
using CashinGame.Quiz.Api.Models;
using CashinGame.Quiz.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Api.Profiles
{
    public class OptionProfile : Profile
    {
        public OptionProfile()
        {
            CreateMap<Option, OptionDto>();
            CreateMap<CreateOptionDto, Option>();
            CreateMap<UpdateOptionDto, Option>();
        }
    }
}
