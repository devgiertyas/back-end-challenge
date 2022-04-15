using AutoMapper;
using TodoAPI.DataObjects;
using TodoAPI.Models;

namespace TodoAPI.AutoMapper
{
    public class AplicationProfile : Profile
    {

     public AplicationProfile()
        {
            CreateMap<Todo, TodoDTO>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<TodoDTO, Todo>();
        }

    }
}
