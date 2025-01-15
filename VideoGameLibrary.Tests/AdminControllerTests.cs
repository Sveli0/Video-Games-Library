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
    class AdminControllerTests
    {
        [Test]
        public void LoginAttemptShouldGiveResult() 
        {
            var data = new List<Admin>
            {
                new Admin("TestUsername", "TestFirstName","TestLastName","TestPassword1", "TestPassword2")
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Admin>>();

            mockSet.As<IQueryable<Admin>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<Admin>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<Admin>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<Admin>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.Admins)
                .Returns(mockSet.Object);

            var service = new AdminController(mockContext.Object);
            data.ToList().ForEach(a => a.HashedPasswordOne = service.Hashing(a.HashedPasswordOne));
            data.ToList().ForEach(a => a.HashedPasswordTwo = service.Hashing(a.HashedPasswordTwo));

            Assert.IsFalse(service.LoginAttempt("WrongUsername","TestPassword","TestPassword"));
            Assert.IsTrue(service.LoginAttempt("TestUsername", "TestPassword1", "TestPassword2"));
        }
        [Test]
        public void GetAdminByUsernameShouldGetAdminByUsername() 
        {
            var data = new List<Admin>
            {
                new Admin("TestUsername", "TestFirstName","TestLastName","TestPassword1", "TestPassword2")
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Admin>>();

            mockSet.As<IQueryable<Admin>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<Admin>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<Admin>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<Admin>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            var mockContext = new Mock<VideoGamePlatformContext>();
            mockContext
                .Setup(c => c.Admins)
                .Returns(mockSet.Object);

            var service = new AdminController(mockContext.Object);

            var admin = service.GetAdminByUsername("TestUsername");

            Assert.That(admin.FirstName== "TestFirstName");
            Assert.That(admin.LastName == "TestLastName");
        }
        [Test]
        public void HashingHashes(
        [Values("TestPassword")] string password)
        {


            var mockContext = new Mock<VideoGamePlatformContext>();

            var service2 = new AdminController(mockContext.Object);
            var hashedPassword = service2.Hashing(password);


            Assert.That(hashedPassword == "7bcf9d89298f1bfae16fa02ed6b61908fd2fa8de45dd8e2153a3c47300765328");

        }
    }
}
