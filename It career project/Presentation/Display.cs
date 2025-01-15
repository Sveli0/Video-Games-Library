using It_career_project.Data.Models;
using It_career_project.Business;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using It_career_project.Presentation;

namespace It_career_project
{
    class Display
    {
        UserController uController;
        AdminController aController;

        public Display()
        {
            uController = new UserController();
            aController = new AdminController();

            StartMenuInput();
        }


        //MENU AND INPUT

        private void ShowStartMenu()
        {
            string headerAndFooter = new string('-', 40);
            string menuWordDivider = new string(' ', 18);

            Console.WriteLine(headerAndFooter);
            Console.WriteLine(menuWordDivider + "MENU" + menuWordDivider);
            Console.WriteLine(headerAndFooter);
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Admin Login");
            Console.WriteLine("4. Stop");
        }

        private void StartMenuInput()
        {
            int input = 0;

            do
            {
                ShowStartMenu();

                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        Login();
                        break;

                    case 2:
                        Register();
                        break;

                    case 3:
                        AdminLogin();
                        break;

                    case 4:
                        return;

                    default:
                        Console.WriteLine("Invalid option! Select one of the above.");
                        break;
                }
            } while (input != 4);
        }


        //METHODS

        private void Login()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            try
            {
                uController.LoginAttempt(username, password);
                Console.WriteLine("Login Successful!");

                User loggedInUser = uController.GetUserByUsername(username);
                UserDisplay userDisplay = new UserDisplay(loggedInUser);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Register()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Country: ");
            string country = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            try
            {
                uController.Register(username, password, firstName, lastName, country, email);
                Console.WriteLine("Registration Completed!");

                User loggedInUser = uController.GetUserByUsername(username);
                UserDisplay userDisplay = new UserDisplay(loggedInUser);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AdminLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password One: ");
            string passwordOne = Console.ReadLine();
            Console.Write("Password Two: ");
            string passwordTwo = Console.ReadLine();

            if (aController.LoginAttempt(username, passwordOne, passwordTwo))
            {
                Console.WriteLine("Login Successful!");

                Admin loggedInAdmin = aController.GetAdminByUsername(username);
                AdminDisplay adminDisplay = new AdminDisplay(loggedInAdmin);
            }
            else
            {
                Console.WriteLine("Invalid user or password!");
                StartMenuInput();
            }
        }
    }
}