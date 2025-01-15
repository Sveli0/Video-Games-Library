using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace It_career_project.Data.Models
{
    public class Genre
    {
        /// <summary>
        /// Constructor for Business
        /// </summary>
        /// A constructor that takes paramaters and is used in Controller classes for ease of access
        public Genre(string name)
        {
            Name = name;
            VideoGames = new HashSet<VideoGame>();
        }
        /// <summary>
        /// Constructor for Migration
        /// </summary>
        /// An empty constructor that is used when migrating the database
        public Genre()
        {
            VideoGames = new HashSet<VideoGame>();
        }
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public ICollection<VideoGame> VideoGames { get; set; }
    }
}