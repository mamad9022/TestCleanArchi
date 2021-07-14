using System;
using AutoMapper;
using TestCleanArch.Application.Common.AutoMapper;
using TestCleanArch.Application.Persons.Command.CreatePesron;
using TestCleanArch.Application.Persons.Command.UpdatePerson;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Persons.Dtos
{
    public class PersonDto : IMapFrom<Person>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool SendType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, PersonDto>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.SendType, opt => opt.MapFrom(s => s.SendType))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));


            profile.CreateMap<CreatePersonCommand, Person>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.SendType, opt => opt.MapFrom(s => s.SendType))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => Guid.NewGuid()));


            profile.CreateMap<UpdatePersonCommand, Person>()
              .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
              .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
              .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email));
        }
    }
}
