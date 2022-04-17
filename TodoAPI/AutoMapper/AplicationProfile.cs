using AutoMapper;
using TodoAPI.DataObjects;
using TodoAPI.DataTransferObjects.Todo;
using TodoAPI.DataTransferObjects.User;
using TodoAPI.Models;

namespace TodoAPI.AutoMapper
{
    public class AplicationProfile : Profile
    {

     public AplicationProfile()
        {

            CreateMap<TodoAddDTO, Todo>();
            CreateMap<Todo, TodoGetDTO>();

            CreateMap<User, UserGetDTO>();

            CreateMap<UserAddTDO, User>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserPutDTO, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<User, UserPutDTO>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
