using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Commander.Controllers
{
    [ApiController] //not mandorty to this
    [Route("api/commands")] //can write [Route("api/[controller]")] to yield the same result
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        //private readonly MockCommanderRepo _repository = new MockCommanderRepo();

        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok( _mapper.Map<IEnumerable<CommandReadDto>>(commandItems) );
        }

        //GET api/commands/{id}
        [HttpGet( "{id}" , Name = "GetCommandById" )]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandyById(id);
            if (commandItem == null)
                return NotFound();
            return Ok( _mapper.Map<CommandReadDto>(commandItem) );
        }

        //POST api/commands/
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand( CommandCreateDto commandCreateDto )
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            //return Ok(commandReadDto);
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto ); //201
        }

        // PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandyById(id);
            if( commandModelFromRepo == null ) return NotFound();
            
            _mapper.Map(commandUpdateDto ,commandModelFromRepo);
            
            _repository.UpdateCommand(commandModelFromRepo); //this isn't required to do as per entity framework but we do because best practices
            _repository.SaveChanges();

            return NoContent(); //204
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandyById(id);
            if( commandModelFromRepo == null ) return NotFound();

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if( !TryValidateModel(commandToPatch) ) return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch ,commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo); //this isn't required to do as per entity framework but we do because best practices

            _repository.SaveChanges();

            return NoContent(); //204
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandyById(id);
            if( commandModelFromRepo == null ) return NotFound();

            _repository.DeleteCommand(commandModelFromRepo);
            
            _repository.SaveChanges();

            return NoContent(); //204
        }
    }
}