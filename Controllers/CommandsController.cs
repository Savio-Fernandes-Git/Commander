using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
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
        [HttpGet("{id}")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandyById(id);
            if (commandItem == null)
                return NotFound();
            return Ok( _mapper.Map<CommandReadDto>(commandItem) );
        }
    }
}