using AutoMapper;
using PhoneBookMVC.Models;
using PhoneBookMVC.ViewModels.AccountVM;
using PhoneBookMVC.ViewModels.ContactsVM;
using PhoneBookMVC.ViewModels.GroupsVM;
using PhoneBookMVC.ViewModels.PhonesVM;
using PhoneBookMVC.ViewModels.UsersVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Mappings
{
    public class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile() : base(nameof(ModelToViewModelMappingProfile)) { }

        protected override void Configure()
        {
            Mapper.CreateMap<Contact, ContactsEditVM>()
                .ForMember(m => m.Countries, opt => opt.Ignore())
                .ForMember(m => m.Cities, opt => opt.Ignore())
                .ForMember(m => m.Groups, opt => opt.Ignore())
                .ForMember(m => m.CountryID, opt => opt.Ignore());

            Mapper.CreateMap<Group, GroupsEditVM>();

            Mapper.CreateMap<Phone, PhonesEditVM>();

            Mapper.CreateMap<User, UsersEditVM>();
        }
    }
}