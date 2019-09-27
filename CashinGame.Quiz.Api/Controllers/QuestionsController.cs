﻿using AutoMapper;
using CashinGame.Quiz.Api.Models;
using CashinGame.Quiz.Entity.Interface;
using CashinGame.Quiz.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Api.Controllers
{
    [Route("api/questions")]
    [Produces("application/json", "application/xml")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _repository;
        private readonly IMapper _mapper;

        public QuestionsController(IQuestionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetQuestions")]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            var questionsFromRepo = await _repository.GetAsync();
            return Ok(_mapper.Map<IEnumerable<Question>>(questionsFromRepo));
        }

        [HttpGet("{questionId}", Name = "GetQuestion")]
        public IActionResult GetQuestion(Guid questionId)
        {
            var questionFromRepo = _repository.GetById(questionId);
            if (questionFromRepo == null) return NotFound();
            
            return Ok(_mapper.Map<Question>(questionFromRepo));
        }

        [HttpPost(Name = "CreateQuestion")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<Question>> CreateQuestion([FromBody] CreateQuestionDto question)
        {
            if (question == null) return BadRequest();

            if (await _repository.isQuestionTextExist(question.QuestionText))
                return BadRequest("The question text inputed already exist");

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var questionToAdd = _mapper.Map<Question>(question);
            _repository.Add(questionToAdd);

            if (!await _repository.SaveChangesAsync())
                throw new Exception("An error occured while trying to save question");

            return CreatedAtRoute("GetQuestion", new { categoryId = questionToAdd.Id },
                _mapper.Map<Category>(questionToAdd));
        }

        [HttpPut(Name = "UpdateQuestion")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<Category>> UpdateQuestion(QuestionDto question)
        {
            var questionFromRepo = _repository.GetById(question.Id);
            if (questionFromRepo == null) return NotFound();

            _mapper.Map(question, questionFromRepo);
            _repository.Update(questionFromRepo);
            await _repository.SaveChangesAsync();

            return Ok(_mapper.Map<Category>(questionFromRepo));
        }

        [HttpDelete("{questionId}", Name = "DeleteQuestion")]
        public async Task<ActionResult> DeleteQuestion(Guid questionId)
        {
            var questionFromRepo = _repository.GetById(questionId);
            if (questionFromRepo == null) return NotFound();

            _repository.Delete(questionFromRepo);

            if (!await _repository.SaveChangesAsync())
                throw new Exception($"Deleting question {questionId} failed to delete.");

            return NoContent();
        }

    }
}