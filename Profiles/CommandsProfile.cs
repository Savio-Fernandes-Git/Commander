using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {   
            //Source -> target/destination
            CreateMap<Command, CommandReadDto>(); //get
            CreateMap<CommandCreateDto, Command>(); //post/create
            CreateMap<CommandUpdateDto, Command>(); //Put/update
            CreateMap<Command, CommandUpdateDto>(); //Patch/update
        }
    }
}