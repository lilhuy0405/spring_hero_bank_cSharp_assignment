using System;
using System.Text;

namespace spring_hero_bank_cSharp_assignment.Helper
{
    public class PromptHelper
    {
        //hỏi người dùng về lựa chon cua ho cho den khi ho nhap 1 so hop le
        public static int GetUserChoice(int start, int end)
        {
            int choice = 0;
            while (true)
            {
                try
                {
                    choice = Int32.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("giá trị không hợp lệ hãy nhập lại");
                    choice = 0;
                    continue;
                }

                if (choice >= start && choice <= end)
                {
                    Console.WriteLine("Bạn đã chọn lựa chọn số " + choice);
                    break;
                }

                Console.WriteLine($"chỉ chấp nhận giá trị từ {start} đến {end}");
            }

            return choice;
        }

        public static void StopConsole(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey(true);
        }

        public static string GetPassword()
        {
            //với mỗi ký tự nhập vào từ bàn phím 
            //cộng chuỗi nó vào pass và hiển thì 1 dấu sao trên console
            //nếu phim backspace dc ấn kiểm tra xem > 0 hay không nếu có write("/b /b") để di chuyển con trỏ về trước và xóa ký tự cuối của chuỗi
            //nếu enter được ấn break loop

            var pass = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass.Append(key.KeyChar);
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        //xoa di 1 ky tu
                        pass.Length--;
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        //newline
                        Console.WriteLine();
                        break;
                    }
                }
            }

            return pass.ToString();
        }
    }
}