using System.ComponentModel.DataAnnotations;

namespace It_career_project.Data.Models
{
   public class Admin
    {
        /// <summary>
        /// Constructor for Migration
        /// </summary>
        /// Empty constructor for the class as it is necessary, when migrating
        public Admin()
        {

        }
        public Admin(string username, string firstName, string lastName, string hashedPasswordOne, string hashedPasswordTwo) 
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            HashedPasswordOne = hashedPasswordOne;
            HashedPasswordTwo = hashedPasswordTwo;

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

        [Required]
        public string HashedPasswordOne { get; set; }

        [Required]
        public string HashedPasswordTwo { get; set; }

        public string Email { get; set; }
    }
}