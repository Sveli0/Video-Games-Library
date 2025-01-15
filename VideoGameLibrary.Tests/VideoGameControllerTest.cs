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
    class VideoGameControllerTest
    {
        [Test]
        public void ChangeGameStudioShouldChangeGameStudio()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2),
                new VideoGame("TestGame2",10,3,4)

            }.AsQueryable();

            var data1 = new List<GameStudio>
            {
                new GameStudio("TestGameStudio", true) {Id=1}

            }.AsQueryable();

            var mockSet1 = new Mock<DbSet<GameStudio>>();
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

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

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(s => s.GameStudios)
                .Returns(mockSet1.Object);

            var service = new VideoGameController(mockContext.Object);
            var service1 = new GameStudioController(mockContext.Object);


            int id1 = service.GetGameByTitle("TestGame1").StudioId;
            int id2 = service1.GetStudioByName("TestGameStudio").Id;

            Assert.AreEqual(id1, id2);
        }



        [Test]
        public void ChangeGameGenreShouldChangeGameGenre(
        [Values("TestGame1", "TestGame2")] string gameTitle,
        [Values("TestGenre1", "TestGenre2")] string genreName)
        {
            var data = new List<VideoGame>
            {
            new VideoGame("TestGame1",15,1,2){Id=1},
            new VideoGame("TestGame2",10,3,4){Id=2 }
            }.AsQueryable();

            var data1 = new List<Genre>
        {
        new Genre("TestGenre1"){Id=1 },
        new Genre("TestGenre2"){Id=2}
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

            var mockSet1 = new Mock<DbSet<Genre>>();
            mockSet1.As<IQueryable<Genre>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<Genre>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Genre>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Genre>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(g => g.Genres)
                .Returns(mockSet1.Object);

            var service = new VideoGameController(mockContext.Object);
            var service1 = new GenreController(mockContext.Object);

            // Act
            service.ChangeGameGenre(gameTitle, genreName);

            // Assert
            var updatedGame = service.GetGameByTitle(gameTitle);
            var updatedGenre = service1.GetGenreByName(genreName);

            Assert.That(updatedGame, Is.Not.Null);
            Assert.AreEqual(updatedGame.GenreId, updatedGenre.Id);
        }


        [TestCase]

        public void ChangeGamePriceShouldChangeGamePrice()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id = 1},
                new VideoGame("TestGame2",10,3,4) {Id = 2}

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

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);

            var service = new VideoGameController(mockContext.Object);
            data.ToList().ForEach(v => service.ChangeGamePrice(v.GameTitle, 10));
            VideoGame changedVideoGame = service.GetGameById(1);
            VideoGame changedVideoGame2 = service.GetGameById(2);
            Assert.AreEqual(changedVideoGame.Price, 10);
            Assert.AreEqual(changedVideoGame2.Price, 10);
        }

        [TestCase]

        public void ChangeGameTitleShouldChangeGameTitle()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2),
                new VideoGame("TestGame2",10,3,4)

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

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);

            var service = new VideoGameController(mockContext.Object);

            data.ToList().ForEach(v => service.ChangeGameTitle(v.GameTitle, "TestTitle"));

        }

        [Test]
        public void AddGameShouldAddGame()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame", 15,1,1)
            }.AsQueryable();
            var data1 = new List<Genre>
            {
                new Genre("TestGenre") {Id=1 }

            }.AsQueryable();
            var data2 = new List<GameStudio>
            {
                new GameStudio("TestGameStudio") {Id=1 }

            }.AsQueryable();


            // Arrange
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

            var mockSet2 = new Mock<DbSet<Genre>>();
            mockSet2.As<IQueryable<Genre>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet2.As<IQueryable<Genre>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet2.As<IQueryable<Genre>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet2.As<IQueryable<Genre>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

            var mockSet3 = new Mock<DbSet<GameStudio>>();
            mockSet3.As<IQueryable<GameStudio>>()
                .Setup(m => m.Provider)
                .Returns(data2.Provider);
            mockSet3.As<IQueryable<GameStudio>>()
                .Setup(m => m.Expression)
                .Returns(data2.Expression);
            mockSet3.As<IQueryable<GameStudio>>()
                .Setup(m => m.ElementType)
                .Returns(data2.ElementType);
            mockSet3.As<IQueryable<GameStudio>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data2.GetEnumerator());


            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext.Setup(c => c.VideoGames).Returns(mockSet.Object);
            mockContext.Setup(g => g.Genres).Returns(mockSet2.Object);
            mockContext.Setup(gs => gs.GameStudios).Returns(mockSet3.Object);
            var service = new VideoGameController(mockContext.Object);

            string gameStudioName = "TestGameStudio";
            string genreName = "TestGenre";

            // Act
            service.AddGame("TestGame10", 15, gameStudioName, genreName);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<VideoGame>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }


        [TestCase(1)]

        public void GetGameByIdShouldGetGameById(int id)
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}

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

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);

            var service = new VideoGameController(mockContext.Object);


            var game = service.GetGameById(id);
            Assert.IsNotNull(game);
            Assert.AreEqual(id, game.Id);

        }

        [TestCase(15)]

        public void GetGamePriceByTitleShouldGetGamePriceByTitle(decimal price)
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

            var mockSet1 = new Mock<DbSet<User>>();
            var mockSet2 = new Mock<DbSet<Country>>();

            mockSet1.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(data2.Provider);
            mockSet1.As<IQueryable<User>>()
                .Setup(m => m.Expression)
                .Returns(data2.Expression);
            mockSet1.As<IQueryable<User>>()
                .Setup(m => m.ElementType)
                .Returns(data2.ElementType);
            mockSet1.As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data2.GetEnumerator());

            mockSet2.As<IQueryable<Country>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet2.As<IQueryable<Country>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet2.As<IQueryable<Country>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet2.As<IQueryable<Country>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(c => c.Countries)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(c => c.Users)
                .Returns(mockSet1.Object);


            var service = new VideoGameController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);
            var service2 = new CountryController(mockContext.Object);
            var gamePrice = service.GetGamePriceByTitle("TestGame1", service1.GetUserByUsername("TestUserName"));

            Assert.IsNotNull(gamePrice);
            Assert.AreEqual(price, gamePrice);

        }

        [TestCase("TestGame1")]

        public void GetGameByTitleShouldGetGameByTitle(string gameTitle)
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
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

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);


            var service = new VideoGameController(mockContext.Object);

            var game = service.GetGameByTitle(gameTitle);


            Assert.IsNotNull(game);
            Assert.That(gameTitle == game.GameTitle);
        }

        [Test]

        public void ValidateGameTitleShouldValidateGameTitle()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data2 = new List<User>
            {

                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<VideoGame>>();
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
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);

            var service = new VideoGameController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);

            var game = service.GetGameById(1);
            var user = service1.GetUserById(1);
            Assert.Throws<ArgumentException>(() => service.ValidateGameTitle(user, game.GameTitle));

        }

        [Test]

        public void GetGamesStartingWithLetterShouldGetGamesStartingWithLetter()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("aTestGame1",15,1,1) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2},
                new VideoGame("aTestGame1",15,1,1, false) {Id=3 }
            }.AsQueryable();

            var data1 = new List<Country>
            {
                new Country("ttt","TestCountry","TCC",1) { CountryCode="ttt"}
            }.AsQueryable();

            var data2 = new List<User>
            {

                new User("TestUserName", "TestPassword", "TestFirstName", "TestLastName","ttt" , "TestEmail") {Id=1 }

            }.AsQueryable();

            var data3 = new List<Genre>
            {
                new Genre("TestGenre"){Id=1}
            }.AsQueryable();

            var data4 = new List<GameStudio>
            {
                new GameStudio("TestStudio") {Id=1}
            }.AsQueryable();


            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<Country>>();
            var mockSet2 = new Mock<DbSet<User>>();
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
               .Setup(cs => cs.Countries)
               .Returns(mockSet1.Object);
            mockContext
                .Setup(u => u.Users)
                .Returns(mockSet2.Object);
            mockContext
                .Setup(g => g.Genres)
                .Returns(mockSet3.Object);
            mockContext
                .Setup(gs => gs.GameStudios)
                .Returns(mockSet4.Object);


            var service = new VideoGameController(mockContext.Object);
            var service1 = new UserController(mockContext.Object);
            var service2 = new GenreController(mockContext.Object);
            var service3 = new GameStudioController(mockContext.Object);
            var service4 = new CountryController(mockContext.Object);

            var game = service.GetGameById(1);
            var user = service1.GetUserById(1);
            var letter1 = 'a';
            var letter2 = 'b';
            Assert.Throws<ArgumentException>(() => service.GetGamesStartingWithLetter(user, letter2));
            var gamesWithA = service.GetGamesStartingWithLetter(user, letter1);
            Assert.IsTrue(gamesWithA[0] == ($"{game.GameTitle} - {service2.GetGenreById(1).Name}, {service3.GetStudioById(1).Name}, " +
                    $"{game.Price / service4.GetCountryByCountryCode("ttt").CurrencyExchangeRateToEuro:f2} {service4.GetCountryByCountryCode("ttt").Currency}"));
            Assert.IsTrue(gamesWithA.Count == 1);
        }
        [Test]
        public void MakeGameUnavailableActuallyMakesAGameUnavailable()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) { Id = 1},
                new VideoGame("TestGame2",10,3,4) { Id = 2}

            }.AsQueryable();

            var data1 = new List<GameStudio>
            {
                new GameStudio("TestGameStudio", true) {Id=1}

            }.AsQueryable();

            var mockSet1 = new Mock<DbSet<GameStudio>>();
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

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

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(s => s.GameStudios)
                .Returns(mockSet1.Object);

            var service = new VideoGameController(mockContext.Object);
            var service1 = new GameStudioController(mockContext.Object);
            data.ToList().ForEach(v => service.MakeGameUnavailable(v.GameTitle));


            Assert.Throws<ArgumentException>(() => service.MakeGameUnavailable("Test"));

            Assert.AreEqual(service.GetGameById(1).IsAvailable, false);
            Assert.AreEqual(service.GetGameById(2).IsAvailable, false);
        }
        [Test]
        public void GetGameInfoByTitleGetsGameInfoByTitle()
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) { Id = 1},
                new VideoGame("TestGame2",10,1,1) { Id = 2}

            }.AsQueryable();

            var data1 = new List<GameStudio>
            {
                new GameStudio("TestGameStudio", true) {Id=1}

            }.AsQueryable();

            var data3 = new List<Genre>
            {
                new Genre("TestGenre"){Id=1 },
                new Genre("TestGenre2"){Id=2 }
            }.AsQueryable();

            var mockSet1 = new Mock<DbSet<GameStudio>>();
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<GameStudio>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

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


            var mockSet3 = new Mock<DbSet<Genre>>();
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

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);
            mockContext
                .Setup(s => s.GameStudios)
                .Returns(mockSet1.Object);
            mockContext.
                Setup(f => f.Genres)
                .Returns(mockSet3.Object);

            var service = new VideoGameController(mockContext.Object);
            var service1 = new GameStudioController(mockContext.Object);


            Assert.Throws<ArgumentException>(() => service.GetGameInfoByTitle("Test"));
            decimal price1 = 15.00m;
            decimal price2 = 10.00m;
            Assert.AreEqual(service.GetGameInfoByTitle("TestGame1"), $"{"TestGame1"} - {price1:f2} EUR, {"TestGameStudio"}, {"TestGenre2"}");
            Assert.AreEqual(service.GetGameInfoByTitle("TestGame2"), $"{"TestGame2"} - {price2:f2} EUR, {"TestGameStudio"}, {"TestGenre"}");
        }

    }
}