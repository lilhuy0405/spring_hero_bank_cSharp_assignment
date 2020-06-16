using System;
using System.Collections.Generic;
using System.Text;
using spring_hero_bank_cSharp_assignment.View;

namespace spring_hero_bank_cSharp_assignment
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
           var view = new ConsoleView();
            // view.LoginSuccess = true;
            // view.IsAdmin = false;
            // view.GenerateMainMenu();
            List<string> list = new List<string>();
            list.Add("nội dung page 1");
            list.Add("nội dung page 2");
            list.Add("nội dung page 3");
            list.Add("nội dung page 4");
            list.Add("nội dung page 5");
            list.Add("nội dung page 6");
            list.Add("nội dung page 7");
            view.GeneratePageView(list);
            
        }
    }
}