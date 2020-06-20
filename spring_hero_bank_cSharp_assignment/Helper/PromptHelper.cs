using System;

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

        public static void StopConsole()
        {
            Console.WriteLine("Nhập phím bất kỳ để tiếp tục.....");
            Console.ReadKey(true);
        }
    }
}