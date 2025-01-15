using System;
using System.Collections.Generic;
using It_career_project.Data;
using It_career_project.Data.Models;
using System.Linq;

namespace It_career_project.Business
{
    public class UserGameCollectionController
    {
        private VideoGamePlatformContext context;

        public UserGameCollectionController()
        {
            context = new VideoGamePlatformContext();
        }

        public UserGameCollectionController(VideoGamePlatformContext context)
        {
            this.context = context;
        }

        public List<VideoGame> GetGameCollectionByUser(User user)
        {
            int userId = user.Id;

            int[] userGameIds =
                context
                .UsersGameCollections
                .Where(x => x.UserId == userId)
                .Select(x => x.GameId)
                .ToArray();

            List<VideoGame> videoGames =
                context
                .VideoGames
                .Where(x => userGameIds.Contains(x.Id))
                .ToList();

            if (videoGames.Count == 0)
            {
                throw new ArgumentException("You don't have any games in your library!");
            }

            return videoGames;
        }

        public List<string> GetGameInfoCollectionByUser(User user)
        {
            GenreController gController = new GenreController(context);
            GameStudioController gsController = new GameStudioController(context);
            List<VideoGame> videoGames = GetGameCollectionByUser(user);

            List<string> GameInfo = new List<string>();

            foreach (VideoGame videoGame in videoGames)
            {
                Genre selectedGameGenre = gController.GetGenreById(videoGame.GenreId);
                GameStudio selectedGameStudio = gsController.GetStudioById(videoGame.StudioId);
                GameInfo.Add($"{videoGame.GameTitle} - {selectedGameGenre.Name}, {selectedGameStudio.Name}");
            }

            return GameInfo;
        }

        public bool UserOwnsGame(User user, string gameTitle)
        {
            List<VideoGame> userLibrary = GetGameCollectionByUser(user);
            bool isGameOwned = userLibrary.Exists(x => x.GameTitle == gameTitle);

            return isGameOwned;
        }
    }
}