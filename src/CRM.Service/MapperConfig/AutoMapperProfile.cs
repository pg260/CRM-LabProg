using AutoMapper;
using CRM.Domain.Entities;
using CRM.Domain.Pagination;
using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.Dtos.UserDtos;

namespace CRM.Service.MapperConfig;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region User

        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<User, UpdateUserDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<PaginatedResult<User>, PagedDto<UserDto>>().ReverseMap();

        #endregion
    }
}