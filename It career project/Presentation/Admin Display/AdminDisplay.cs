using It_career_project.Business;
using It_career_project.Data.Models;
using It_career_project.Presentation.Admin_Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace It_career_project.Presentation
{
    class AdminDisplay
    {
        Admin loggedInAdmin;

        GiftCardController gcController;

        public AdminDisplay(Admin loggedInAdmin)
        {
            this.loggedInAdmin = loggedInAdmin;
            gcController = new GiftCardController();

            AdminMenuInput();
        }

        
        //MENU AND INPUT

        private void ShowAdminMenu()
        {
            string adminFirstName = loggedInAdmin.FirstName;
            string adminLastName = loggedInAdmin.LastName;
            string headerAndFooter = new string('-', 40);
            string menuWordDivider = new string(' ', 15);

            Console.WriteLine(headerAndFooter);
            Console.WriteLine(menuWordDivider + "ADMIN_MENU" + menuWordDivider);
            Console.WriteLine(headerAndFooter);
            Console.WriteLine($"Welcome {adminFirstName} {adminLastName}! \n");
            Console.WriteLine("1. Games Manager");
            Console.WriteLine("2. Users Manager");
            Console.WriteLine("3. Studios Manager");
            Console.WriteLine("4. Sales Manager");
            Console.WriteLine("5. Generate Gift Cards");
            Console.WriteLine("6. Log Out");
        }

        private void AdminMenuInput()
        {
            int input = 0;

            do
            {
                ShowAdminMenu();

                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        GamesManager();
                        break;

                    case 2:
                        UserManager();
                        break;

                    case 3:
                        StudiosManager();
                        break;

                    case 4:
                        SalesManager();
                        break;

                    case 5:
                        GenerateGiftCards();
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

        private void GamesManager()
        {
            GamesManagerDisplay gamesManager = new GamesManagerDisplay();
        }

        private void UserManager()
        {
            UsersManagerDisplay usersManager = new UsersManagerDisplay();
        }

        private void StudiosManager()
        {
            StudiosManagerDisplay studiosManager = new StudiosManagerDisplay();
        }

        private void SalesManager()
        {
            SalesManagerDisplay salesManager = new SalesManagerDisplay();
        }

        private void GenerateGiftCards()
        {
            gcController.GenerateRandomGiftCards();
            Console.WriteLine("Operation successful!");
        }
    }
}