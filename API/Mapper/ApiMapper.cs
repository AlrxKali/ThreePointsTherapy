using API.DTOs;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class ApiMapper : Profile
    {
        public ApiMapper() 
        {
            CreateMap<Word, WordDTO>().ReverseMap();
            CreateMap<Word, AddWordDTO>().ReverseMap();
            CreateMap<UserDTO, UserDTO>().ReverseMap();
        }
    }
}
