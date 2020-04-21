using System;
using ShareLib;

namespace ShareCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Sharepad TestInstance = new Sharepad(1);
            /* Console.WriteLine(DateTime.Now); */
            TestInstance.CleanDatabase();

            /* string TextID = TestInstance.CreateText();
            Console.WriteLine($"ID:   '{TextID}'");
            string TextData = TestInstance.ReadText(TextID);
            Console.WriteLine($"Data: '{TextData}'");
            TestInstance.WriteText(TextID, "This is some new text. Kewl!");
            TextData = TestInstance.ReadText(TextID);
            Console.WriteLine($"Data: '{TextData}'"); */

            /* Console.WriteLine("Please give TextID.");
            string TextID = Console.ReadLine();
            Console.WriteLine("Please give TextData.");
            string TextData = Console.ReadLine();
            TestInstance.WriteText(TextID, TextData); */
        }
    }
}
