using AutoMapper;
using FitAppServer.Model;
using FitAppServer.DTO;
using Microsoft.AspNetCore.Identity;

namespace FitAppServer.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Food, FoodDTO>();
        CreateMap<User, UserDTO>();
        CreateMap<UserLoginInfo, UserDTO>();
        CreateMap<RegisterDTO, UserDTO>();
        CreateMap<Mentor, MentorDTO>();
        CreateMap<Recipe, RecipeDTO>();
        CreateMap<Recipe, RecipeCardDTO>();
        CreateMap<Conversation, ConversationDTO>();
        CreateMap<Message, MessageDTO>();

    }
}