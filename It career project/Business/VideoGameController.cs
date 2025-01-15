using It_career_project.Data;
using It_career_project.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace It_career_project.Business
{
    public class VideoGameController
    {
        private VideoGamePlatformContext context;

        public VideoGameController()
        {
            context = new VideoGamePlatformContext();
        }

        public VideoGameController(VideoGamePlatformContext context)
        {
            this.context = context;
        }

        public void EditGame(string oldGameTitle, string newGameTitle, decimal newPrice, string newStudioName, string newGenreName)
        {
            if (string.IsNullOrEmpty(oldGameTitle))
            {
                throw new ArgumentException("Game title cannot be empty!");
            }

            if (string.IsNullOrEmpty(newGameTitle))
            {
                throw new ArgumentException("Game title cannot be empty!");
            }

            if (string.IsNullOrEmpty(newStudioName))
            {
                throw new ArgumentException("Studio name cannot be empty!");
            }

            if (string.IsNullOrEmpty(newGenreName))
            {
                throw new ArgumentException("Genre cannot be empty!");
            }

            VideoGame videoGameToBeChanged = GetGameByTitle(oldGameTitle);

            ChangeGameGameStudio(oldGameTitle, newStudioName);

            if (newPrice < 0)
            {
                throw new ArgumentException("Price cannot be negative!");
            }

            ChangeGameGenre(oldGameTitle, newGenreName);

            if (oldGameTitle != newGameTitle)
            {
                ChangeGameTitle(oldGameTitle, newGameTitle);
            }

            decimal oldGamePrice = videoGameToBeChanged.Price;

            if (oldGamePrice != newPrice)
            {
                ChangeGamePrice(newGameTitle, newPrice);
            }
        }

        public void ChangeGameGameStudio(string gameTitle, string studioName)
        {
            GameStudioController gsController = new GameStudioController(context);
            VideoGame videoGameToBeChanged = GetGameByTitle(gameTitle);

            GameStudio studio = gsController.GetStudioByName(studioName);
            videoGameToBeChanged.StudioId = studio.Id;

            context.Update(videoGameToBeChanged);
            context.SaveChanges();
        }

        public void ChangeGameGenre(string gameTitle, string genreName)
        {
            GenreController gController = new GenreController(context);
            VideoGame videoGameToBeChanged = GetGameByTitle(gameTitle);

            Genre newGenre = gController.GetGenreByName(genreName);
            videoGameToBeChanged.GenreId = newGenre.Id;

            context.Update(videoGameToBeChanged);
            context.SaveChanges();
        }

        public void ChangeGamePrice(string gameTitle, decimal newPrice)
        {
            SaleController sController = new SaleController();

            VideoGame videoGameToBeChanged = GetGameByTitle(gameTitle);

            videoGameToBeChanged.Price = newPrice;

            Sale saleOfGame = sController.GetSaleByGameId(videoGameToBeChanged.Id);

            context.Update(videoGameToBeChanged);
            context.SaveChanges();

            if (saleOfGame != null)
            {
                sController.MakeSaleDiscountEffective(saleOfGame);
            }
        }

        public void ChangeGameTitle(string oldGameTitle, string newGameTitle)
        {
            VideoGame videoGameToBeChanged = GetGameByTitle(oldGameTitle);

            videoGameToBeChanged.GameTitle = newGameTitle;

            context.Update(videoGameToBeChanged);
            context.SaveChanges();
        }

        public void AddGame(string gameTitle, decimal price, string studioName, string genre)
        {
            if (string.IsNullOrEmpty(gameTitle))
            {
                throw new ArgumentException("Game title cannot be empty!");
            }

            if (string.IsNullOrEmpty(studioName))
            {
                throw new ArgumentException("Studio name cannot be empty!");
            }

            if (string.IsNullOrEmpty(genre))
            {
                throw new ArgumentException("Genre cannot be empty!");
            }

            VideoGame sameTitleGame =
                context
                .VideoGames
                .FirstOrDefault(x => x.GameTitle == gameTitle);

            if (sameTitleGame != null)
            {
                throw new ArgumentException("A game with this title already exists!");
            }

            GameStudioController gsController = new GameStudioController(context);
            GenreController gController = new GenreController(context);

            int studioId = gsController.GetStudioByName(studioName).Id;

            if (price < 0)
            {
                throw new ArgumentException("Price cannot be negative!");
            }

            int genreId = gController.GetGenreByName(genre).Id;

            context.VideoGames.Add(new VideoGame(gameTitle, price, studioId, genreId));
            context.SaveChanges();
        }

        public VideoGame GetGameById(int id)
        {
            VideoGame game =
                context
                .VideoGames
                .FirstOrDefault(x => x.Id == id);

            return game;
        }

        public decimal GetGamePriceByTitle(string title, User user)
        {
            CountryController cController = new CountryController(context);
            VideoGame game = GetGameByTitle(title);

            Country userCountry = cController.GetCountryByCountryCode(user.CountryCode);

            return game.Price / userCountry.CurrencyExchangeRateToEuro;
        }

        public VideoGame GetGameByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Game title cannot be empty!");
            }

            VideoGame game =
                context
                .VideoGames
                .FirstOrDefault(x => x.GameTitle == title);

            if (game == null)
            {
                throw new ArgumentException("Game does not exist!");
            }

            return game;
        }

        public void ValidateGameTitle(User user, string gameTitle, bool hasToOwnGame = true)
        {
            UserGameCollectionController ugcController = new UserGameCollectionController(context);

            if (string.IsNullOrEmpty(gameTitle))
            {
                throw new ArgumentException("Game title cannot be empty!");
            }

            VideoGame game =
                context
                .VideoGames
                .FirstOrDefault(x => x.GameTitle == gameTitle);

            if (game == null)
            {
                throw new ArgumentException("Game does not exist!");
            }

            if (hasToOwnGame && !ugcController.UserOwnsGame(user, gameTitle))
            {
                throw new ArgumentException("You do not own this game!");
            }
        }

        public List<string> GetGamesStartingWithLetter(User user, char letter)
        {
            CountryController cController = new CountryController(context);
            Country userCountry = cController.GetCountryByCountryCode(user.CountryCode);

            List<VideoGame> gamesWithLetter =
                context
                .VideoGames
                .ToList()
                .Where(x => x.GameTitle.First() == letter)
                .ToList();

            List<string> gamesStartingWithLetterStrings = new List<string>();

            if (gamesWithLetter.Count == 0)
            {
                throw new ArgumentException("There are no games starting with this letter!");
            }

            foreach (VideoGame videoGame in gamesWithLetter)
            {
                if (videoGame.IsAvailable)
                {
                    GenreController gController = new GenreController(context);
                    GameStudioController gsController = new GameStudioController(context);
                    Genre selectedGameGenre = gController.GetGenreById(videoGame.GenreId);
                    GameStudio selectedGameStudio = gsController.GetStudioById(videoGame.StudioId);

                    gamesStartingWithLetterStrings.Add($"{videoGame.GameTitle} - {selectedGameGenre.Name}, {selectedGameStudio.Name}, " +
                        $"{videoGame.Price / userCountry.CurrencyExchangeRateToEuro:f2} {userCountry.Currency}");
                }
            }

            return gamesStartingWithLetterStrings;
        }

        public void MakeGameUnavailable(string gameTitle)
        {
            VideoGame videoGameToBeGutted = GetGameByTitle(gameTitle);
            videoGameToBeGutted.IsAvailable = false;

            context.Update(videoGameToBeGutted);
            context.SaveChanges();
        }

        public string GetGameInfoByTitle(string gameTitle)
        {
            GenreController gController = new GenreController(context);
            GameStudioController gsController = new GameStudioController(context);

            VideoGame queryVideoGame = GetGameByTitle(gameTitle);
            GameStudio gameStudio = gsController.GetStudioById(queryVideoGame.StudioId);
            Genre gameGenre = gController.GetGenreById(queryVideoGame.GenreId);

            string gameInfo = $"{queryVideoGame.GameTitle} - {queryVideoGame.Price:f2} EUR, {gameStudio.Name}, {gameGenre.Name}";

            return gameInfo;
        }
    }
}