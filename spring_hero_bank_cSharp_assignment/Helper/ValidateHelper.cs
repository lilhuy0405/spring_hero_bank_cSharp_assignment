using System.Text.RegularExpressions;

namespace spring_hero_bank_cSharp_assignment.Helper
{
    public class ValidateHelper
    {
        public static bool IsEmailValid(string email)
        {
            //get from: https://www.regexpal.com/93886
            //địa chỉ email phải bắt đầu bằng 1 ký tự
            //địa chỉ email là tập hợp của các ký tự a-z, 0-9 A-Z và có thể có các ký tự như dấu chấm, dấu gạch dưới
            //độ dài tối thiểu của email là 3, độ dài tối đa là 32
            // tên miền của email có thể là tên miền cấp 1 or tên miền cấp 2(@gmail.com or @fpt.vnu.edu)
            string emailPattern = "^[a-zA-z0-9_\\.]{3,32}@[a-z0-9]{2,}(\\.[a-z0-9]{2,4}){1,2}$";
            var result = Regex.IsMatch(email, emailPattern);
            return result;
        }

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            //số điện thoại là 1 dãy số 10 số từ 0 - 9
            //có thể có +84 +23 ....
            string phonePattern = "^([+][0-9]{2})?([0-9]{9,10})$";
            return Regex.IsMatch(phoneNumber, phonePattern);
        }

        public static bool IsUsernameValid(string userName)
        {
            //username khong dc có khoảng trắng
            //do dai tu 6 tro len
            //gom  a-z A-Z 0-9
            //duoc phep co - _ 
            string usernamePattern = "^([a-zA-Z0-9_-]){6,}$";
            return Regex.IsMatch(userName, usernamePattern);
        }

        public static bool IsPasswordValid(string password)
        {
            //password khong dc có khoảng trắng và do dai tu 6 tro len
            if (password.Length < 6 || password.Contains(" "))
            {
                return false;
            }
            return true;
        }
    }
}