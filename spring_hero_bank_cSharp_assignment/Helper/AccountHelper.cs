using System;

namespace spring_hero_bank_cSharp_assignment.Helper
{
    public class AccountHelper
    {
        public string RamdomAccountNumber()
        {
            var random = new Random();
            string ramdomNumber = null;
            int i;
            for (i = 1; i < 11; i++)
            {
                ramdomNumber += random.Next(0, 9).ToString();
            }
            
            return ramdomNumber;
        }
    }
}