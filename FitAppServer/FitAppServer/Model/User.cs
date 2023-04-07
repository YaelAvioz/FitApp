using AutoMapper;
using FitAppServer.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace FitAppServer.Model
{
    public class User : GenericEntity
    {
        public virtual string username { get; set; }
        public virtual byte[] passwordHash { get; set; }
        public virtual byte[] passwordSalt { get; set; }
        public virtual string chat
        {
            get
            {
                // Use AutoMapper to map User to UserDTO and get the UserProfile
                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
                var mapper = new Mapper(config);
                var userDto = mapper.Map<UserDTO>(this);

                return "the client is a " + userDto.gender + " at the age of " + userDto.age.ToString() +
                        ". The client's height is " + userDto.height.ToString() +
                        " and curren weight is " + userDto.weight[-1].Item1.ToString() + ". " +
                        "The client's goal is: " + userDto.goal + ". and the tags the client is" +
                        " intrested in are: " + userDto.tags.ToString() + ". Last thing you need to know is the food " +
                        "the client ate recently: " + userDto.foods.ToString() + ".";
            }
        }

    }
}