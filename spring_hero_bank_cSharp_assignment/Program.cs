using System;
using System.Text;
using spring_hero_bank_cSharp_assignment.View;

namespace spring_hero_bank_cSharp_assignment
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            new ConsoleView().GenerateMainMenu();
 
        }
        
    }
}