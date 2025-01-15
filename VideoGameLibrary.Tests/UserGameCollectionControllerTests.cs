using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using It_career_project.Data.Models;
using It_career_project.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using It_career_project;
using It_career_project.Business;

namespace VideoGameLibrary.Tests
{
    class UserGameCollectionControllerTests
    {
        [Test]
        public void GetGameCollectionByUserShouldGetGameCollectionByUser()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 },
                new User("TestUserName1", "TestPassword1", "TestFirstName1", "TestLastName1","ttt" , "TestEmail1") {Id=2 }
            }.AsQueryable();

            var data1 = new List<UserGameCollection>
            {
                new UserGameCollection(1,1),
                new UserGameCollection(1,2)
            }.AsQueryable();

            var mockSet2 = new Mock<DbSet<User>>();
            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<UserGameCollection>>();
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

            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(ugc => ugc.UsersGameCollections)
                .Returns(mockSet1.Object);

            var service = new UserGameCollectionController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);

            var user = service1.GetUserById(1);
            var user1 = service1.GetUserById(2);

            Assert.That(service.GetGameCollectionByUser(user).Count == 2);
            Assert.Throws<ArgumentException>(() => service.GetGameCollectionByUser(user1));
        }
        [Test]
        public void GetGameInfoCollectionByUserShouldGetGameInfoCollectionByUser()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,1) {Id=1 },
                new VideoGame("TestGame2",10,1,1) {Id=2}
            }.AsQueryable();

            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }

            }.AsQueryable();

            var data1 = new List<UserGameCollection>
            {
                new UserGameCollection(1,1),
                new UserGameCollection(1,2)
            }.AsQueryable();

            var data3 = new List<Genre>
            {
                new Genre("TestGenre"){ Id=1}
            }.AsQueryable();

            var data4 = new List<GameStudio>
            {
                new GameStudio("TestGameStudio"){Id=1 }
            }.AsQueryable();

            var mockSet2 = new Mock<DbSet<User>>();
            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<UserGameCollection>>();
            var mockSet3 = new Mock<DbSet<Genre>>();
            var mockSet4 = new Mock<DbSet<GameStudio>>();

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

            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

            mockSet3.As<IQueryable<Genre>>()
                .Setup(m => m.Provider)
                .Returns(data3.Provider);
            mockSet3.As<IQueryable<Genre>>()
                .Setup(m => m.Expression)
                .Returns(data3.Expression);
            mockSet3.As<IQueryable<Genre>>()
                .Setup(m => m.ElementType)
                .Returns(data3.ElementType);
            mockSet3.As<IQueryable<Genre>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data3.GetEnumerator());

            mockSet4.As<IQueryable<GameStudio>>()
                .Setup(m => m.Provider)
                .Returns(data4.Provider);
            mockSet4.As<IQueryable<GameStudio>>()
                .Setup(m => m.Expression)
                .Returns(data4.Expression);
            mockSet4.As<IQueryable<GameStudio>>()
                .Setup(m => m.ElementType)
                .Returns(data4.ElementType);
            mockSet4.As<IQueryable<GameStudio>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data4.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(ugc => ugc.UsersGameCollections)
                .Returns(mockSet1.Object);
            mockContext
                .Setup(g => g.Genres)
                .Returns(mockSet3.Object);
            mockContext
                .Setup(gs => gs.GameStudios)
                .Returns(mockSet4.Object);

            var service = new UserGameCollectionController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);

            var user = service1.GetUserById(1);

            Assert.That(service.GetGameInfoCollectionByUser(user).Count == 2);

        }
        [Test]
        public void UserOwnsGameIsTrueWhenUserOwnsAGame()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,1) {Id=1 },
                new VideoGame("TestGame2",10,1,1) {Id=2}
            }.AsQueryable();

            var data1 = new List<UserGameCollection>
            {
                new UserGameCollection(1,1)
            }.AsQueryable();

            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }

            }.AsQueryable();

            var mockSet2 = new Mock<DbSet<User>>();
            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<UserGameCollection>>();

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

            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<UserGameCollection>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(ugc => ugc.UsersGameCollections)
                .Returns(mockSet1.Object);

            var service = new UserGameCollectionController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);

            var user = service1.GetUserById(1);

            Assert.IsTrue(service.UserOwnsGame(user, "TestGame1"));
            Assert.IsFalse(service.UserOwnsGame(user, "TestGame2"));

        }
    }
}