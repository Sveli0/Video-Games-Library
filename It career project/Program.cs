using System;

namespace It_career_project
{
    class Program
    {
        static void Main()
        {
            try
            {
                Display display = new Display();
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Wrong data type!");
                Main();
            }
        }
    }
}