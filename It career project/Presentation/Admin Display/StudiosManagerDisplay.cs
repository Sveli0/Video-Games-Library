using It_career_project.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace It_career_project.Presentation.Admin_Display
{
    class StudiosManagerDisplay
    {
        GameStudioController gsController;

        public StudiosManagerDisplay()
        {
            gsController = new GameStudioController();

            StudioManagerInput();
        }

        private void ShowStudiosManagerMenu()
        {
            string headerAndFooter = new string('-', 40);
            string menuWordDivier = new string(' ', 13);

            Console.WriteLine(headerAndFooter);
            Console.WriteLine(menuWordDivier + "STUDIO_MANAGER" + menuWordDivier);
            Console.WriteLine(headerAndFooter);
            Console.WriteLine("1. Add Studio");
            Console.WriteLine("2. Edit Studio");
            Console.WriteLine("3. Go Back");
        }

        private void StudioManagerInput()
        {
            int input = 0;

            do
            {
                ShowStudiosManagerMenu();

                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        AddStudio();
                        break;

                    case 2:
                        EditStudio();
                        break;

                    case 3:
                        return;

                    default:
                        Console.WriteLine("Invalid option! Select one of the above.");
                        break;
                }
            } while (input != 3);
        }

        private void AddStudio()
        {
            try
            {
                Console.Write("Enter studio name: ");
                string studioName = Console.ReadLine();

                gsController.ValidateStudioName(studioName);

                bool underContract = false;
                int input = 0;

                do
                {
                    Console.WriteLine("Under Contract \n" +
                        "1. Yes \n" +
                        "2. No");

                    input = int.Parse(Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            underContract = true;
                            break;

                        case 2:
                            break;

                        default:
                            Console.WriteLine("Invalid option! Select one of the above.");
                            break;
                    }

                } while (input < 1 || input > 2);

                gsController.AddStudio(studioName, underContract);

                Console.WriteLine("Studio successfully added!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EditStudio()
        {
            try
            {
                Console.Write("Enter the name of the studio you want to edit: ");
                string studioName = Console.ReadLine();
                Console.WriteLine();

                Console.Write("Enter studio name: ");
                string newStudioName = Console.ReadLine();

                gsController.ValidateStudioName(studioName, true);
                gsController.ValidateStudioName(newStudioName, true);

                bool isUnderContract = false;
                int input = 0;

                do
                {
                    Console.WriteLine("Under Contract \n" +
                        "1. Yes \n" +
                        "2. No");

                    input = int.Parse(Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            isUnderContract = true;
                            break;

                        case 2:
                            break;

                        default:
                            Console.WriteLine("Invalid option! Select one of the above.");
                            break;
                    }

                } while (input < 1 || input > 2);

                gsController.EditStudio(studioName, newStudioName, isUnderContract);

                Console.WriteLine("Studio successfully edited!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}