using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using spring_hero_bank_cSharp_assignment.Controller;
using spring_hero_bank_cSharp_assignment.Entity;
using spring_hero_bank_cSharp_assignment.Helper;
using spring_hero_bank_cSharp_assignment.Model;

namespace spring_hero_bank_cSharp_assignment.View
{
    public class ConsoleView
    {
        public static Account CurrentLogin;
        private AccountController _accountController = new AccountController();

        public void GenerateMainMenu()
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
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
                        Console.Clear();
                        Console.WriteLine("Đăng ký tài khoản");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        Account newAccount = _accountController.Register();
                        if (newAccount == null)
                        {
                            Console.WriteLine("Đăng ký tài khoản thất bại");
                            PromptHelper.StopConsole("Nhấn phím bất kỳ để quay lại menu chính...");
                            break;
                        }

                        Console.WriteLine("Bạn đã đăng ký thành công tài khoản với thông tin: ");
                        Console.WriteLine($"Họ và tên: {newAccount.FullName}\n" +
                                          $"Số tài khoản: {newAccount.AccountNumber}\n" +
                                          $"Sô dư hiện tại: {newAccount.Balance}\n" +
                                          $"Số điện thoại: {newAccount.PhoneNumber}\n" +
                                          $"Email: {newAccount.Email}\n" +
                                          $"Tên đăng nhập: {newAccount.Username}");
                        PromptHelper.StopConsole("Ấn phím bất kỳ để tiếp tục....");
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Đăng nhập hệ thống");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        var currentAccount = _accountController.Login();
                        if (currentAccount == null)
                        {
                            Console.WriteLine("Đăng nhập thất bại");
                            PromptHelper.StopConsole("Nhấn phím bất kỳ để quay lại menu chính...");
                            break;
                        }

                        CurrentLogin = currentAccount;
                        if (CurrentLogin.Role == AccountRole.ADMIN)
                        {
                            GenerateAdminMenu();
                        }

                        GenerateCustomMenu();

                        break;
                    case 3:
                        Console.WriteLine("Thoát");
                        Console.WriteLine("Cảm ơn quý khách đã sử dụng dịch vụ của ngân hàng SHB");
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
            Console.Clear();
            while (true)
            {
                Console.Clear();

                Console.WriteLine("-------------------------- Ngân Hàng Spring Hero Bank --------------------------");
                Console.WriteLine($"Chào mừng admin {CurrentLogin.FullName} quay trỏ lại. Vui lòng chọn thao tác");
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
                        Console.Clear();
                        Console.WriteLine("danh sách người dùng");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        List<Account> listAccounts = _accountController.ListAccount();
                        if (listAccounts == null)
                        {
                            PromptHelper.StopConsole("Nhấn phím bất kỳ để tiếp tục...");
                            break;
                        }

                        if (listAccounts.Count == 0)
                        {
                            Console.WriteLine("Không có người dùng nào");
                            break;
                        }

                        Console.WriteLine($"đã tìm thấy {listAccounts.Count} tài khoản");
                        PromptHelper.StopConsole("Bấm phím bất kỳ để hiển thị danh sách tài khoản...");
                        //chuyển sang list string để lấy page view
                        List<string> listPages = new List<string>();
                        foreach (var account in listAccounts)
                        {
                            listPages.Add(account.ToString());
                        }

                        //generate page view
                        GeneratePageView(listPages);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Danh sách lịch sử giao dịch");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        var listAllTransactions = _accountController.GetListTransactions();
                        if (listAllTransactions == null)
                        {
                            PromptHelper.StopConsole("Nhấn phím bất kỳ để quay lại menu...");
                            break;
                        }

                        if (listAllTransactions.Count == 0)
                        {
                            Console.WriteLine("Chưa có giao dịch nào");
                            PromptHelper.StopConsole("Nhấn phím bất kỳ để quay lại menu...");
                            break;
                        }

                        Console.WriteLine($"Đã tìm thấy {listAllTransactions.Count} giao dịch");
                        PromptHelper.StopConsole("Bấm phím bất kỳ để hiển thị danh sách giao dịch...");
                        //chuyển sang list <string>
                        List<string> listTransactionString = new List<string>();
                        foreach (var transaction in listAllTransactions)
                        {
                            listTransactionString.Add(transaction.ToString());
                        }

                        //generate page view
                        GeneratePageView(listTransactionString);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Tìm kiếm người dùng theo tên");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        var searchResult = _accountController.SearchAccountByName();
                        if (searchResult == null)
                        {
                            PromptHelper.StopConsole("Nhấn phím bất kỳ để quay lại menu...");
                            break;
                        }

                        if (searchResult.Count == 0)
                        {
                            Console.WriteLine("Không có tài khoản nào khớp");
                            PromptHelper.StopConsole("Nhấn phím bất kỳ để quay lại menu...");
                            break;
                        }

                        Console.WriteLine($"Đã tìm thấy {searchResult.Count} tài khoản");
                        PromptHelper.StopConsole("Bấm phím bất kỳ để hiển thị danh sách tài khoản...");
                        //chuển sang list string
                        List<string> listResultString = new List<string>();
                        foreach (var account in searchResult)
                        {
                            listResultString.Add(account.ToString());
                        }

                        //generate page view
                        GeneratePageView(listResultString);
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine(" Tìm kiếm người dùng theo số tài khoản");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        var resultAccount = _accountController.SearchAccountByAccountNumber();
                        if (resultAccount == null)
                        {
                            PromptHelper.StopConsole("Ấn phím bất kỳ để quay lại menu...");
                            break;
                        }

                        var listAccountString = new List<string>()
                        {
                            resultAccount.ToString()
                        };
                        GeneratePageView(listAccountString);
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Tìm kiếm người dùng theo số điện thoại");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        var resultAccountByPhoneNumber = _accountController.SearchAccountByPhoneNumber();
                        if (resultAccountByPhoneNumber == null)
                        {
                            PromptHelper.StopConsole("Ấn phím bất kỳ để quay lại menu...");
                            break;
                        }

                        var listSearchAccountByPhoneNumber = new List<string>()
                        {
                            resultAccountByPhoneNumber.ToString()
                        };
                        GeneratePageView(listSearchAccountByPhoneNumber);
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Thêm người dùng mới");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        var newUser = _accountController.AddUser();
                        if (newUser == null)
                        {
                            Console.WriteLine("Thêm người dùng thất bại ");
                            PromptHelper.StopConsole("Ấn phím bất kỳ để tiếp tục...");
                            break;
                        }

                        Console.WriteLine("Đã thêm thành công tài khoản: ");
                        Console.WriteLine(newUser.ToString());
                        PromptHelper.StopConsole("Ấn phím bất kỳ để tiếp tục...");
                        break;
                    case 7:
                        Console.Clear();
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
                                PromptHelper.StopConsole("Nhấn phím bất kỳ để tiếp tục...");
                                break;
                            case 2:
                                Console.WriteLine("Mở khóa tài khoản");
                                _accountController.UnLockAccount();
                                PromptHelper.StopConsole("Nhấn phím bất kỳ để tiếp tục...");
                                break;
                            case 3:
                                Console.WriteLine("quay lại menu chính");
                                break;
                        }

                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("Tìm kiếm lịch sử giao dịch theo số tài khoản");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        Console.WriteLine("Nhập số tài khoản bạn muốn tìm kiếm lịch sử giao dịch: ");
                        var accountNumber = Console.ReadLine();
                        var listTransactions = _accountController.GetTransactionsByAccountNumber(accountNumber);
                        if (listTransactions == null)
                        {
                            PromptHelper.StopConsole("Ấn phím bất kỳ để tiếp tục....");
                            break;
                        }

                        List<string> listPage = new List<string>();
                        if (listTransactions.Count == 0)
                        {
                            Console.WriteLine("Không có giao dịch nào được thực hiện");
                            PromptHelper.StopConsole("Ấn phím bất kỳ để quay lại menu...");
                            break;
                        }

                        Console.WriteLine($"đã tìm thấy {listTransactions.Count} giao dịch");
                        foreach (var transaction in listTransactions)
                        {
                            listPage.Add(transaction.ToString());
                        }

                        PromptHelper.StopConsole("Ấn phím bất kỳ để hiển thị danh sách giao dịch...");
                        GeneratePageView(listPage);
                        break;
                    case 9:
                        Console.Clear();
                        Console.WriteLine("Thay đổi thông tin tài khoản");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Lựa chọn thông tin muốn thay đổi: ");
                            Console.WriteLine("1. Thay đổi tên đầy đủ");
                            Console.WriteLine("2. Thay đổi email");
                            Console.WriteLine("3. Thay đổi số điện thoại");
                            Console.WriteLine("4. Quay lại menu");
                            var updateChoice = PromptHelper.GetUserChoice(1, 4);
                            switch (updateChoice)
                            {
                                case 1:
                                    Console.Clear();
                                    _accountController.UpdateFullName(CurrentLogin.AccountNumber);
                                    PromptHelper.StopConsole("Ấn phím bất kỳ để tiếp tục...");
                                    break;
                                case 2:
                                    Console.Clear();
                                    _accountController.UpdateEmail(CurrentLogin.AccountNumber);
                                    PromptHelper.StopConsole("Ấn phím bất kỳ để tiếp tục...");
                                    break;
                                case 3:
                                    Console.Clear();
                                    _accountController
                                        .UpdatePhoneNumber(CurrentLogin.AccountNumber);
                                    PromptHelper.StopConsole("Ấn phím bất kỳ để tiếp tục...");
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
                        Console.Clear();
                        Console.WriteLine(" Thay đổi thông tin mật khẩu");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        if (_accountController.UpdatePassWord(CurrentLogin.AccountNumber) == false)
                        {
                            Console.WriteLine("Câp nhật mật khẩu thất bại...");
                            Console.WriteLine("Phiên đăng nhập đã hết hạn...");
                            PromptHelper.StopConsole("Bấm phím bất kỳ để quay lại menu chính...");
                            break;
                        }

                        Console.WriteLine("mời bạn đăng nhập lại...");
                        PromptHelper.StopConsole("Bấm phím bất kỳ để tiếp tục....");
                        break;
                    case 11:
                        Console.Clear();
                        Console.WriteLine("Thoát");
                        break;
                }

                if (choice == 11 || choice == 10) // break ve main menu ngay sau khi update password hoac chon thoát
                {
                    break;
                }
            } // end while true
        } //end function

        public void GenerateCustomMenu()
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-------------------------- Ngân Hàng Spring Hero Bank --------------------------");
                Console.WriteLine($"Chào mừng {CurrentLogin.FullName} quay trỏ lại. Vui lòng chọn thao tác");
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
                        Console.Clear();
                        Console.WriteLine("Gửi tiền");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        if (_accountController.Deposit(CurrentLogin.AccountNumber) == false)
                        {
                            Console.WriteLine("Có lỗi xảy ra trong quá trình gửi tiền xin hãy thử lại");
                            PromptHelper.StopConsole("Nhấn phím bất kỳ để quay lại menu....");
                            break;
                        }

                        Console.WriteLine("thao tác thành công");
                        PromptHelper.StopConsole("Nhấn phím bất kỳ để tiếp tục...");
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Rút tiền");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        if (_accountController.WithDraw(CurrentLogin.AccountNumber) == false)
                        {
                            Console.WriteLine("Có lỗi xảy ra trong quá trình rút tiền xin hãy thử lại");
                            PromptHelper.StopConsole("Nhấn phím bất kỳ để quay lại menu....");
                            break;
                        }

                        Console.WriteLine("thao tác thành công");
                        PromptHelper.StopConsole("Nhấn phím bất kỳ để tiếp tục...");
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Chuyển khoản");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        if (_accountController.Transfer(CurrentLogin.AccountNumber) == false)
                        {
                            Console.WriteLine("đã xảy ra lỗi xin hãy thử lại");
                            PromptHelper.StopConsole("Nhấn phím bất kỳ để quay lại menu....");
                            break;
                        }

                        Console.WriteLine("thao tác thành công");
                        PromptHelper.StopConsole("Nhấn phím bất kỳ để tiếp tục...");
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Truy vấn số dư");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        var balance = _accountController.GetBalance(CurrentLogin.AccountNumber);
                        if (balance > -1)
                        {
                            Console.WriteLine("Số dư của tài khoản: " + balance);
                        }

                        PromptHelper.StopConsole("Ấn Phím bất kỳ để tiếp tục...");
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Thay đổi thông tin cá nhân");
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
                                    _accountController.UpdateFullName(CurrentLogin.AccountNumber);
                                    PromptHelper.StopConsole("Ấn phím bất kỳ để tiếp tục");
                                    break;
                                case 2:
                                    _accountController.UpdateEmail(CurrentLogin.AccountNumber);
                                    PromptHelper.StopConsole("Ấn phím bất kỳ để tiếp tục");
                                    break;
                                case 3:
                                    _accountController.UpdatePhoneNumber(CurrentLogin.AccountNumber);
                                    PromptHelper.StopConsole("Ấn phím bất kỳ để tiếp tục");
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
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Thay đổi thông tin mật khẩu");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        if (_accountController.UpdatePassWord(CurrentLogin.AccountNumber) == false)
                        {
                            Console.WriteLine("Câp nhật mật khẩu thất bại...");
                            Console.WriteLine("Phiên đăng nhập đã hết hạn...");
                            PromptHelper.StopConsole("Bấm phím bất kỳ để quay lại menu chính...");
                            break;
                        }

                        Console.WriteLine("Cập nhật mật khẩu thành công mời bạn đăng nhập lại...");
                        PromptHelper.StopConsole("Bấm phím bất kỳ để tiếp tục....");
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Truy vấn lịch sử giao dịch");
                        List<SHBTransaction> listTransactions =
                            _accountController.GetTransactionsByAccountNumber(CurrentLogin.AccountNumber);
                        if (listTransactions.Count == 0)
                        {
                            Console.WriteLine("Chưa có giao dịch nào được thực hiện");
                            PromptHelper.StopConsole("Nhấn phím bất kỳ để quay lại menu....");
                            break;
                        }

                        List<string> listPage = new List<string>();
                        foreach (var shbTransaction in listTransactions)
                        {
                            listPage.Add(shbTransaction.ToString());
                        }

                        GeneratePageView(listPage);
                        PromptHelper.StopConsole("Nhấn phím bất kỳ để tiếp tục...");
                        Console.WriteLine(
                            "---------------------------------------------------------------------------------");
                        break;
                    case 8:
                        Console.WriteLine("Thoát");
                        break;
                } //end swtich case

                if (choice == 8 || choice == 6) // sau khi update mat khau -> break về main menu luôn
                {
                    break;
                }
            } //end outer while loop
        } //end of the function

        public void GeneratePageView(List<string> data)
        {
            Console.Clear();
            int currentPageIndex = 0;
            int total = data.Count;
            while (true)
            {
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
                    Console.Clear();
                    currentPageIndex++;
                    if (currentPageIndex > total - 1)
                    {
                        currentPageIndex = 0;
                        continue;
                    }
                }

                if (charKey == 44) //60 is ascii code of comma ',' or <
                {
                    Console.Clear();
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