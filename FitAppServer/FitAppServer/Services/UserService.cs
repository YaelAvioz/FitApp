using AutoMapper;
using FitAppServer.DTO;
using FitAppServer.Helper;
using FitAppServer.Model;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FitAppServer.Services
{
    public class UserService
    {
        protected readonly IMongoCollection<User> _collection;
        public readonly ConversationService _conversationService;
        public readonly MessageService _messageService;
        public readonly MentorService _mentorService;
        protected readonly FoodService _foodService;
        protected readonly RecipeService _recipeService;
        protected readonly IMongoDatabase _db;
        protected readonly IMapper _mapper;
        protected readonly string connectionString = "mongodb+srv://FitApp:FitAppYaelCoral@cluster0.hsylfut.mongodb.net/?retryWrites=true&w=majority";
        protected readonly string databaseName = "fitapp";
        protected readonly string collectionName = "user";
        readonly string fakeId = "aaaaaaaaaaaaaaaaaaaaaaaa";

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(databaseName);
            _collection = _db.GetCollection<User>(collectionName);
            _conversationService = new ConversationService(_mapper);
            _messageService = new MessageService(_mapper);
            _mentorService = new MentorService(_mapper);
            _foodService = new FoodService(_mapper);
            _recipeService = new RecipeService(_mapper);
        }

        public UserService(IMapper mapper, IMongoCollection<User> collection)
        {
            _mapper = mapper;
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(databaseName);
            _collection = collection;
            _conversationService = new ConversationService(_mapper);
            _messageService = new MessageService(_mapper);
            _mentorService = new MentorService(_mapper);
            _foodService = new FoodService(_mapper);
            _recipeService = new RecipeService(mapper);
        }

        public async Task<User> RegisterUser(RegisterDTO userDTO)
        {
            var isUserExist = await UserExists(userDTO.username);
            if (isUserExist != null) return null;

            using var hmac = new HMACSHA512();
            var newUser = new User
            {
                username = userDTO.username.ToLower(),
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.password)),
                passwordSalt = hmac.Key,

                age = userDTO.age,
                firstname = userDTO.firstname,
                lastname = userDTO.lastname,
                gender = userDTO.gender,
                height = userDTO.height,
                foods = new List<Tuple<string, double, DateTime>>(),
                weight = new List<Tuple<double, DateTime>>(),
                tags = userDTO.tags,
                goal = userDTO.goal,
                water = new List<bool>(),
                mentor = userDTO.mentor,
            };

            // Update weight, bmi and recommended water
            newUser.weight.Add(Tuple.Create(userDTO.weight, DateTime.Now));
            newUser.bmi = newUser.getBmi(userDTO.weight);
            newUser.water = Enumerable.Repeat(false, newUser.GetWaterRecommendation()).ToList();

            // add the user to the db
            await _collection.InsertOneAsync(newUser);

            // get the user from the db to get its Id
            var user = await UserExists(userDTO.username);

            // choose mentor
            var newUserTags = new Dictionary<string, List<string>>
            {
                { newUser.Id, newUser.tags }
            };
            var mentors = await _mentorService.GetMappingInfo();
            MapClientMentor map = new MapClientMentor(newUserTags, mentors);
            newUser.mentor = map.AssignMentor(newUser.Id);

            // update the user in the db (mentor added)
            await _collection.UpdateOneAsync(Builders<User>.Filter.Eq(u => u.Id, newUser.Id),
                Builders<User>.Update.Set(u => u.mentor, newUser.mentor));


            // when registering a new user: create a conversation + send him the first msg.
            Conversation conversation = new Conversation
            {
                Messages = new List<string>(),
                UserId = user.Id
            };

            // create the first message and add it to the db
            Message message = new Message
            {
                Content = newUser.FirstMsg(),
                IsFromUser = false,
                Timestamp = DateTime.Now,
                ConversationId = fakeId
            };
            var msg = await _messageService.Create(message);

            // add the message to the conversation
            conversation.Messages.Add(msg.Id);

            // new conversation for the new user + update the Id
            var conv = await _conversationService.Create(conversation);
            msg.ConversationId = conv.Id;
            await _messageService.UpdateId(msg);

            return newUser;
        }

        public async Task<User> Login(LoginDTO userDTO)
        {
            var userExists = await UserExists(userDTO.username);
            if (userExists == null) return null;

            using var hmac = new HMACSHA512(userExists.passwordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.password));
            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != userExists.passwordHash[i]) throw new Exception("Unauthorized newUser!");
            }
            return userExists;
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _collection.Find(x => x.username.Equals(username)).FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserDTO> UpdateToken(UserDTO userDTO)
        {
            // Create a filter to find the user in the database
            var filter = Builders<User>.Filter.Eq(u => u.username, userDTO.username);

            // Create an update definition to set the user's token to the new value
            var update = Builders<User>.Update.Set(u => u.token, userDTO.token);

            // Find the user and update their token in the database
            var result = await _collection.FindOneAndUpdateAsync(
                filter,
                update,
                new FindOneAndUpdateOptions<User, User> { ReturnDocument = ReturnDocument.After }
            );

            // Convert the updated user entity to a UserDTO model using AutoMapper
            var updatedUserDTO = _mapper.Map<UserDTO>(result);

            return updatedUserDTO;
        }

        public async Task<List<Tuple<double, DateTime>>> WeightCharts(string id)
        {
            User user = await GetUserById(id);
            return user.weight;
        }

        public async Task<List<bool>> GetWater(string id)
        {
            User user = await GetUserById(id);
            return user.water;
        }

        public async Task<List<bool>> UpdateWater(string id, int cupsToAdd)
        {
            User user = await GetUserById(id);
            user.AddWater(cupsToAdd);

            // update the user in the db (water changed)
            await _collection.UpdateOneAsync(Builders<User>.Filter.Eq(u => u.Id, user.Id),
            Builders<User>.Update.Set(u => u.water, user.water));
            return user.water;
        }

        public async Task<UserDTO> UpdateWeight(string id, double newWeight)
        {
            User user = await GetUserById(id);
            if (user != null)
            {
                user.weight.Add(new Tuple<double, DateTime>(newWeight, DateTime.Now));

                // Update the user in the db
                await _collection.UpdateOneAsync(
                    Builders<User>.Filter.Eq(u => u.Id, user.Id),
                    Builders<User>.Update.Set(u => u.weight, user.weight));

                // Create a UserDTO from the updated user
                UserDTO userDto = new UserDTO
                {
                    id = user.Id,
                    username = user.username,
                    token = user.token,
                    firstname = user.firstname,
                    lastname = user.lastname,
                    age = user.age,
                    height = user.height,
                    weight = user.weight,
                    gender = user.gender,
                    bmi = user.bmi,
                    goal = user.goal,
                    mentor = user.mentor,
                    tags = user.tags,
                    water = user.water,
                    foods = user.foods,
                };

                return userDto;
            }
            return null;
        }

        public async Task<Food> AddFood(string username, string foodId, double amount)
        {
            User user = await GetUserByUsername(username);
            if (user != null)
            {
                FoodDTO foodDto = await _foodService.GetFoodInfoByAmount(foodId, amount);
                if (foodDto != null)
                {
                    Food food = _mapper.Map<Food>(foodDto);
                    Tuple<string, double, DateTime> foodToAdd = Tuple.Create(foodId, amount, DateTime.Now);
                    user.foods.Add(foodToAdd);

                    // update the user in the db
                    await _collection.UpdateOneAsync(Builders<User>.Filter.Eq(u => u.username, user.username),
                    Builders<User>.Update.Set(u => u.foods, user.foods));

                    return food;
                }
            }
            return null;
        }

        public async Task<Food> AddFoods(string id, string foodId, double amount)
        {
            User user = await GetUserById(id);
            if (user != null)
            {
                FoodDTO foodDto = await _foodService.GetFoodInfoByAmount(foodId, amount);
                if (foodDto != null)
                {
                    Food food = _mapper.Map<Food>(foodDto);
                    Tuple<string, double, DateTime> foodToAdd = Tuple.Create(foodId, amount, DateTime.Now);
                    user.foods.Add(foodToAdd);

                    // update the user in the db
                    await _collection.UpdateOneAsync(Builders<User>.Filter.Eq(u => u.Id, user.Id),
                    Builders<User>.Update.Set(u => u.foods, user.foods));

                    return food;
                }
            }
            return null;
        }

        public async Task<List<FoodDTO>> AddRecipe(string id, string recipeId)
        {
            User user = await GetUserById(id);
            if (user != null)
            {
                List<Tuple<string, double>> ingredients = await _recipeService.GetIngredients(recipeId);
                List<FoodDTO> foods = await _foodService.GetFoods(ingredients);

                foreach (FoodDTO food in foods)
                {
                    await AddFood(id, food.Id, 50.0);
                }
                return foods;
            }
            return null;
        }

        public async Task<FoodDTO> GetTodaysFoodData(string username)
        {
            User user = await GetUserByUsername(username);
            if ((user != null) && (user.foods != null))
            {
                FoodDTO res = new FoodDTO();

                double calories = 0;
                double total_fat = 0;
                double calcium = 0;
                double protein = 0;
                double carbohydrate = 0;
                double fiber = 0;
                double sugars = 0;
                double fat = 0;


                foreach (Tuple<string, double, DateTime> unit in user.foods)
                {
                    DateTime date = unit.Item3;
                    DateTime now = DateTime.Now;

                    if (date >= now.AddHours(-24) && date <= now)
                    {
                        FoodDTO food = await _foodService.GetFoodInfoByAmount(unit.Item1, unit.Item2);

                        calories += double.Parse(CleanString(food.calories));
                        total_fat += double.Parse(CleanString(food.total_fat));
                        calcium += double.Parse(CleanString(food.calcium));
                        protein += double.Parse(CleanString(food.protein));
                        carbohydrate += double.Parse(CleanString(food.carbohydrate));
                        fiber += double.Parse(CleanString(food.fiber));
                        sugars += double.Parse(CleanString(food.sugars));
                        fat += double.Parse(CleanString(food.fat));
                    }
                }

                res.calories = calories.ToString();
                res.total_fat = total_fat.ToString();
                res.protein = protein.ToString();
                res.sugars = sugars.ToString();
                res.calcium = calcium.ToString();
                res.fat = fat.ToString();
                res.fiber = fiber.ToString();
                res.carbohydrate = carbohydrate.ToString();

                return res;

            }
            return null;
        }

        public async Task<FoodDTO> GetTodaysFoodData(User user)
        {
            if ((user != null) && (user.foods != null))
            {
                FoodDTO res = new FoodDTO();

                double calories = 0;
                double total_fat = 0;
                double calcium = 0;
                double protein = 0;
                double carbohydrate = 0;
                double fiber = 0;
                double sugars = 0;
                double fat = 0;


                foreach (Tuple<string, double, DateTime> unit in user.foods)
                {
                    DateTime date = unit.Item3;
                    DateTime now = DateTime.Now;

                    if (date >= now.AddHours(-24) && date <= now)
                    {
                        FoodDTO food = await _foodService.GetFoodInfoByAmount(unit.Item1, unit.Item2);

                        calories += double.Parse(CleanString(food.calories));
                        total_fat += double.Parse(CleanString(food.total_fat));
                        calcium += double.Parse(CleanString(food.calcium));
                        protein += double.Parse(CleanString(food.protein));
                        carbohydrate += double.Parse(CleanString(food.carbohydrate));
                        fiber += double.Parse(CleanString(food.fiber));
                        sugars += double.Parse(CleanString(food.sugars));
                        fat += double.Parse(CleanString(food.fat));
                    }
                }

                res.calories = calories.ToString();
                res.total_fat = total_fat.ToString();
                res.protein = protein.ToString();
                res.sugars = sugars.ToString();
                res.calcium = calcium.ToString();
                res.fat = fat.ToString();
                res.fiber = fiber.ToString();
                res.carbohydrate = carbohydrate.ToString();

                return res;

            }
            return null;
        }

        public async Task<GradeDTO> GetGrade(string username)
        {
            User user = await GetUserByUsername(username);
            if (user != null)
            {
                GradeDTO expected = GradeCalculator.CalculateDailyExpectedNutrition(user.gender, user.age, user.weight[user.weight.Count - 1].Item1, user.height);
                FoodDTO currentStrings = await GetTodaysFoodData(user);
                DoubleFoodDTO currentDouble = GetDoubleFromFoodDTO(currentStrings);

                if ((expected != null) && (currentDouble != null))
                {
                    return GradeCalculator.AnalyzeData(currentDouble, expected);
                }
            }
            return null;
        }

        public async Task<List<Tuple<FoodDTO, DateTime>>> GetFoodData(string username)
        {
            List<Tuple<FoodDTO, DateTime>> foodData = new List<Tuple<FoodDTO, DateTime>>();

            User user = await GetUserByUsername(username);
            if ((user != null) && (user.foods != null))
            {
                foreach (Tuple<string, double, DateTime> tuple in user.foods)
                {
                    Tuple<FoodDTO, DateTime> singleFood = new Tuple<FoodDTO, DateTime>
                        (await _foodService.GetFoodInfoByAmount(tuple.Item1, tuple.Item2), tuple.Item3);
                    foodData.Add(singleFood);
                }
            }
            
            return foodData;
        }


        private async Task<User> UserExists(string username)
        {
            var user = await _collection.Find(x => x.username.ToLower().Equals(username.ToLower())).FirstOrDefaultAsync();
            return user;
        }

        private GradeDTO ParseAnswer(string answer)
        {
            GradeDTO gradeDTO = new GradeDTO();

            answer = answer.Replace("\n", "");
            string[] values = answer.Split(", ");

            if (values.Length != 7) return null;

            gradeDTO.calories_diff = int.Parse(values[0]);
            gradeDTO.total_fat_diff = double.Parse(values[1]);
            gradeDTO.calcium_diff = double.Parse(values[2]);
            gradeDTO.protein_diff = double.Parse(values[3]);
            gradeDTO.carbohydrate_diff = double.Parse(values[4]);
            gradeDTO.fiber_diff = double.Parse(values[5]);
            gradeDTO.sugars_diff = double.Parse(values[6]);
            gradeDTO.fat_diff = double.Parse(values[7]);

            return gradeDTO;
        }

        private string CleanString(string input)
        {
            return Regex.Match(input, @"[\d.]+").Value;
        }

        private DoubleFoodDTO GetDoubleFromFoodDTO(FoodDTO foodDTO)
        {
            DoubleFoodDTO res = new DoubleFoodDTO();

            res.calories = double.Parse(CleanString(foodDTO.calories));
            res.total_fat = double.Parse(CleanString(foodDTO.total_fat));
            res.calcium = double.Parse(CleanString(foodDTO.calcium));
            res.protein = double.Parse(CleanString(foodDTO.protein));
            res.carbohydrate = double.Parse(CleanString(foodDTO.carbohydrate));
            res.fiber = double.Parse(CleanString(foodDTO.fiber));
            res.sugars = double.Parse(CleanString(foodDTO.sugars));
            res.fat = double.Parse(CleanString(foodDTO.fat));

            return res;
        }
    }
}

