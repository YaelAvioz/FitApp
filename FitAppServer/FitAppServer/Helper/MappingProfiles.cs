using AutoMapper;
using FitAppServer.Model;
using FitAppServer.DTO;

namespace FitAppServer.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Food, FoodDTO>();
        CreateMap<User, UserDTO>();
        CreateMap<Mentor, MentorDTO>();
        CreateMap<Recipe, RecipeDTO>();
        CreateMap<Recipe, RecipeCardDTO>();
        CreateMap<Conversation, ConversationDTO>();
        CreateMap<Message, MessageDTO>();

    }
}