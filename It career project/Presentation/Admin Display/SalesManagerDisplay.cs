using It_career_project.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace It_career_project.Presentation.Admin_Display
{
    class SalesManagerDisplay
    {
        SaleController sController;

        public SalesManagerDisplay()
        {
            sController = new SaleController();

            SalesManagerInput();
        }

        private void ShowSalesManagerMenu()
        {
            string headerAndFooter = new string('-', 40);
            string menuWordDivier = new string(' ', 14);

            Console.WriteLine(headerAndFooter);
            Console.WriteLine(menuWordDivier + "SALES_MANAGER" + menuWordDivier);
            Console.WriteLine(headerAndFooter);
            Console.WriteLine("1. See All Sales");
            Console.WriteLine("2. Add Sale");
            Console.WriteLine("3. End Sale");
            Console.WriteLine("4. Refresh Sales");
            Console.WriteLine("5. Go Back");
        }

        private void SalesManagerInput()
        {
            int input = 0;

            do
            {
                ShowSalesManagerMenu();

                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        SeeAllSales();
                        break;

                    case 2:
                        AddSale();
                        break;

                    case 3:
                        EndSale();
                        break;

                    case 4:
                        RefreshSales();
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid option! Select one of the above.");
                        break;
                }
            } while (input != 5);
        }

        private void SeeAllSales()
        {
            try
            {
                List<string> listWithInfoOfSales = sController.GetInfoOfAllSales();

                Console.WriteLine(new string('-', 40) + "\n");

                foreach (string saleInfo in listWithInfoOfSales)
                {
                    Console.WriteLine(saleInfo);
                }

                Console.WriteLine("\n" + new string('-', 40) + "\n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddSale()
        {
            try
            {
                Console.Write("Enter game title: ");
                string gameTitle = Console.ReadLine();
                Console.Write("Enter % discount: ");
                int discount = int.Parse(Console.ReadLine());
                Console.Write("Enter start date (MM/DD/YYYY): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter end date (MM/DD/YYYY): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                sController.AddSale(gameTitle, discount, startDate, endDate);

                Console.WriteLine("Sale successfully added!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EndSale()
        {
            try
            {
                Console.Write("Enter game title of the game which's sale you want to end: ");
                string gameTitle = Console.ReadLine();

                sController.RemoveSale(gameTitle);

                Console.WriteLine("Sale successfully removed!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RefreshSales()
        {
            sController.RefreshSales();
            Console.WriteLine("Operation successful!");
        }
    }
}