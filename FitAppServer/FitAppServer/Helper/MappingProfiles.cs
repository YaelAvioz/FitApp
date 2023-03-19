using AutoMapper;
using FitAppServer.Model;
using FitAppServer.DTO;

namespace FitAppServer.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Food, FoodDTO>();
    }
}