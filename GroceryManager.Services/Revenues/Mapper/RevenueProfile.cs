using AutoMapper;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.Revenue;

namespace GroceryManager.Services.Revenues.Mapper
{
    public class RevenueProfile : Profile
    {
        public RevenueProfile()
        {
            CreateMap<ApiRevenueItem, GetRevenuesDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Receita))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredientes))
                .ForMember(dest => dest.PreparationMode, opt => opt.MapFrom(src => src.Modo_Preparo))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Link_Imagem))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.IngredientNames, opt => opt.MapFrom(
                    src => src.IngredientesBase.SelectMany(x => x.NomesIngrediente).Distinct().ToList()
                ));
            CreateMap<Revenue, GetRevenuesDto>();
            CreateMap<GetRevenuesDto, Revenue>();
        }

        public static GetRevenuesDto ToDto(ApiRevenueItem apiItem)
        {
            return new GetRevenuesDto
            {
                Id = apiItem.Id,
                ApiId = apiItem.Id,
                Name = apiItem.Receita,
                Ingredients = apiItem.Ingredientes,
                PreparationMode = apiItem.Modo_Preparo,
                ImageUrl = apiItem.Link_Imagem,
                Type = apiItem.Tipo,
                IngredientNames = apiItem.IngredientesBase
                    .SelectMany(x => x.NomesIngrediente)
                    .Distinct()
                    .ToList()
            };
        }
    }
}
