using It_career_project.Business;
using It_career_project.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace It_career_project.Presentation
{
    class ReviewsDisplay
    {
        User loggedInUser;

        ReviewController rController;
        UserGameCollectionController ugcController;
        VideoGameController vgController;

        public ReviewsDisplay(User loggedInUser)
        {
            this.loggedInUser = loggedInUser;
            rController = new ReviewController();
            ugcController = new UserGameCollectionController();
            vgController = new VideoGameController();

            ReviewMenuInput();
        }


        //MENU AND INPUT

        private void ShowReviewMenu()
        {
            string headerAndFooter = new string('-', 40);
            string menuWordDivider = new string(' ', 15);

            Console.WriteLine(headerAndFooter);
            Console.WriteLine(menuWordDivider + "REVIEW_MENU" + menuWordDivider);
            Console.WriteLine(headerAndFooter);
            Console.WriteLine("1. Add Review");
            Console.WriteLine("2. Delete Review");
            Console.WriteLine("3. See Your Reviews");
            Console.WriteLine("4. See Game Reviews");
            Console.WriteLine("5. Go Back");
        }

        private void ReviewMenuInput()
        {
            int input = 0;

            do
            {
                ShowReviewMenu();

                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        AddReview();
                        break;

                    case 2:
                        DeleteReview();
                        break;

                    case 3:
                        SeeYourReviews();
                        break;

                    case 4:
                        SeeGameReviews();
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid option! Select one of the above.");
                        break;
                }
            } while (input != 5);
        }


        //METHODS

        private void AddReview()
        {
            try
            {
                ShowGamesLibrary();

                Console.Write("Which game do you want to leave a review for: ");
                string gameTitleInput = Console.ReadLine();

                vgController.ValidateGameTitle(loggedInUser, gameTitleInput);
                rController.UserHasReviewFor(loggedInUser, gameTitleInput);

                int posNegInput;
                bool isPositive = true;

                do
                {
                    Console.WriteLine("1. Positive");
                    Console.WriteLine("2. Negative");

                    posNegInput = int.Parse(Console.ReadLine());

                    switch (posNegInput)
                    {
                        case 1:
                            isPositive = true;
                            break;

                        case 2:
                            isPositive = false;
                            break;

                        default:
                            Console.WriteLine("Invalid option! Select one of the above.");
                            break;
                    }
                } while (posNegInput < 1 || posNegInput > 2);

                Console.WriteLine("|Write your review|");
                string review = Console.ReadLine();

                rController.AddReview(loggedInUser, gameTitleInput, review, isPositive);
                Console.WriteLine("Review successfully added!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DeleteReview()
        {
            try
            {
                List<string> gamesReviewsStringList = rController.GetShortInfoOfAllUserReviews(loggedInUser);

                Console.WriteLine(new string('-', 40));

                foreach (String gameReview in gamesReviewsStringList)
                {
                    Console.WriteLine(gameReview);
                }

                Console.WriteLine(new string('-', 40));

                Console.Write("Enter the game title of the review you want deleted: ");
                string gameTitleInput = Console.ReadLine();

                vgController.ValidateGameTitle(loggedInUser, gameTitleInput);

                rController.DeleteReview(loggedInUser, gameTitleInput);
                Console.WriteLine("Your review has been successfully deleted!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SeeYourReviews()
        {
            try
            {
                List<string> gamesReviewsStringList = rController.GetShortInfoOfAllUserReviews(loggedInUser);

                Console.WriteLine(new string('-', 40));

                foreach (String gameReview in gamesReviewsStringList)
                {
                    Console.WriteLine(gameReview);
                }

                Console.WriteLine(new string('-', 40));

                Console.Write("Enter the game title of the review you want to see: ");
                string gameTitleInput = Console.ReadLine();

                vgController.ValidateGameTitle(loggedInUser, gameTitleInput);

                String review = rController.GetUserReviewInfoOfSpecificGame(loggedInUser, gameTitleInput);
                Console.WriteLine();
                Console.WriteLine(review);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SeeGameReviews()
        {
            try
            {
                Console.Write("Which game do you want to see reviews of: ");
                string gameTitleInput = Console.ReadLine();

                bool hasToOwnGame = false;
                vgController.ValidateGameTitle(loggedInUser, gameTitleInput, hasToOwnGame);

                List<string> reviewList = rController.GetFirstFiveReviewsOfAGameByTitle(gameTitleInput);

                Console.WriteLine(new string('-', 40));
                Console.WriteLine();

                foreach (String review in reviewList)
                {
                    Console.WriteLine(review);
                }

                Console.Write(new string('-', 40) + "\n \n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //NOT IN MENU OPTIONS

        private void ShowGamesLibrary()
        {
            List<string> vgList = ugcController.GetGameInfoCollectionByUser(loggedInUser);

            Console.WriteLine(new string('-', 40));

            foreach (string gameInfo in vgList)
            {
                Console.WriteLine(gameInfo);
            }

            Console.WriteLine(new string('-', 40));
        }
    }
}