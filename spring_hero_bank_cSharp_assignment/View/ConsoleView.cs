using System;
using System.Collections.Generic;
using spring_hero_bank_cSharp_assignment.Controller;
using spring_hero_bank_cSharp_assignment.Entity;
using spring_hero_bank_cSharp_assignment.Helper;
using spring_hero_bank_cSharp_assignment.Model;

namespace spring_hero_bank_cSharp_assignment.View
{
    public class ConsoleView
    {
        private static Account _currentLogin;
        private AccountController _accountController = new AccountController();

        public void GenerateMainMenu()
        {
            while (true)
            {
                Console.WriteLine("-------------------------- Ngân Hàng Spring Hero Bank --------------------------");
                Console.WriteLine("1. Đăng ký tài khoản.");
                Console.WriteLine("2. Đăng Nhập hệ thống.");
                Console.WriteLine("3. Thoát");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Nhập lựa chọn của bạn (1, 2, 3)");
                var choice = PromptHelper.GetUserChoice(1, 3);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Đăng ký tài khoản");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        break;
                    case 2:
                        Console.WriteLine("Đăng nhập hệ thống");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        var account = _accountController.Login();
                        if (account == null)
                        {
                            Console.WriteLine("Login failed!");
                            return;
                        }

                        _currentLogin = account;
                        Console.WriteLine($"Login success! Welcome back {_currentLogin.FullName}");
                        if (_currentLogin.Role == AccountRole.ADMIN)
                        {
                            GenerateAdminMenu();
                        }
                        else
                        {
                            GenerateCustomMenu();
                        }


                        break;
                    case 3:
                        Console.WriteLine("Thoát");
                        break;
                    default:
                        Console.WriteLine("không hợp lệ");
                        break;
                }

                if (choice == 3)
                {
                    break;
                }
            }
        }

        public void GenerateAdminMenu()
        {
            while (true)
            {
                Console.WriteLine("-------------------------- Ngân Hàng Spring Hero Bank --------------------------");
                Console.WriteLine("Chào mừng admin xuân hùng quay trỏ lại. Vui lòng chọn thao tác");
                Console.WriteLine("1. Danh sách người dùng");
                Console.WriteLine("2. Danh sách lịch sử giao dịch.");
                Console.WriteLine("3. Tìm kiếm người dùng theo tên.");
                Console.WriteLine("4. Tìm kiếm người dùng theo số tài khoản");
                Console.WriteLine("5. Tìm kiếm người dùng theo số điện thoại");
                Console.WriteLine("6. Thêm người dùng mới");
                Console.WriteLine("7. Khoá và mở tài khoản người dùng");
                Console.WriteLine("8. Tìm kiếm lịch sử giao dịch theo số tài khoản");
                Console.WriteLine("9. Thay đổi thông tin tài khoản.");
                Console.WriteLine("10. Thay đổi thông tin mật khẩu");
                Console.WriteLine("11. Thoát");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Nhập lựa chọn của bạn (từ 1 đến 11)");
                var choice = PromptHelper.GetUserChoice(1, 11);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("danh sách người dùng");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        _accountController.ListAccount();
                        break;
                    case 2:
                        Console.WriteLine("Danh sách lịch sử giao dịch");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        _accountController.ListTransaction();
                        break;
                    case 3:
                        Console.WriteLine("Tìm kiếm người dùng theo tên");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        _accountController.SearchAccountByName();
                        break;
                    case 4:
                        Console.WriteLine(" Tìm kiếm người dùng theo số tài khoản");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        _accountController.SearchAccountByAccountNumber();
                        break;
                    case 5:
                        Console.WriteLine("Tìm kiếm người dùng theo số điện thoại");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        _accountController.SearchAccountByPhoneNumber();
                        break;
                    case 6:
                        Console.WriteLine(" Thêm người dùng mới");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        _accountController.AddUser();
                        break;
                    case 7:
                        Console.WriteLine("Khoá và mở tài khoản người dùng");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        Console.WriteLine("1. Khóa Tài khoản theo số tài khoản");
                        Console.WriteLine("2. Mở tài khoản theo số tài Khoản");
                        Console.WriteLine("3. Quay lại menu");
                        Console.WriteLine("Nhập lựa chọn (1, 2)");
                        var actionChoice = PromptHelper.GetUserChoice(1, 3);
                        switch (actionChoice)
                        {
                            case 1:
                                Console.WriteLine("khóa tài khoản");
                                _accountController.LockAccount();
                                PromptHelper.StopConsole();
                                break;
                            case 2:
                                Console.WriteLine("Mở khóa tài khoản");
                                _accountController.UnLockAccount();
                                PromptHelper.StopConsole();
                                break;
                            case 3:
                                Console.WriteLine("quay lại menu chính");
                                break;
                        }

                        break;
                    case 8:
                        Console.WriteLine("Tìm kiếm lịch sử giao dịch theo số tài khoản");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        var listTransactions = _accountController.GetTransactionsByAccountNumber();
                        List<string> listPage = new List<string>();

                        if (listTransactions.Count == 0)
                        {
                            Console.WriteLine("Không có giao dịch nào được thực hiện");
                            break;
                        }

                        foreach (var transaction in listTransactions)
                        {
                            listPage.Add(transaction.ToString());
                        }

                        Console.WriteLine($"Đã tìm thấy {listTransactions.Count} lịch sử giao dịch");
                        GeneratePageView(listPage);
                        PromptHelper.StopConsole();
                        break;
                    case 9:
                        Console.WriteLine("Thay đổi thông tin tài khoản");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        while (true)
                        {
                            Console.WriteLine("Lựa chọn thông tin muốn thay đổi: ");
                            Console.WriteLine("1. Thay đổi tên đầy đủ");
                            Console.WriteLine("2. Thay đổi email");
                            Console.WriteLine("3. Thay đổi số điện thoại");
                            Console.WriteLine("4. Quay lại menu");
                            var updateChoice = PromptHelper.GetUserChoice(1, 4);
                            switch (updateChoice)
                            {
                                case 1:
                                    _accountController.UpdateFullName("123456789");
                                    break;
                                case 2:
                                    _accountController.UpdateEmail("123456789");
                                    break;
                                case 3:
                                    _accountController
                                        .UpdatePhoneNumber("123456789"); // instance account number just for test
                                    break;
                                case 4:
                                    break;
                            }

                            if (updateChoice == 4)
                            {
                                break;
                            }
                        }

                        break;
                    case 10:
                        Console.WriteLine(" Thay đổi thông tin mật khẩu");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        break;
                    case 11:
                        Console.WriteLine("Thoát");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        break;
                    // default:
                    //     break;
                }

                if (choice == 11)
                {
                    break;
                }
            } // end while true
        } //end function

        public void GenerateCustomMenu()
        {
            while (true)
            {
                Console.WriteLine("-------------------------- Ngân Hàng Spring Hero Bank --------------------------");
                Console.WriteLine("Chào mừng xuân hùng quay trỏ lại. Vui lòng chọn thao tác");
                Console.WriteLine("1. Gửi tiền");
                Console.WriteLine("2. Rút tiền");
                Console.WriteLine("3. Chuyển khoản");
                Console.WriteLine("4. Truy vấn số dư");
                Console.WriteLine("5. Thay đổi thông tin cá nhân");
                Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("7. Truy vấn lịch sử giao dịch");
                Console.WriteLine("8. Thoát");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Nhập lựa chọn của bạn (từ 1 đến 8)");
                int choice = PromptHelper.GetUserChoice(1, 8);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Gửi tiền");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");

                        break;
                    case 2:
                        Console.WriteLine("Rút tiền");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        break;
                    case 3:
                        Console.WriteLine("Chuyển khoản");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        break;
                    case 4:
                        Console.WriteLine("Truy vấn số dư");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        break;
                    case 5:
                        Console.WriteLine("Thay đổi thông tin cá nhân");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        break;
                    case 6:
                        Console.WriteLine("Thay đổi thông tin mật khẩu");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        break;
                    case 7:
                        Console.WriteLine("Truy vấn lịc sử giao dịch");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        break;
                    case 8:
                        Console.WriteLine("Thoát");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        break;
                    default:
                        break;
                } //end swtich case

                if (choice == 8)
                {
                    break;
                }
            } //end outer while loop
        } //end of the function

        public void GeneratePageView(List<string> data)
        {
            int currentPageIndex = 0;
            int total = data.Count;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                    $"---------------------------- đang hiển thị trang {currentPageIndex + 1} trên tổng số {total} trang ----------------------------");
                Console.WriteLine(data[currentPageIndex]);
                Console.WriteLine(
                    "--------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("ấn > để next, ấn < để back, ấn esc để exit");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                int charKey = keyInfo.GetHashCode(); //get ascii code of keyboard character entered
                if (charKey == 46) // 62 is ascii code of '.' or >
                {
                    currentPageIndex++;
                    if (currentPageIndex > total - 1)
                    {
                        currentPageIndex = 0;
                        continue;
                    }
                }

                if (charKey == 44) //60 is ascii code of comma ',' or <
                {
                    currentPageIndex--;
                    if (currentPageIndex < 0)
                    {
                        currentPageIndex = total - 1;
                        continue;
                    }
                }

                if (charKey == 27) //27 is ascii code of esc
                {
                    break;
                }
            }
        }
    } // end of the class
}