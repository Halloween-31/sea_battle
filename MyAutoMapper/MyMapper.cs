﻿using asp_MVC_letsTry.Models;
using asp_MVC_letsTry.Models.registrationForms;
using AutoMapper;

namespace asp_MVC_letsTry.MyAutoMapper
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<user, signUpForm>().ReverseMap();
            //CreateMap<List<user>, List<signUpForm>>().ReverseMap();
        }
    }
}
