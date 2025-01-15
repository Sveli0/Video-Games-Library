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
    class GiftCardControllerTests
    {
        [Test]
        public void RedeemGiftCardShouldRedeemGiftCard(
            [Values("TestCode")] string code)
        {
            var data = new List<GiftCard>
            {
                new GiftCard(code, 20){Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<GiftCard>>();



            mockSet.As<IQueryable<GiftCard>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<GiftCard>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<GiftCard>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<GiftCard>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(g => g.GiftCards)
                .Returns(mockSet.Object);

            var service = new GiftCardController(mockContext.Object);
       
            Assert.That(service.RedeemGiftCard(code) == 20);
        }

        [Test]
        public void GetCardByCodeShouldGetCardByCode(
            [Values("TestCode")] string code)
        {
            var data = new List<GiftCard>
            {
                new GiftCard(code, 20){Id=1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<GiftCard>>();

            mockSet.As<IQueryable<GiftCard>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<GiftCard>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<GiftCard>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<GiftCard>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(g => g.GiftCards)
                .Returns(mockSet.Object);

            var service = new GiftCardController(mockContext.Object);

            Assert.IsNotNull(service.GetCardByCode(code));
            Assert.That(service.GetCardByCode(code).Value == 20);
            Assert.That(service.GetCardByCode(code).Code == code);
            Assert.That(service.GetCardByCode(code).Id == 1);


        }
    }
}
