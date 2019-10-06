using AutoMapper;
using CashinGame.Quiz.Api.Models;
using CashinGame.Quiz.Entity.Interface;
using CashinGame.Quiz.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(IQuestionRepository repository, IMapper mapper, ILogger<QuestionsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetQuestions")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        {
            var questionsFromRepo = await _repository.GetAsync();
            return Ok(_mapper.Map<IEnumerable<QuestionDto>>(questionsFromRepo));
        }

        [HttpGet("{questionId}", Name = "GetQuestion")]
        public IActionResult GetQuestion(Guid questionId)
        {
            var questionFromRepo = _repository.GetById(questionId);
            if (questionFromRepo == null) return NotFound();

            return Ok(_mapper.Map<QuestionDto>(questionFromRepo));
        }

        [HttpPost(Name = "CreateQuestion")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult> CreateQuestion([FromBody] CreateQuestionDto question)
        {
            if (question == null) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            if (await _repository.isQuestionTextExist(question.QuestionText))
                return BadRequest("The question text inputed already exist");

            var questionToAdd = _mapper.Map<Question>(question);         
            _repository.Add(questionToAdd);

            if (!await _repository.SaveChangesAsync())
                throw new Exception("An error occured while trying to save question");

            return CreatedAtRoute("GetQuestion", new { questionId = questionToAdd.Id },
                _mapper.Map<QuestionDto>(questionToAdd));
        }

        [HttpPut("{questionId}", Name = "UpdateQuestion")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<QuestionDto>> UpdateQuestion(Guid questionId, UpdateQuestionDto question)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var questionFromRepo = _repository.GetById(questionId);
            if (questionFromRepo == null) return NotFound();

            _mapper.Map(question, questionFromRepo);
            _repository.Update(questionFromRepo);

            if (!await _repository.SaveChangesAsync())
                throw new Exception("An error occured while trying to updating question");

            return Ok(_mapper.Map<QuestionDto>(questionFromRepo));
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