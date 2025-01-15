using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace It_career_project.Data.Models
{
    public class Review
    {
        /// <summary>
        /// Constructor for Business
        /// </summary>
        /// A constructor that takes paramaters and is used in Controller classes for ease of access
        public Review(int userId, int gameId, string reviewText, bool isPositive)
        {
            UserId = userId;
            GameId = gameId;
            ReviewText = reviewText;
            IsPositive = isPositive;
        }
        /// <summary>
        /// Constructor for Migration
        /// </summary>
        /// An empty constructor that is used when migrating the database
        public Review()
        {

        }
        /// <summary>
        /// Primary Keys
        /// </summary>
        /// This class/table has a composite primary key, made up of two foreign keys that connect to the User and VideoGame tables
        [ForeignKey(nameof(Data.Models.User))]
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public User User { get; set; }

        [ForeignKey(nameof(Data.Models.VideoGame))]
        [Required]
        public int GameId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public VideoGame VideoGame { get; set; }

        public string ReviewText { get; set; }
        [Required]
        public bool IsPositive { get; set; }

    }
}