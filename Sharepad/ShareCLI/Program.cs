using System;
using ShareLib;

namespace ShareCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Sharepad TestInstance = new Sharepad();
            Console.WriteLine(TestInstance.CreateID());
            Console.WriteLine(TestInstance.CreateID());
            string TextID = TestInstance.CreateText();
            Console.WriteLine(TextID);
            /* Console.WriteLine("Please give TextID.");
            string TextID = Console.ReadLine();
            Console.WriteLine("Please give TextData.");
            string TextData = Console.ReadLine();
            TestInstance.WriteText(TextID, TextData); */
        }
    }
}
