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
    class SalesControllerTests
    {
        [Test]
        public void AddSaleShouldAddSale() 
        {
            var data = new List<VideoGame>
            {
                new VideoGame("TestGame1",15,1,2) {Id=1 },
                new VideoGame("TestGame2",10,3,4) {Id=2}
            }.AsQueryable();

            var data1 = new List<Sale>
            {
                new Sale(1,20,DateTime.Parse("01/02/2023"),DateTime.Parse("10/04/2023")),
            }.AsQueryable();

            
            var mockSet = new Mock<DbSet<VideoGame>>();
            var mockSet1 = new Mock<DbSet<Sale>>();

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

            mockSet1.As<IQueryable<Sale>>()
                .Setup(m => m.Provider)
                .Returns(data1.Provider);
            mockSet1.As<IQueryable<Sale>>()
                .Setup(m => m.Expression)
                .Returns(data1.Expression);
            mockSet1.As<IQueryable<Sale>>()
                .Setup(m => m.ElementType)
                .Returns(data1.ElementType);
            mockSet1.As<IQueryable<Sale>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data1.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.Sales)
                .Returns(mockSet1.Object);
            mockContext
                .Setup(c => c.VideoGames)
                .Returns(mockSet.Object);

            var service = new SaleController(mockContext.Object);

            Assert.Throws<ArgumentException>(() => service.AddSale("TestGame1", 20, DateTime.Parse("10/02/2023 00:00"), DateTime.Parse("10/04/2023 00:00")));
            Assert.Throws<ArgumentException>(() => service.AddSale("TestGame1", 101, DateTime.Parse("10/02/2023 00:00"), DateTime.Parse("10/04/2023 00:00")));
            Assert.Throws<ArgumentException>(() => service.AddSale("TestGame1", 20, DateTime.Parse("10/04/2023 00:00"), DateTime.Parse("10/02/2023 00:00")));
            Assert.Throws<ArgumentException>(() => service.AddSale("TestGame1", -1, DateTime.Parse("10/02/2023 00:00"), DateTime.Parse("10/04/2023 00:00")));
            Assert.Throws<ArgumentException>(() => service.AddSale("TestGame1", 20, DateTime.Parse("10/02/2023 00:00"), DateTime.Parse("01/01/2023 00:00")));
        }
    }
}
