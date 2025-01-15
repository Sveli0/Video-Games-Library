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
    class ReviewControllerTests
    {
        [Test]
        public void AddReviewShouldAddReview()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();
            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<Review>>();
            var mockSet2 = new Mock<DbSet<User>>();

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

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(c => c.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Reviews)
                .Returns(mockSet1.Object);

            var service = new ReviewController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);

            service.AddReview(service1.GetUserById(1), "TestGame1", "TestText", false);
            mockContext.Verify(m => m.Add(It.IsAny<Review>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);

        }
        [Test]
        public void GetShortInfoOfAllUserReviewsShouldGetShortInfoOfAllUserReviews()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data1 = new List<Review>
            {
                new Review(1,1,"TestReview",true),
                new Review(1,2,"TestReview",false)
            }.AsQueryable();
            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<Review>>();
            var mockSet2 = new Mock<DbSet<User>>();

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

            mockSet1.As<IQueryable<Review>>()
               .Setup(m => m.Provider)
               .Returns(data1.Provider);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Review>>()
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
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(c => c.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Reviews)
                .Returns(mockSet1.Object);


            var service = new ReviewController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);

            var user = service1.GetUserById(1);
            var list = service.GetShortInfoOfAllUserReviews(user);
            Assert.That(list.Count() == 2);

        }
        [Test]
        public void GetUserReviewInfoOfSpecificGameShouldGetUserReviewInfoOfSpecificGame()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data1 = new List<Review>
            {
                new Review(1,1,"TestReview",true)

            }.AsQueryable();
            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<Review>>();
            var mockSet2 = new Mock<DbSet<User>>();

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

            mockSet1.As<IQueryable<Review>>()
               .Setup(m => m.Provider)
               .Returns(data1.Provider);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Review>>()
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
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(c => c.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Reviews)
                .Returns(mockSet1.Object);

            var service = new ReviewController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);
            var user = service1.GetUserById(1);
            Assert.Throws<ArgumentException>(() => service.GetUserReviewInfoOfSpecificGame(user, "TestGame2"));
            Assert.That($"Review By {user.Username} for Game: TestGame1 \n" +
                $"Type: Positive \n" +
                $"TestReview\n" == service.GetUserReviewInfoOfSpecificGame(user, "TestGame1"));
        }
        [Test]
        public void DeleteReviewShouldDeleteReview()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data1 = new List<Review>
            {
                new Review(1,1,"TestReview",true)

            }.AsQueryable();
            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<Review>>();
            var mockSet2 = new Mock<DbSet<User>>();

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

            mockSet1.As<IQueryable<Review>>()
               .Setup(m => m.Provider)
               .Returns(data1.Provider);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Review>>()
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
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(c => c.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Reviews)
                .Returns(mockSet1.Object);

            var service = new ReviewController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);

            var user = service1.GetUserById(1);
            Assert.Throws<ArgumentException>(() => service.DeleteReview(user, "TestGame2"));
            service.DeleteReview(user, "TestGame1");
            mockContext.Verify(m => m.Remove(It.IsAny<Review>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);

        }
        [Test]
        public void GetReviewInfoByGameTitleShouldGetReviewInfoByGameTitle()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data1 = new List<Review>
            {
                new Review(1,1,"TestReview",true)

            }.AsQueryable();
            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<Review>>();
            var mockSet2 = new Mock<DbSet<User>>();

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

            mockSet1.As<IQueryable<Review>>()
               .Setup(m => m.Provider)
               .Returns(data1.Provider);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Review>>()
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
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(c => c.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Reviews)
                .Returns(mockSet1.Object);

            var service = new ReviewController(mockContext.Object);

            var list = service.GetReviewInfoByGameTitle("TestGame1");

            Assert.That(list.Count() == 1);
        }
        [Test]
        public void GetFirstFiveReviewsOfAGameByTitleShouldGetFirstFiveReviewsOfAGameByTitle()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data1 = new List<Review>
            {
                new Review(1,1,"TestReview",true),
                new Review(2,1,"TestReview",true),
                new Review(3,1,"TestReview",true),
                new Review(4,1,"TestReview",true),
                new Review(5,1,"TestReview",true),
                new Review(6,1,"TestReview",true)

            }.AsQueryable();
            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 },
                new User("TestUserName1", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=2 },
                new User("TestUserName2", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=3 },
                new User("TestUserName3", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=4 },
                new User("TestUserName4", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=5 },
                new User("TestUserName5", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=6 },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<Review>>();
            var mockSet2 = new Mock<DbSet<User>>();

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

            mockSet1.As<IQueryable<Review>>()
               .Setup(m => m.Provider)
               .Returns(data1.Provider);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Review>>()
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
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(c => c.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Reviews)
                .Returns(mockSet1.Object);

            var service = new ReviewController(mockContext.Object);

            var list = service.GetFirstFiveReviewsOfAGameByTitle("TestGame1");

            Assert.That(list.Count() == 5);
        }
        [Test]
        public void GetReviewsByGameTitleShouldGetReviewsByGameTitle()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data1 = new List<Review>
            {
                new Review(1,1,"TestReview",true)

            }.AsQueryable();
            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<Review>>();
            var mockSet2 = new Mock<DbSet<User>>();

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

            mockSet1.As<IQueryable<Review>>()
               .Setup(m => m.Provider)
               .Returns(data1.Provider);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Review>>()
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
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(c => c.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Reviews)
                .Returns(mockSet1.Object);

            var service = new ReviewController(mockContext.Object);

            var list = service.GetReviewsByGameTitle("TestGame1");

            Assert.That(list.Count() == 1);
        }
        [Test]
        public void UserHasReviewForShouldUserHasReviewFor() 
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data1 = new List<Review>
            {
                new Review(1,1,"TestReview",true)

            }.AsQueryable();
            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<Review>>();
            var mockSet2 = new Mock<DbSet<User>>();

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

            mockSet1.As<IQueryable<Review>>()
               .Setup(m => m.Provider)
               .Returns(data1.Provider);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Review>>()
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
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(c => c.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Reviews)
                .Returns(mockSet1.Object);

            var service = new ReviewController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);

            Assert.Throws<ArgumentException>(()=>service.UserHasReviewFor(service1.GetUserById(1),"TestGame1"));
            Assert.DoesNotThrow(()=>service.UserHasReviewFor(service1.GetUserById(1), "TestGame2"));
        }
        [Test]
        public void GetUserReviewsShouldGetUserReviews() 
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data1 = new List<Review>
            {
                new Review(1,1,"TestReview",true)

            }.AsQueryable();
            var data2 = new List<User>
            {
                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 },
                new User("TestUserName1", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<Review>>();
            var mockSet2 = new Mock<DbSet<User>>();

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

            mockSet1.As<IQueryable<Review>>()
               .Setup(m => m.Provider)
               .Returns(data1.Provider);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Review>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Review>>()
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
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(c => c.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Reviews)
                .Returns(mockSet1.Object);

            var service = new ReviewController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);

            var user1 = service1.GetUserById(1);
            var user2 = service1.GetUserById(2);

            Assert.Throws<ArgumentException>(() => service.GetUserReviews(user2));
            Assert.That(service.GetUserReviews(user1).Count()==1);
        }

    }
}
