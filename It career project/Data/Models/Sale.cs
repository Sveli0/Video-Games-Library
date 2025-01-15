using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace It_career_project.Data.Models
{
    public class Sale
    {
        /// <summary>
        /// Constructor for Business
        /// </summary>
        /// A constructor that takes paramaters and is used in Controller classes for ease of access
        public Sale(int gameId, int discount, DateTime startDate, DateTime endDate)
        {
            GameId = gameId;
            Discount = discount;
            StartDate = startDate;
            EndDate = endDate;
        }
        /// <summary>
        /// Constructor for Migration
        /// </summary>
        /// An empty constructor that is used when migrating the database
        public Sale()
        {   

        }

        /// <summary>
        /// Primary Key/Foreign Key
        /// </summary>
        /// This table has a primary key that is also a foreign key that connects to the table VideoGame
        [ForeignKey(nameof(Data.Models.VideoGame))]
        [Key]
        [Required]
        public int GameId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public VideoGame VideoGame { get; set; }

        [Required]
        public int Discount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}