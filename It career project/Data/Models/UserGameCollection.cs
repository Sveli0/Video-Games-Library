using System.ComponentModel.DataAnnotations.Schema;

namespace It_career_project.Data.Models
{
    public class UserGameCollection
    {
        /// <summary>
        /// Constructor for Business
        /// </summary>
        /// A constructor that takes paramaters and is used in Controller classes for ease of access
        public UserGameCollection(int userId, int gameId)
        {
            UserId = userId;
            GameId = gameId;
        }
        /// <summary>
        /// Constructor for Migration
        /// </summary>
        /// An empty constructor that is used when migrating the database
        public UserGameCollection()
        {

        }
        /// <summary>
        /// Primary Keys
        /// </summary>
        /// This table has a composite primary key
       
        /// <summary>
        /// Foreign Key connected with table Vehicles
        /// </summary>
        [ForeignKey(nameof(Data.Models.User))] 
        public int UserId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Foreign Key connected with table VideoGames
        /// </summary>
        [ForeignKey(nameof(Data.Models.VideoGame))]
        public int GameId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public VideoGame VideoGame { get; set; }
    }
}