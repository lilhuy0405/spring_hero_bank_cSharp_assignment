using System;
using System.Text;

namespace spring_hero_bank_cSharp_assignment.Helper
{
    public class AccountHelper
    {
        private static Random _random = new Random();
        public static string RandomAccountNumber(int length)
        {
            
            var randomAccountNumber = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                randomAccountNumber.Append(_random.Next(0, 9).ToString());
            }
            
            return randomAccountNumber.ToString();
        }
    }
}