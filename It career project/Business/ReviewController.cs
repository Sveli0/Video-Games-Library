using It_career_project.Data.Models;
using It_career_project.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace It_career_project.Business
{
    /// <summary>
    /// Business logic for the table Reviews
    /// </summary>
    public class ReviewController
    {
        private VideoGamePlatformContext context;

        /// <summary>
        /// Constructor used in Presentation layer
        /// </summary>
        public ReviewController()
        {
            context = new VideoGamePlatformContext();
        }

        /// <summary>
        /// Constructer used in tests
        /// </summary>
        public ReviewController(VideoGamePlatformContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Shows all reviews of a specified user
        /// </summary>
        /// <returns>Returns a collection of strings composed of all the details
        /// of the reviews of a user</returns>
        public List<string> GetShortInfoOfAllUserReviews(User user)
        {
            List<Review> reviewsOfUser = GetUserReviews(user);
            List<string> shortReviewInfoCollection = new List<string>();

            foreach (Review review in reviewsOfUser)
            {
                string positiveOrNegative = "Negative";

                VideoGame videoGameOfReview =
                    context
                    .VideoGames
                    .FirstOrDefault(x => x.Id == review.GameId);

                if (review.IsPositive)
                {
                    positiveOrNegative = "Positivie";
                }

                string shortInfoOfReview = $"{videoGameOfReview.GameTitle} - {positiveOrNegative}";
                shortReviewInfoCollection.Add(shortInfoOfReview);
            }

            return shortReviewInfoCollection;
        }

        /// <summary>
        /// Gets a specific review of a specific user and game title
        /// </summary>
        /// <param name="user">user is used to get the userId of the review</param>
        /// <param name="gameTitle">is used to find the game with that title and then get 
        /// its id to use for the sales gameId</param>
        /// <returns>The string of the specific review</returns>
        public string GetUserReviewInfoOfSpecificGame(User user, string gameTitle)
        {
            Review userReviewOfGame =
                GetReviewsByGameTitle(gameTitle)
                .FirstOrDefault(x => x.UserId == user.Id);

            if (userReviewOfGame == null)
            {
                throw new ArgumentException("You have not written a review for this game!");
            }

            string positiveOrNegative = "Negative";

            if (userReviewOfGame.IsPositive)
            {
                positiveOrNegative = "Positive";
            }

            string reviewText = $"Review By {user.Username} for Game: {gameTitle} \n" +
                $"Type: {positiveOrNegative} \n" +
                $"{userReviewOfGame.ReviewText}\n";

            return reviewText;
        }

        /// <summary>
        /// Deletes a specific review given the user and game title
        /// </summary>
        /// <param name="user">user, whose review is being deleted</param>
        /// <param name="gameTitle">is used to find the game with that title and then get 
        /// its id to use for the sales gameId</param>
        public void DeleteReview(User user, string gameTitle)
        {
            VideoGameController vgController = new VideoGameController(context);
            VideoGame videoGame = vgController.GetGameByTitle(gameTitle);

            Review reviewToBeDeleted =
                context.Reviews
                .FirstOrDefault(x => x.GameId == videoGame.Id && x.UserId == user.Id);

            if (reviewToBeDeleted == null)
            {
                throw new ArgumentException("You have not written a review for this game!");
            }

            context.Remove(reviewToBeDeleted);
            context.SaveChanges();
        }

        /// <summary>
        /// Adds a review given all of it's details 
        /// </summary>
        /// <param name="user">User is used for userId</param>
        /// <param name="gameTitle">is used to find the game with that title and then get 
        /// its id to use for the sales gameId</param>
        /// <param name="reviewText">the text of a review</param>
        /// <param name="isPositive">true or false if it's positive or negative</param>
        public void AddReview(User user, string gameTitle, string reviewText, bool isPositive)
        {
            VideoGameController vgController = new VideoGameController(context);
            VideoGame game = vgController.GetGameByTitle(gameTitle);

            context.Add(new Review(user.Id, game.Id, reviewText, isPositive));
            context.SaveChanges();
        }

        /// <summary>
        /// Gets The String of all Reviews after being given a game title
        /// </summary>
        /// <param name="gameTitle">is used to find the game with that title and then get 
        /// its id to use for the sales gameId</param>
        /// <returns>A collection of the strings of the reviews</returns>
        public List<string> GetReviewInfoByGameTitle(string gameTitle)
        {
            List<Review> reviewsOfGame = GetReviewsByGameTitle(gameTitle);
            List<string> reviewsInfoOfGame = new List<string>();

            foreach (Review review in reviewsOfGame)
            {
                User reviewingUser =
                    context
                    .Users
                    .FirstOrDefault(x => x.Id == review.UserId);

                string reviewText;
                string posNeg = "Negative";

                if (review.IsPositive)
                {
                    posNeg = "Positive";
                }

                reviewText = $"Review By [{reviewingUser.Username}] for Game: {gameTitle}\n" +
                $"Type: {posNeg} \n" +
                $"{review.ReviewText} \n";

                reviewsInfoOfGame.Add(reviewText);
            }

            return reviewsInfoOfGame;
        }

        /// <summary>
        /// Gives the first five reviews of a game
        /// </summary>
        /// <param name="gameTitle">is used to find the game with that title and then get 
        /// its id to use for the sales gameId</param>
        /// <returns>A collection of the five strings</returns>
        public List<string> GetFirstFiveReviewsOfAGameByTitle(string gameTitle)
        {
            List<string> topFiveReviews =
                GetReviewInfoByGameTitle(gameTitle)
                .Take(5)
                .ToList();

            return topFiveReviews;
        }

        /// <summary>
        /// Returns a List of Reviews after being given the game title they are for
        /// </summary>
        /// <param name="gameTitle">is used to find the game with that title and then get 
        /// its id to use for the sales gameId</param>
        /// <returns></returns>
        public List<Review> GetReviewsByGameTitle(string gameTitle)
        {
            VideoGameController vgController = new VideoGameController(context);
            VideoGame videoGame = vgController.GetGameByTitle(gameTitle);

            List<Review> reviewsOfGame =
                context
                .Reviews
                .ToList()
                .Where(x => x.GameId == videoGame.Id)
                .ToList();

            return reviewsOfGame;
        }

        public void UserHasReviewFor(User user, string gameTitle)
        {
            VideoGameController vgController = new VideoGameController(context);
            VideoGame toBeReviewedGame = vgController.GetGameByTitle(gameTitle);

            List<Review> reviewsOfUser =
                context
                .Reviews
                .ToList()
                .Where(x => x.UserId == user.Id)
                .ToList();

            if (reviewsOfUser.Exists(x => x.GameId == toBeReviewedGame.Id))
            {
                throw new ArgumentException("You have already reviewed this game!");
            }
        }

        public List<Review> GetUserReviews(User user)
        {
            List<Review> reviewsOfUser =
                context
                .Reviews
                .ToList()
                .Where(x => x.UserId == user.Id)
                .ToList();

            if (reviewsOfUser.Count == 0)
            {
                throw new ArgumentException("You have not written any reviews!");
            }

            return reviewsOfUser;
        }
    }
}