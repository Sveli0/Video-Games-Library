using It_career_project.Business;
using It_career_project.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace It_career_project.Presentation
{
    class UserDisplay
    {
        User loggedInUser;

        CountryController cController;
        UserController uController;
        UserGameCollectionController ugcController;
        VideoGameController vgController;
        GiftCardController gcController;

        public UserDisplay(User loggedInUser)
        {
            this.loggedInUser = loggedInUser;
            cController = new CountryController();
            uController = new UserController();
            ugcController = new UserGameCollectionController();
            vgController = new VideoGameController();
            gcController = new GiftCardController();

            UserMenuInput();
        }


        //MENU AND INPUT

        private void ShowUserMenu()
        {
            string userFirstName = loggedInUser.FirstName;
            string userLastName = loggedInUser.LastName;
            decimal userBalance = loggedInUser.Balance;
            string userCurrency = cController.GetCountryByCountryCode(loggedInUser.CountryCode).Currency;

            string headerAndFooter = new string('-', 40);
            string menuWordDivier = new string(' ', 16);

            Console.WriteLine(headerAndFooter);
            Console.WriteLine(menuWordDivier + "USER_MENU" + menuWordDivier);
            Console.WriteLine(headerAndFooter);
            Console.WriteLine($"Welcome {userFirstName} {userLastName}!");
            Console.WriteLine($"Your Balance is: {userBalance:F2} {userCurrency} \n");
            Console.WriteLine("1. Show Games Library");
            Console.WriteLine("2. Browse Games");
            Console.WriteLine("3. Add Balance");
            Console.WriteLine("4. Review Menu");
            Console.WriteLine("5. Find User");
            Console.WriteLine("6. Log Out");
        }

        private void UserMenuInput()
        {
            int input = 0;

            do
            {
                ShowUserMenu();

                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        ShowGamesLibrary();
                        break;

                    case 2:
                        BrowseGames();
                        break;

                    case 3:
                        AddBalance();
                        break;

                    case 4:
                        ReviewMenu();
                        break;

                    case 5:
                        FindUser();
                        break;

                    case 6:
                        Console.WriteLine("Successfully logged out!");
                        return;

                    default:
                        Console.WriteLine("Invalid option! Select one of the above.");
                        break;
                }
            } while (input != 6);
        }


        //METHODS

        private void ShowGamesLibrary()
        {
            try
            {
                List<string> vgList = ugcController.GetGameInfoCollectionByUser(loggedInUser);

                Console.WriteLine(new string('-', 40));

                foreach (string gameInfo in vgList)
                {
                    Console.WriteLine(gameInfo);
                }

                Console.WriteLine(new string('-', 40) + "\n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BrowseGames()
        {
            try
            {
                string symbolAssortmentString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

                Console.Write("Enter the first letter of your game (A-Z): ");
                char letterInput = char.Parse(Console.ReadLine().ToUpper());

                while (!symbolAssortmentString.Contains(letterInput))
                {
                    Console.WriteLine($"\"{letterInput}\" is not (A-Z)!");
                    Console.Write("Enter the first letter of your game (A-Z): ");
                    letterInput = char.Parse(Console.ReadLine().ToUpper());
                }

                List<string> vgList = vgController.GetGamesStartingWithLetter(loggedInUser, letterInput);

                Console.WriteLine(new string('-', 40));

                foreach (string gameInfo in vgList)
                {
                    Console.WriteLine(gameInfo);
                }

                Console.WriteLine(new string('-', 40));

                BuyGame(); //Ask if user wants to buy a game
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddBalance()
        {
            Console.Write("Enter your gift card code: ");
            string code = Console.ReadLine();
            decimal giftCardValue;

            try
            {
                giftCardValue = gcController.RedeemGiftCard(code);
                uController.AddBalanceInUserCurrency(loggedInUser, giftCardValue);

                Console.WriteLine("Balance successfully added!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ReviewMenu()
        {
            ReviewsDisplay reviewsDisplay = new ReviewsDisplay(loggedInUser);
        }

        private void FindUser()
        {
            Console.Write("Enter user's username: ");
            string username = Console.ReadLine();

            try
            {
                Console.WriteLine(uController.GetUserInfoByUsername(username));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //NOT IN MENU OPTIONS

        private void BuyGame()
        {
            int buyGameInput = 0;

            do
            {
                Console.WriteLine("Do you want to buy a game? \n" +
                "1. Yes \n" +
                "2. No");

                buyGameInput = int.Parse(Console.ReadLine());

                switch (buyGameInput)
                {
                    case 1:
                        Console.Write("Which game do you want to buy: ");
                        string gameTitleInput = Console.ReadLine();

                        try
                        {
                            VideoGame videoGame = vgController.GetGameByTitle(gameTitleInput);
                            uController.UserTriesToBuyGame(loggedInUser, videoGame);
                            Console.WriteLine("Game successfully bought!");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                            buyGameInput = 0;
                        }
                        break;

                    case 2:
                        return;

                    default:
                        Console.WriteLine("Invalid option! Select one of the above.");
                        break;
                }
            } while (buyGameInput < 1 || buyGameInput > 2);
        }
    }
}