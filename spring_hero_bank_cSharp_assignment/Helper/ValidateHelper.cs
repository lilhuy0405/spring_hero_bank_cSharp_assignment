using System;
using System.Text.RegularExpressions;

namespace spring_hero_bank_cSharp_assignment.Helper
{
    public class ValidateHelper
    {
        public static bool IsEmailValid(string email)
        {
            if (!email.Contains("@") || !email.Contains("."))
            {
                Console.WriteLine("Email bạn nhập không hợp lệ email phải bao gồm ký tự @ ví dụ me@gmail.com");
                return false;
            }

            return true;
        }
    }
}