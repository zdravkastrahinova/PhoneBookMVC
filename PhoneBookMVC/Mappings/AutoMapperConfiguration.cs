using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PhoneBookMVC.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(map =>
            {
                map.AddProfile<ModelToViewModelMappingProfile>();
                map.AddProfile<ViewModelToModelMappingProfile>();
            });
        }
    }
}