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
    class GenreControllerTests
    {
        [Test]
        public void GetGenreByIdShouldGetGenreById() 
        {
            var data = new List<Genre>
            {
                new Genre("TestGenre"){Id=1}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Genre>>();
            mockSet.As<IQueryable<Genre>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<Genre>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<Genre>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<Genre>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(g => g.Genres)
                .Returns(mockSet.Object);

            var service = new GenreController(mockContext.Object);

            var genre = service.GetGenreById(1);
            Assert.That(genre.Name=="TestGenre");
        }
        [Test]
        public void GetGenreByName() 
        {
            var data = new List<Genre>
            {
                new Genre("TestGenre"){Id=1}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Genre>>();
            mockSet.As<IQueryable<Genre>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<Genre>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<Genre>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<Genre>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(g => g.Genres)
                .Returns(mockSet.Object);

            var service = new GenreController(mockContext.Object);

            var genre = service.GetGenreByName("TestGenre");
            Assert.That(genre.Id==1);
        }
    }
}
