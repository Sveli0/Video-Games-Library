using It_career_project.Data;
using It_career_project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace It_career_project.Business
{
    public class AdminController
    {
        private VideoGamePlatformContext context;

        public AdminController()
        {
            context = new VideoGamePlatformContext();
        }

        public AdminController(VideoGamePlatformContext context)
        {
            this.context = context;
        }

        public bool LoginAttempt(string username, string passwordOne, string passwordTwo)
        {
            string hashedPasswordOne = Hashing(passwordOne);
            string hashedPasswordTwo = Hashing(passwordTwo);
            Admin adminExistsCheck = GetAdminByUsername(username);

            if (adminExistsCheck != null)
            {

                if (adminExistsCheck.HashedPasswordOne == hashedPasswordOne &&
                    adminExistsCheck.HashedPasswordTwo == hashedPasswordTwo)
                {
                    return true;
                }
            }

            return false;
        }

        public Admin GetAdminByUsername(string username)
        {
            Admin admin =
                context
                .Admins
                .FirstOrDefault(x => x.Username.Equals(username));

            return admin;
        }

        public string Hashing(string password)
        {
            using var hashing = SHA256.Create();
            byte[] bytes = hashing.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}