using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace It_career_project.Data.Models
{
    public class GameStudio
    {
        /// <summary>
        /// Constructor for Business
        /// </summary>
        /// A constructor that is used when creating object of this class in the controllers for ease of access
        public GameStudio(string name, bool underContract=false)
        {
            Name = name;
            UnderContract = underContract;
            VideoGames = new HashSet<VideoGame>();

        }
        /// <summary>
        /// Constructor for Migration
        /// </summary>
        /// An empty constructor that is used when migrating
        public GameStudio()
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

        public bool UnderContract { get; set; }

        public ICollection<VideoGame> VideoGames { get; set; }
    }
}