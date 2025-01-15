using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace It_career_project.Data.Models
{
    public class User
    {
        /// <summary>
        /// Constructor for Business
        /// </summary>
        /// A constructor that takes paramaters and is used in Controller classes for ease of access
        public User(string username, string hashedPassword, string firstName, string lastName,
            string countryCode, string email, decimal balance = 0)
        {
            Username = username;
            HashedPassword = hashedPassword;
            FirstName = firstName;
            LastName = lastName;
            CountryCode = countryCode;
            Email = email;
            Balance = balance;
            UserGameCollections = new HashSet<UserGameCollection>();
            Reviews = new HashSet<Review>();
        }
        /// <summary>
        /// Constructor for Migration
        /// </summary>
        /// An empty constructor that is used when migrating the database
        public User()
        {
            UserGameCollections = new HashSet<UserGameCollection>();
            Reviews = new HashSet<Review>();
        }
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Username { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// Foreign Key connected with table Countries
        /// </summary>
        [ForeignKey(nameof(It_career_project.Data.Models.Country))]
        [Required]
        public string CountryCode { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public Country Country { get; set; }

        [Required]
        public string Email { get; set; }

        public decimal Balance { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        public ICollection<UserGameCollection> UserGameCollections { get; set; }

        public ICollection<Review> Reviews { get; set; }

    }
}