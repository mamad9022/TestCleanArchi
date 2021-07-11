using System;
using AutoMapper;
using TestCleanArch.Application.Common.AutoMapper;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Persons.Dtos
{
   public class PersonDto: IMapFrom<Person>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, PersonDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password));
        }
    }
}
