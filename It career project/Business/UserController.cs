using It_career_project.Data;
using It_career_project.Data.Models;
using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace It_career_project.Business
{
    /// <summary>
    /// Business logic for the table Users
    /// </summary>
    public class UserController
    {
        private VideoGamePlatformContext context;

        /// <summary>
        /// Constructor used in Presentation layer
        /// </summary>
        public UserController()
        {
            context = new VideoGamePlatformContext();
        }

        /// <summary>
        /// Constructer used in tests
        /// </summary>
        public UserController(VideoGamePlatformContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Checks if a login is possible with a set of given details
        /// </summary>
        /// <param name="username">is used to check if a user exists with that username</param>
        /// <param name="password">is used to check if the user
        /// with that username matches the
        /// password after Hasing with method</param>
        /// <returns>True if the details are correct, False if the details are incorrect</returns>
        public void LoginAttempt(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be empty!");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be empty!");
            }

            string hashedPassword = Hashing(password);

            User user =
                context
                .Users
                .FirstOrDefault(x => x.Username == username);

            if (user == null || user.HashedPassword != hashedPassword)
            {
                throw new ArgumentException("Invalid username or password!");
            }

            return;
        }

        internal void DeleteUser(string usernameOrEmail)
        {
            User user =
                context
                .Users
                .FirstOrDefault(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail);

            context.Users.Remove(user);
            context.SaveChanges();
        }

        /// <summary>
        /// Gets a user with a given username from the table Users
        /// </summary>
        /// <param name="username">username used to find the user</param>
        /// <returns>User with the given username</returns>
        public User GetUserByUsername(string username)
        {
            User user =
                context
                .Users
                .FirstOrDefault(x => x.Username.Equals(username));

            if (user == null)
            {
                throw new ArgumentException("There is no user with this username!");
            }

            return user;
        }

        /// <summary>
        /// Gets a user with a given id from the table Users
        /// </summary>
        /// <param name="id">Id used to find the user</param>
        /// <returns>User with the given id</returns>
        public User GetUserById(int id)
        {
            User user =
                context
                .Users
                .FirstOrDefault(x => x.Id == id);

            return user;
        }

        /// <summary>
        /// Registers a user into the database given all their details
        /// </summary>
        /// <param name="countryName">countryName used to find the Country in Countries table</param>
        /// <returns>True if the registering is succesfull, 
        /// False if it fails, throws exception if password is empty or 
        /// country doesn't exist</returns>
        public void Register(string username, string password, string firstName, string lastName, string countryName, string email)
        {
            CountryController cController = new CountryController(context);

            bool isUsernameTaken = true;

            try
            {
                User usernameIsTaken = GetUserByUsername(username);
            }
            catch (ArgumentException)
            {
                isUsernameTaken = false;
            }

            if (isUsernameTaken)
            {
                throw new ArgumentException("This username is already registered!");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be empty!");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be empty!");
            }

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Name cannot be empty!");
            }

            if (string.IsNullOrEmpty(countryName))
            {
                throw new ArgumentException("Country cannot be empty!");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be empty!");
            }

            string hashedPassword = Hashing(password);
            Country country = cController.GetCountryByName(countryName);
            string countryCode = country.CountryCode;

            context.Users.Add(new User(username, hashedPassword, firstName, lastName, countryCode, email));
            context.SaveChanges();
        }

        /// <summary>
        /// Adds a sum to User's balance
        /// </summary>
        /// Adds a given sum in Euro to a user's balance by converting it to a user's currency
        /// <param name="valueInEuro">Is added to a user's balance after conversion</param>
        public void AddBalanceInUserCurrency(User user, decimal valueInEuro)
        {
            Country userCountry =
                context
                .Countries
                .FirstOrDefault(x => x.CountryCode == user.CountryCode);

            user.Balance += valueInEuro / userCountry.CurrencyExchangeRateToEuro;

            context.Update(user);
            context.SaveChanges();
        }

        /// <summary>
        /// Outputs a user's info by getting their username
        /// </summary>
        /// Gets a user with a given username from the table Users and returns their main details
        /// <param name="username">username used to find the user</param>
        /// <returns>The important details of a User</returns>
        public string GetUserInfoByUsername(string username)
        {
            CountryController cController = new CountryController(context);
            User wantedUser = GetUserByUsername(username);
            Country userCountry = cController.GetCountryByCountryCode(wantedUser.CountryCode);

            string UserInfo = $"{wantedUser.Username}: {wantedUser.FirstName} {wantedUser.LastName}, From: {userCountry.Name}";

            return UserInfo;
        }

        /// <summary>
        /// User tries to buy a game
        /// </summary>
        ///  Checks if a user's balance is enough to buy a game and then adds it to their game collection
        public void UserTriesToBuyGame(User user, VideoGame videoGame)
        {
            Country userCountry =
                context
                .Countries
                .FirstOrDefault(x => x.CountryCode == user.CountryCode);
            
            if (!videoGame.IsAvailable)
            {
                throw new ArgumentException("Game is not available for purchase!");
            }

            decimal userBalanceInEuro = user.Balance * userCountry.CurrencyExchangeRateToEuro;
            bool canBuy = userBalanceInEuro >= videoGame.Price;

            UserGameCollection userAlreadyOwnsGameChecker =
                context
                .UsersGameCollections
                .ToList()
                .FirstOrDefault(x => x.UserId == user.Id && x.GameId == videoGame.Id);

            if (userAlreadyOwnsGameChecker != null)
            {
                throw new ArgumentException("You already own this game!");
            }

            if (canBuy)
            {
                user.Balance -= videoGame.Price / userCountry.CurrencyExchangeRateToEuro;

                context
                    .UsersGameCollections
                    .Add(new UserGameCollection(user.Id, videoGame.Id));

                context.SaveChanges();
                context.Update(user);
                context.SaveChanges();
            }
            else
            {
                decimal moneyneeded = Math.Abs(user.Balance - videoGame.Price / userCountry.CurrencyExchangeRateToEuro);
                throw new ArgumentException($"Not enough balance! You need {moneyneeded:f2} {userCountry.Currency} more");
            }
        }

        public List<string> GetInfoOfAllUsers()
        {
            CountryController cController = new CountryController(context);
            List<User> listOfAllUsers = context.Users.ToList();
            List<string> infoOfAllUsers = new List<string>();

            foreach (User user in listOfAllUsers)
            {
                string userCountryName = cController.GetCountryByCountryCode(user.CountryCode).Name;
                infoOfAllUsers.Add($"{user.Username} - {user.FirstName} {user.LastName}, {userCountryName}, {user.Email}");
            }

            if (infoOfAllUsers.Count == 0)
            {
                throw new ArgumentException("There are no users!");
            }

            return infoOfAllUsers;
        }

        public string GetUserInfoByUsernameOrEmail(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Username cannot be empty!");
            }

            User user = GetUserByUsernameOrEmail(input);
            CountryController cController = new CountryController(context);

            string userCountry =
                cController
                .GetCountryByCountryCode(user.CountryCode)
                .Name;

            string userInfo = $"{user.Username} - {user.FirstName} {user.LastName}, {userCountry}, {user.Email}";

            return userInfo;
        }

        public User GetUserByUsernameOrEmail(string input)
        {
            User user = context.Users.FirstOrDefault(x => x.Username == input || x.Email == input);

            if (user == null)
            {
                throw new ArgumentException("No user found!");
            }

            return user;
        }

        /// <summary>
        /// Hashes passowrds using complicated algorithm
        /// </summary>
        /// <param name="password">password is hashed</param>
        /// <returns>A hashed password</returns>
        public string Hashing(string password)
        {
            using var hashing = SHA256.Create();
            byte[] bytes = hashing.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}