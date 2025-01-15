using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using It_career_project.Data.Models;
using It_career_project.Data;
using System.Collections.Generic;
using System.Linq;
using It_career_project.Business;

namespace VideoGameLibrary.Tests
{
    class CountryControllerTests
    {
        [Test]
        public void GetCountryByNameShouldGetCountryByName() 
        {
            var data1 = new List<Country>
            {
                new Country("tt1","TestCountry1","TC1",1) { CountryCode="tt1"},
                new Country("tt2","TestCountry2","TC2",2) { CountryCode="tt2"}
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

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.Countries)
                .Returns(mockSet1.Object);

            var service = new CountryController(mockContext.Object);

            var country1 = service.GetCountryByName("TestCountry1");
            var country2 = service.GetCountryByName("TestCountry2");
            Assert.That(country1.Currency=="TC1");
            Assert.That(country2.Currency=="TC2");

        }

        [Test]
        public void GetCountryByCountryCodeShouldGetCountryByCountryCode() 
        {
            var data1 = new List<Country>
            {
                new Country("tt1","TestCountry1","TC1",1) { CountryCode="tt1"},
                new Country("tt2","TestCountry2","TC2",2) { CountryCode="tt2"}
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

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.Countries)
                .Returns(mockSet1.Object);

            var service = new CountryController(mockContext.Object);

            var country1 = service.GetCountryByCountryCode("tt1");
            var country2 = service.GetCountryByCountryCode("tt2");
            Assert.That(country1.Currency == "TC1");
            Assert.That(country2.Currency == "TC2");
        }

        [Test]
        public void GetCountryCurrencyExchangeRateShouldGetCountryCurrencyExchangeRate() 
        {
            var data1 = new List<Country>
            {
                new Country("tt1","TestCountry1","TC1",1) { CountryCode="tt1"},
                new Country("tt2","TestCountry2","TC2",2) { CountryCode="tt2"}
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

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.Countries)
                .Returns(mockSet1.Object);

            var service = new CountryController(mockContext.Object);

            var country1 = service.GetCountryByCountryCode("tt1");
            var country2 = service.GetCountryByCountryCode("tt2");

            Assert.That(country1.CurrencyExchangeRateToEuro==1);
            Assert.That(country2.CurrencyExchangeRateToEuro == 2);
        }
    }
}
