using It_career_project.Business;
using It_career_project.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace It_career_project.Presentation
{
    class UsersManagerDisplay
    {
        UserController uController;

        public UsersManagerDisplay()
        {
            uController = new UserController();

            UserManagerInput();
        }

        private void ShowUserManagerMenu()
        {
            string headerAndFooter = new string('-', 40);
            string menuWordDivier = new string(' ', 14);

            Console.WriteLine(headerAndFooter);
            Console.WriteLine(menuWordDivier + "USER_MANAGER" + menuWordDivier);
            Console.WriteLine(headerAndFooter);
            Console.WriteLine("1. See All Users");
            Console.WriteLine("2. Find User");
            Console.WriteLine("3. Delete User");
            Console.WriteLine("4. Go Back");
        }

        private void UserManagerInput()
        {
            int input = 0;

            do
            {
                ShowUserManagerMenu();

                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        SeeAllUsers();
                        break;

                    case 2:
                        FindUser();
                        break;

                    case 3:
                        DeleteUser();
                        break;

                    case 4:
                        return;

                    default:
                        Console.WriteLine("Invalid option! Select one of the above.");
                        break;
                }
            } while (input != 4);
        }

        private void SeeAllUsers()
        {
            try
            {
                List<string> listWithUsersInfo = uController.GetInfoOfAllUsers();

                Console.WriteLine(new string('-', 40) + "\n");

                foreach (string userInfo in listWithUsersInfo)
                {
                    Console.WriteLine(userInfo);
                }

                Console.WriteLine("\n" + new string('-', 40) + "\n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FindUser()
        {
            try
            {
                Console.Write("Enter the username or the email of the user you want to find: ");
                string input = Console.ReadLine();
                string userInfo = uController.GetUserInfoByUsernameOrEmail(input);
                Console.WriteLine(userInfo);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DeleteUser()
        {
            try
            {
                Console.Write("Enter the username or the email of the user you want to delete: ");
                string usernameOrEmailInput = Console.ReadLine();
                string userInfo = uController.GetUserInfoByUsernameOrEmail(usernameOrEmailInput);
                Console.WriteLine(userInfo);

                int input = 0;

                do
                {
                    Console.WriteLine("Are you sure you want to delete this user? \n" +
                    "1. Yes\n" +
                    "2. No");

                    input = int.Parse(Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            uController.DeleteUser(usernameOrEmailInput);
                            Console.WriteLine("User successfully deleted!");
                            break;

                        case 2:
                            //Empty - Leaves
                            break;

                        default:
                            Console.WriteLine("Invalid option! Select one of the above.");
                            break;
                    }
                } while (input < 1 || input > 2);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}