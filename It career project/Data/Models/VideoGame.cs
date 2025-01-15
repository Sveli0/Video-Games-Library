using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace It_career_project.Data.Models
{
    public class VideoGame
    {
        /// <summary>
        /// Constructor for Business
        /// </summary>
        /// A constructor that takes paramaters and is used in Controller classes for ease of access
        public VideoGame(string gameTitle, decimal price, int studioId,int genreId, bool isAvailable = true)
        {
            GameTitle = gameTitle;
            Price = price;
            StudioId = studioId;
            GenreId = genreId;
            IsAvailable = isAvailable;
            UserGameCollections = new HashSet<UserGameCollection>();
            Reviews = new HashSet<Review>();
            Sales = new HashSet<Sale>();
        }
        /// <summary>
        /// Constructor for Migration
        /// </summary>
        /// An empty constructor that is used when migrating the database
        public VideoGame()
        {
            UserGameCollections = new HashSet<UserGameCollection>();
            Reviews = new HashSet<Review>();
            Sales = new HashSet<Sale>();
        }
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        [Required]
        public string GameTitle { get; set; }

        [Required]
        public decimal Price { get; set; }
        /// <summary>
        /// Foreign Key connected with table Studio
        /// </summary>
        [ForeignKey(nameof(Data.Models.GameStudio))]
        [Required]
        public int StudioId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public GameStudio GameStudio { get; set; }
        /// <summary>
        /// Foreign Key connected with table Genre
        /// </summary>
        [ForeignKey(nameof(Data.Models.Genre))]
        [Required]
        public int GenreId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public Genre Genre { get; set; }

        [DefaultValue("true")]
        public bool IsAvailable { get; set; } = true;

        public ICollection<UserGameCollection> UserGameCollections { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Sale> Sales { get; set; }

    }
}