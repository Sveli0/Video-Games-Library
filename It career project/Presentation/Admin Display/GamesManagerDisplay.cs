using It_career_project.Business;
using It_career_project.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace It_career_project.Presentation.Admin_Display
{
    class GamesManagerDisplay
    {
        VideoGameController vgController;

        public GamesManagerDisplay()
        {
            vgController = new VideoGameController();

            GamesManagerInput();
        }

        private void ShowGamesManagerMenu()
        {
            string headerAndFooter = new string('-', 40);
            string menuWordDivier = new string(' ', 14);

            Console.WriteLine(headerAndFooter);
            Console.WriteLine(menuWordDivier + "GAMES_MANAGER" + menuWordDivier);
            Console.WriteLine(headerAndFooter);
            Console.WriteLine("1. Find Game");
            Console.WriteLine("2. Add Game");
            Console.WriteLine("3. Remove Game");
            Console.WriteLine("4. Edit Game");
            Console.WriteLine("5. Go Back");
        }

        private void GamesManagerInput()
        {
            int input = 0;

            do
            {
                ShowGamesManagerMenu();

                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        FindGame();
                        break;

                    case 2:
                        AddGame();
                        break;

                    case 3:
                        RemoveGame();
                        break;

                    case 4:
                        EditGame();
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid option! Select one of the above.");
                        break;
                }
            } while (input != 5);
        }

        private void FindGame()
        {
            try
            {
                Console.Write("Enter game title: ");
                string gameTitle = Console.ReadLine();

                string gameInfo = vgController.GetGameInfoByTitle(gameTitle);
                Console.WriteLine(gameInfo);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddGame()
        {
            try
            {
                Console.Write("Enter game title: ");
                string gameTitle = Console.ReadLine();
                Console.Write("Enter game studio: ");
                string gameStudio = Console.ReadLine();
                Console.Write("Enter game price: ");
                decimal gamePrice = decimal.Parse(Console.ReadLine());
                Console.Write("Enter game genre: ");
                string gameGenre = Console.ReadLine();

                vgController.AddGame(gameTitle, gamePrice, gameStudio, gameGenre);
                Console.WriteLine("Game successfully added!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RemoveGame()
        {
            try
            {
                Console.Write("Which game do you want to remove: ");
                string gameTitle = Console.ReadLine();
                vgController.MakeGameUnavailable(gameTitle);
                Console.WriteLine("Game successfully removed from store!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EditGame()
        {
            try
            {
                Console.Write("Enter game title of the game you want to edit: ");
                string gameTitle = Console.ReadLine();
                Console.WriteLine();

                Console.Write("Enter game title: ");
                string newGameTitle = Console.ReadLine();
                Console.Write("Enter game studio: ");
                string gameStudio = Console.ReadLine();
                Console.Write("Enter game price: ");
                decimal gamePrice = decimal.Parse(Console.ReadLine());
                Console.Write("Enter game genre: ");
                string gameGenre = Console.ReadLine();

                vgController.EditGame(gameTitle, newGameTitle, gamePrice, gameStudio, gameGenre);
                Console.WriteLine("Game successfully edited!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}