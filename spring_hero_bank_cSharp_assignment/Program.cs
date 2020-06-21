using System;
using System.Collections.Generic;
using System.Text;
using spring_hero_bank_cSharp_assignment.Controller;
using spring_hero_bank_cSharp_assignment.Entity;
using spring_hero_bank_cSharp_assignment.Helper;
using spring_hero_bank_cSharp_assignment.Model;
using spring_hero_bank_cSharp_assignment.View;

namespace spring_hero_bank_cSharp_assignment
{
    internal class Program
    {
        public static void Main(string[] args)
        {
           // Console.OutputEncoding = Encoding.UTF8; // console hiển thị dc tiếng việt có dấu
          //  Console.InputEncoding = Encoding.UTF8;
          //  var view = new ConsoleView();
         //   view.LoginSuccess = true;
         //   view.IsAdmin = true;
         //   view.GenerateMainMenu();
         Console.OutputEncoding = Encoding.UTF8;
         Console.InputEncoding = Encoding.UTF8;
         var acc = new AccountModel();
         Console.WriteLine(acc.Withdraw("0000000000", 50000));
         
        }
    }
}