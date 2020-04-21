using System;
using System.Text;
using ShareLib;

namespace ShareCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Sharepad SharepadInstance = new Sharepad(1);
            Console.WriteLine("Welcome to Sharepad CLI!");
            string UserInput;
            while (true)
            {
                Console.WriteLine("Please input 'r' for reading text, 'w' for writing text, 'c' for database cleanup, or 'q' to quit.");
                UserInput = Console.ReadLine();
                switch (UserInput)
                {
                    case "q":
                        /* This is a VERY dirty feature of C#. The only two ways to exit the while(true) loop within a switch statement are either throwing a return here or using GOTO... */
                        return;
                    case "r":
                        ReadText(SharepadInstance);
                        break;
                    case "w":
                        WriteText(SharepadInstance);
                        break;
                    case "c":
                        Console.WriteLine("Cleaning the database. Please wait...");
                        SharepadInstance.CleanDatabase();
                        Console.WriteLine("Cleanup finished.");
                        break;
                    default:
                        break;
                }
                Console.WriteLine("");
            }
        }
        static void ReadText(Sharepad SharepadInstance)
        {
            Console.WriteLine("Please give a Text ID.");
            string UserInput = Console.ReadLine();
            string TextData;
            Console.WriteLine("Retrieving data. Please wait...");
            try
            {
                TextData = SharepadInstance.ReadText(UserInput);
            }
            catch (System.ArgumentException CurrentException)
            {
                Console.WriteLine($"Error: {CurrentException.Message}");
                return;
            }
            Console.WriteLine("--- Text ---");
            Console.WriteLine(TextData);
            Console.WriteLine("--- Text ---");
            return;
        }
        static void WriteText(Sharepad SharepadInstance)
        {
            Console.WriteLine("Please give text to store. Empty line terminates input.");
            StringBuilder InputText = new StringBuilder();
            string UserInput;
            while (true)
            {
                UserInput = Console.ReadLine();
                if (UserInput == "")
                {
                    break;
                }
                else
                {
                    InputText.Append(UserInput + "\n");
                }
            }
            Console.WriteLine("Writing text. Please wait...");
            string TextID = SharepadInstance.CreateText();
            Console.WriteLine($"Text ID is '{TextID}'.");
            SharepadInstance.WriteText(TextID, InputText.ToString().Trim());
            Console.WriteLine("Text written into the database.");
        }
    }
}
