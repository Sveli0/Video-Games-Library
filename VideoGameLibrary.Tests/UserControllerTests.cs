using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using It_career_project.Data.Models;
using It_career_project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using It_career_project.Business;

namespace VideoGameLibrary.Tests
{
    class UserControllerTests
    {
        [Test]

        public void LoginAttemptShouldNotLogin(
        [Values("", "InvalidUserName")] string username,
        [Values("", "InvalidPassword")] string password)
        {
            var data = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();

            mockSet.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<User>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<User>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet.Object);
            var service = new UserController(mockContext.Object);

            Assert.Throws<ArgumentException>(() => service.LoginAttempt(username, password));
        }
        [Test]
        public void LoginAttemptShouldLogin(
        [Values("TestUserName")] string username,
        [Values("TestPassword")] string password)
        {
            var data = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();

            mockSet.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<User>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<User>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet.Object);

            var service = new UserController(mockContext.Object);

            data.ToList().ForEach(u => u.HashedPassword = service.Hashing(password));

            Assert.DoesNotThrow(() => service.LoginAttempt(username, password));


        }

        [Test]
        public void GetUserByIdShouldGetUserById(
            [Values(1)] int Id,
            [Values("TestUserName")] string username)
        {
            var data = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();

            mockSet.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<User>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<User>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet.Object);

            var service = new UserController(mockContext.Object);
            var user = service.GetUserById(Id);
            Assert.That(user.Username == username);
        }

        [Test]
        public void RegisterShouldNotRegisterOrRegister(
        [Values("", "TestRegisteredUsername", "TestUsername")] string username,
        [Values("", "TestPassword")] string password,
        [Values("", "TestFirstName")] string firstName,
        [Values("", "TestLastName")] string lastName,
        [Values("", "TestCountry")] string countryName,
        [Values("", "TestEmail")] string email)
        {
            var data2 = new List<User>
            {
                new User("TestRegisteredUsername", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var data1 = new List<Country>
            {
                new Country("ttt","TestCountry","TCC",1) { CountryCode="ttt"}
            }.AsQueryable();

            var mockSet1 = new Mock<DbSet<Country>>();
            var mockSet2 = new Mock<DbSet<User>>();

            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(data2.Provider);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Expression)
                .Returns(data2.Expression);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.ElementType)
                .Returns(data2.ElementType);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data2.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Countries)
                .Returns(mockSet1.Object);


            var service2 = new UserController(mockContext.Object);

            if (username == "TestUsername" && password == "TestPassword" && firstName == "TestFirstName" && lastName == "TestLastName" && countryName == "TestCountry" && email == "TestEmail")
            {
                Assert.DoesNotThrow(() => service2.Register("TestUsername", "TestPassword", "TestFirstName", "TestLastName", "TestCountry", "TestEmail"));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => service2.Register(username, password, firstName, lastName, countryName, email));
            }
        }

        [Test]

        public void HashingHashes(
        [Values("TestPassword")] string password)
        {


            var mockContext = new Mock<VideoGamePlatformContext>();

            var service2 = new UserController(mockContext.Object);
            var hashedPassword = service2.Hashing(password);


            Assert.That(hashedPassword == "7bcf9d89298f1bfae16fa02ed6b61908fd2fa8de45dd8e2153a3c47300765328");

        }

        [Test]
        public void AddBalanceInUserCurrencyShouldAddBalanceInUserCurrency()
        {
            var data1 = new List<Country>
            {
                new Country("ttt","TestCountry","TCC",1) { CountryCode="ttt"}
            }.AsQueryable();

            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet1 = new Mock<DbSet<Country>>();
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());



            var mockSet2 = new Mock<DbSet<User>>();
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(data2.Provider);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Expression)
                .Returns(data2.Expression);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.ElementType)
                .Returns(data2.ElementType);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data2.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Countries)
                .Returns(mockSet1.Object);

            var service = new UserController(mockContext.Object);

            var user = service.GetUserById(1);
            service.AddBalanceInUserCurrency(user, 13);

            Assert.AreEqual(13, service.GetUserById(1).Balance);


        }

        [Test]
        public void GetUserInfoByUsernameShouldGetUserInfoByUsername()
        {
            var data1 = new List<Country>
            {
                new Country("ttt","TestCountry","TCC",1) { CountryCode="ttt"}
            }.AsQueryable();

            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet1 = new Mock<DbSet<Country>>();
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());



            var mockSet2 = new Mock<DbSet<User>>();
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(data2.Provider);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Expression)
                .Returns(data2.Expression);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.ElementType)
                .Returns(data2.ElementType);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data2.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Countries)
                .Returns(mockSet1.Object);

            var service = new UserController(mockContext.Object);
            var service1 = new CountryController(mockContext.Object);

            var user = service.GetUserById(1);
            var userCountry = service1.GetCountryByCountryCode(user.CountryCode);
            string userInfo = $"{user.Username}: {user.FirstName} {user.LastName}, From: {userCountry.Name}";
            Assert.That(userInfo == service.GetUserInfoByUsername(user.Username));
        }

        [Test]
        public void UserTriesToBuyGameShouldBuyGame()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data1 = new List<Country>
            {
                new Country("ttt","TestCountry","TCC",1) { CountryCode="ttt"}
            }.AsQueryable();

            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var data3 = new List<UserGameCollection>
            {
                new UserGameCollection(1,1)
            }.AsQueryable();

            var mockSet = new Mock<DbSet<VideoGame>>();
            mockSet.As<IQueryable<VideoGame>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<VideoGame>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<VideoGame>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<VideoGame>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockSet1 = new Mock<DbSet<Country>>();
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

            var mockSet2 = new Mock<DbSet<User>>();
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(data2.Provider);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Expression)
                .Returns(data2.Expression);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.ElementType)
                .Returns(data2.ElementType);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data2.GetEnumerator());

            var mockSet3 = new Mock<DbSet<UserGameCollection>>();
            mockSet3.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.Provider)
                .Returns(data3.Provider);
            mockSet3.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.Expression)
                .Returns(data3.Expression);
            mockSet3.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.ElementType)
                .Returns(data3.ElementType);
            mockSet3.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data3.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(v => v.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(c => c.Countries)
                .Returns(mockSet1.Object);
            mockContext
                .Setup(ug => ug.UsersGameCollections)
                .Returns(mockSet3.Object);

            var service = new UserController(mockContext.Object);
            var service1 = new VideoGameController(mockContext.Object);

            var game = service1.GetGameById(1);
            var user = service.GetUserById(1);

            Assert.Throws<ArgumentException>(() => service.UserTriesToBuyGame(service.GetUserById(1), service1.GetGameById(1)));
            Assert.Throws<ArgumentException>(() => service.UserTriesToBuyGame(service.GetUserById(1), service1.GetGameById(2)));
            data2.ToList().ForEach(u => service.AddBalanceInUserCurrency(service.GetUserById(1), 20));
            Assert.DoesNotThrow(() => service.UserTriesToBuyGame(service.GetUserById(1), service1.GetGameById(2)));

        }

        [Test]
        public void GetAllUsersShouldGetAllUSers()
        {
            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 },
                new User("TestUserName1", "TestPassword1", "TestFirstName1", "TestLastName1","ttt" , "TestEmail1") {Id=2 }
            }.AsQueryable();

            var data1 = new List<Country>
            {
                new Country("ttt","TestCountry","TCC",1) { CountryCode="ttt"}
            }.AsQueryable();

            var mockSet1 = new Mock<DbSet<Country>>();
            var mockSet2 = new Mock<DbSet<User>>();

            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(data2.Provider);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Expression)
                .Returns(data2.Expression);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.ElementType)
                .Returns(data2.ElementType);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data2.GetEnumerator());

            mockSet1.As<IQueryable<Country>>()

                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());


            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Countries)
                .Returns(mockSet1.Object);


            var service = new UserController(mockContext.Object);
            var service1 = new CountryController(mockContext.Object);


            Assert.That(2 == service.GetInfoOfAllUsers().Count);
            var user = service.GetUserById(1);
            var userCountry = service1.GetCountryByCountryCode(user.CountryCode);
            var user2 = service.GetUserById(2);
            Assert.IsTrue($"{user.Username} - {user.FirstName} {user.LastName}, {userCountry.Name}, {user.Email}" == service.GetInfoOfAllUsers()[0]);
            Assert.IsFalse($"{user.Username} - {user.FirstName} {user.LastName}, {userCountry.Name}, {user.Email}" == service.GetInfoOfAllUsers()[1]);

        }

        [Test]
        public void GetUserInfoByUsernameOrEmailShouldGetUserInfoByUsernameOrEmail(
            [Values("TestUserName" , "TestEmail")]string input) 
        {
            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
                
            }.AsQueryable();

            var data1 = new List<Country>
            {
                new Country("ttt","TestCountry","TCC",1) { CountryCode="ttt"}
            }.AsQueryable();

            var mockSet1 = new Mock<DbSet<Country>>();
            var mockSet2 = new Mock<DbSet<User>>();

            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(data2.Provider);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Expression)
                .Returns(data2.Expression);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.ElementType)
                .Returns(data2.ElementType);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data2.GetEnumerator());

            mockSet1.As<IQueryable<Country>>()

                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Countries)
                .Returns(mockSet1.Object);

            var service = new UserController(mockContext.Object);

            var userInfo = service.GetUserInfoByUsernameOrEmail(input);

            Assert.That($"TestUserName - TestFirstName TestLastName, TestCountry, TestEmail"==userInfo);
        }
        [Test]
        public void GetUserByUsernameOrEmailShouldGetUserByUsernameOrEmail(
            [Values("TestUserName", "TestEmail")] string input) 
        {

            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }

            }.AsQueryable();

            var data1 = new List<Country>
            {
                new Country("ttt","TestCountry","TCC",1) { CountryCode="ttt"}
            }.AsQueryable();

            var mockSet1 = new Mock<DbSet<Country>>();
            var mockSet2 = new Mock<DbSet<User>>();

            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(data2.Provider);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.Expression)
                .Returns(data2.Expression);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.ElementType)
                .Returns(data2.ElementType);
            mockSet2.As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data2.GetEnumerator());

            mockSet1.As<IQueryable<Country>>()

                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Country>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Countries)
                .Returns(mockSet1.Object);

            var service = new UserController(mockContext.Object);

            var user = service.GetUserByUsernameOrEmail(input);

            Assert.That(service.GetUserById(1)==user);
        }
    }
}
