using AutoMapper;
using KolmeoAPI.Data.Models;
using KolmeoAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KolmeoAPI.Data.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
