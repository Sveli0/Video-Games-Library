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
    class GameStudioControllerTests
    {
        [Test]
        public void AddStudioShouldAddStudio() 
        {
            var mockSet = new Mock<DbSet<GameStudio>>();

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.GameStudios)
                .Returns(mockSet.Object);

            var service = new GameStudioController(mockContext.Object);

            service.AddStudio("TestStudio", false);

            mockSet.Verify(m => m.Add(It.IsAny<GameStudio>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void EditStudioShouldEditStudio() 
        {
            var data = new List<GameStudio>
            {
                new GameStudio("TestStudio1",true){Id=1 },
                new GameStudio("TestStudio2",false){Id=2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<GameStudio>>();

            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.GameStudios)
                .Returns(mockSet.Object);

            var service = new GameStudioController(mockContext.Object);

            service.EditStudio("TestStudio1", "TestStudio1New", true);
            service.EditStudio("TestStudio2", "TestStudio2New",true);
            Assert.That("TestStudio1New"==service.GetStudioById(1).Name);
            Assert.That(true == service.GetStudioById(2).UnderContract);
        }

        [Test]
        public void EditStudioNameShouldEditStudioName() 
        {
            var data = new List<GameStudio>
            {
                new GameStudio("TestStudio1",true){Id=1 },
                new GameStudio("TestStudio2",false){Id=2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<GameStudio>>();

            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.GameStudios)
                .Returns(mockSet.Object);

            var service = new GameStudioController(mockContext.Object);

            

            Assert.Throws<ArgumentException>(()=>service.EditStudioName("TestStudio1", "TestStudio2"));

            Assert.DoesNotThrow(() => service.EditStudioName("TestStudio1", "TestStudio1New"));

        }

        [Test]
        public void EditStudioIsUnderContractShouldEditStudioIsUnderContract() 
        {
            var data = new List<GameStudio>
            {
                new GameStudio("TestStudio1",true){Id=1 },
                new GameStudio("TestStudio2",false){Id=2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<GameStudio>>();

            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.GameStudios)
                .Returns(mockSet.Object);

            var service = new GameStudioController(mockContext.Object);

            service.EditStudioIsUnderContract("TestStudio2",true);
            Assert.That(service.GetStudioById(2).UnderContract==true);
        }
        [Test]
        public void GetStudioByIdShouldGetStudioById() 
        {
            var data = new List<GameStudio>
            {
                new GameStudio("TestStudio1",true){Id=1 },
                new GameStudio("TestStudio2",false){Id=2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<GameStudio>>();

            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.GameStudios)
                .Returns(mockSet.Object);

            var service = new GameStudioController(mockContext.Object);

            Assert.That(service.GetStudioById(1).Name=="TestStudio1");
        }
        [Test]
        public void GetStudioByNameShouldGetStudioByName() 
        {
            var data = new List<GameStudio>
            {
                new GameStudio("TestStudio1",true){Id=1 },
                new GameStudio("TestStudio2",false){Id=2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<GameStudio>>();

            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.GameStudios)
                .Returns(mockSet.Object);

            var service = new GameStudioController(mockContext.Object);

            Assert.Throws<ArgumentException>(() => service.GetStudioByName(""));
            Assert.Throws<ArgumentException>(() => service.GetStudioByName("WrongStudio"));
            Assert.That(service.GetStudioByName("TestStudio1").UnderContract==true);
        }
        [Test]
        public void ValidateStudioNameShouldValidateStudioName() 
        {
            var data = new List<GameStudio>
            {
                new GameStudio("TestStudio1",true){Id=1 },
                new GameStudio("TestStudio2",false){Id=2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<GameStudio>>();

            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<GameStudio>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.GameStudios)
                .Returns(mockSet.Object);

            var service = new GameStudioController(mockContext.Object);

            Assert.Throws<ArgumentException>(() => service.ValidateStudioName(""));
            Assert.Throws<ArgumentException>(() => service.ValidateStudioName("TestStudio1"));
            

        }

    }
}
