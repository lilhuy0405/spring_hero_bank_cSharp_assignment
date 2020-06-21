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
            //   view.GenerateMainMenu();
         Console.OutputEncoding = Encoding.UTF8;
         Console.InputEncoding = Encoding.UTF8;
         var acc = new AccountModel();
         Console.WriteLine(acc.Deposit("1234569789", 10000));
         Console.WriteLine(acc.Withdraw("123456789", 20000));

        }
    }
}