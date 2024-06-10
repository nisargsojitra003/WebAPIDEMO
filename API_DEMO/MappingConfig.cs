using API_DEMO_DAL.Models;
using API_DEMO_DAL.Models.ProductDTO;
using AutoMapper;
namespace API_DEMO
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, CreateProduct>().ReverseMap();
            
        }
    }
}
