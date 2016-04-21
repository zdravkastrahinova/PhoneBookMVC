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
    public class ViewModelToModelMappingProfile : Profile
    {
        public ViewModelToModelMappingProfile() : base(nameof(ViewModelToModelMappingProfile)) { }

        protected override void Configure()
        {
            Mapper.CreateMap<ContactsEditVM, Contact>()
                .ForMember(c => c.Phones, opt => opt.Ignore())
                .ForMember(c => c.UserID, opt => opt.Ignore());

            Mapper.CreateMap<GroupsEditVM, Group>()
                .ForMember(g => g.UserID, opt => opt.Ignore());

            Mapper.CreateMap<PhonesEditVM, Phone>();

            Mapper.CreateMap<UsersEditVM, User>()
                .ForMember(u => u.RememberMeHash, opt => opt.Ignore())
                .ForMember(u => u.RememberMeExpiryDate, opt => opt.Ignore());

            Mapper.CreateMap<AccountRegisterVM, User>();
        }
    }
}