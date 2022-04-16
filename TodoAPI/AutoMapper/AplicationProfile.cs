using AutoMapper;
using TodoAPI.DataObjects;
using TodoAPI.Models;

namespace TodoAPI.AutoMapper
{
    public class AplicationProfile : Profile
    {

     public AplicationProfile()
        {
            CreateMap<Todo, TodoDTO>();

            CreateMap<TodoDTO, Todo>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<User, UserDTO>().ForMember(x => x.Password, opt => opt.Ignore());

            CreateMap<UserDTO, User>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }

    }
}
