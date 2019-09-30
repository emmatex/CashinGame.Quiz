using AutoMapper;
using CashinGame.Quiz.Api.Models;
using CashinGame.Quiz.Entity.Interface;
using CashinGame.Quiz.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Api.Controllers
{
    [Route("api/questions/{questionId}/options")]
    [Produces("application/json", "application/xml")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionRepository _optionRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OptionsController> _logger;

        public OptionsController(IOptionRepository optionRepository, IQuestionRepository questionRepository, 
            IMapper mapper, ILogger<OptionsController> logger)
        {
            _optionRepository = optionRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet(Name = "GetOptions")]
        public async Task<ActionResult<IEnumerable<Option>>> GetOptions(Guid questionId)
        {
            if (!await _questionRepository.isExists(questionId))
                return NotFound();

            var optionsFromRepo = await _optionRepository.GetOptionAsync(questionId);
            return Ok(_mapper.Map<IEnumerable<Option>>(optionsFromRepo));
        }


        [HttpGet("{optionId}", Name = "GetOption")]
        public async Task<ActionResult<Option>> GetOption(Guid questionId, Guid optionId)
        {
            if (!await _questionRepository.isExists(questionId))
                return NotFound();

            var optionFromRepo = await _optionRepository.GetOptionAsync(questionId, optionId);
            if (optionFromRepo == null) return NotFound();

            return Ok(_mapper.Map<Option>(optionFromRepo));
        }

        [HttpPost(Name = "CreateOption")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<Option>> CreateOption(Guid questionId, [FromBody]CreateOptionDto option)
        {
            if (!await _questionRepository.isExists(questionId))
                return NotFound();

            if (option == null) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            if (await _optionRepository.isExists(questionId, option.Value))
                return BadRequest("The value inputed already exist for that question");

            var optionToAdd = _mapper.Map<Option>(option);
            _optionRepository.Add(optionToAdd);

            if (!await _optionRepository.SaveChangesAsync())
                throw new Exception("An error occured while trying to save option");

            return CreatedAtRoute("GetOption", new { questionId, optionId = optionToAdd.Id },
                _mapper.Map<Option>(optionToAdd));
        }

        [HttpPut("{optionId}", Name = "UpdateOption")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<Option>> UpdateOption(Guid questionId, Guid optionId, [FromBody]UpdateOptionDto option)
        {
            if (option == null) return BadRequest();

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            if (!await _questionRepository.isExists(questionId))
                return NotFound();

            var optionFromRepo = await _optionRepository.GetOptionAsync(questionId, optionId);
            if (optionFromRepo == null)
            {
                var optionToAdd = Mapper.Map<Option>(option);
                optionToAdd.Id = optionId;
                _questionRepository.AddOptionForQuestion(questionId, optionToAdd);

                if (!await _optionRepository.SaveChangesAsync())
                    throw new Exception($"Upserting option {optionId} for question {questionId} failed on save.");

                var optionToReturn = Mapper.Map<OptionDto>(optionToAdd);
                return CreatedAtRoute("GetOption", new { id = optionToReturn.Id }, optionToReturn);
            }

            Mapper.Map(option, optionFromRepo);
            _optionRepository.Update(optionFromRepo);

            if (!await _optionRepository.SaveChangesAsync())
                throw new Exception($"Upserting option {optionId} for question {questionId} failed on update.");

            return NoContent();
        }

        [HttpDelete("{optionId}", Name = "DeleteOption")]
        public async Task<ActionResult> DeleteOption(Guid questionId, Guid optionId)
        {
            if (!await _questionRepository.isExists(questionId))
                return NotFound();

            var optionFromRepo = await _optionRepository.GetOptionAsync(questionId, optionId);
            if (optionFromRepo == null) return NotFound();

            _optionRepository.Delete(optionFromRepo);

            if (!await _optionRepository.SaveChangesAsync())
                throw new Exception($"Deleting a option {optionId} for question {questionId} failed to delete.");

            return NoContent();
        }


    }
}