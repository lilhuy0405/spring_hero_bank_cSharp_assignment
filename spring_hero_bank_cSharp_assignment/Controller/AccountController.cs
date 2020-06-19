using System;
using System.Collections.Generic;
using HelloT1908E.Helper;
using spring_hero_bank_cSharp_assignment.Entity;
using spring_hero_bank_cSharp_assignment.Model;

namespace spring_hero_bank_cSharp_assignment.Controller
{
    public class AccountController
    {
        private AccountModel _accountModel = new AccountModel();
        private ShbTransactionModel _transactionModel = new ShbTransactionModel();
        private PasswordHelper _passwordHelper = new PasswordHelper();
        
        public Account Login() // Đăng nhập hệ thống 
        {
            Console.WriteLine("Login...");
            Console.WriteLine("Please enter your username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            var password = Console.ReadLine();
            var account = _accountModel.GetActiveAccountByUsername(username);

            // mã hóa pass người dùng nhập vào kèm theo muối trong database và so sánh kết quả với password đã được mã hóa trong database.
            if (account != null && _passwordHelper.ComparePassword(password, account.Salt, account.PasswordHash))
            {
                return account;
            }

            return null;
        }

        // 1. Danh sách người dùng
        public void ListAccount()
        {
            Console.WriteLine("Danh sách người dùng: ");
            foreach (var account in _accountModel.GetListAccount())
            {
                Console.WriteLine(account.ToString());
            }
        }

        // 2. Danh sách lịch sử giao dịch
        public void ListTransaction()
        {
            Console.WriteLine("Danh sách lịch sử giao dịch: ");
            foreach (var transaction in _accountModel.GetListTransaction())
            {
                Console.WriteLine(transaction.ToString());
            }
        }

        // 3. Tìm kiếm người dùng theo tên.
        public void SearchAccountByName()
        {
            Console.WriteLine("Tìm kiếm người dùng theo tên: ");
        }

        // 4. Tìm kiếm người dùng theo số tài khoản.
        public void SearchAccountByAccountNumber()
        {
            Console.WriteLine("Tìm kiếm người dùng theo số tài khoản: ");
        }

        // 5. Tìm kiếm người dùng theo số điện thoại
        public void SearchAccountByPhone()
        {
            Console.WriteLine("Tìm kiếm người dùng theo số điện thoại: ");
        }

        // 6. Thêm người dùng mới
        public void AddAccount()
        {
            Console.WriteLine("Thêm người dùng mới: ");
        }

        // 7. Khoá và mở tài khoản người dùng
        // 7.1. Khóa tài khoản người dùng
        public bool LockAccount()
        {
            Console.WriteLine("Nhập vào số tài khoản bạn muốn khóa");
            var toLockAccountNumber = Console.ReadLine();
            var isSuccess = _accountModel.UpdateAccountStatusByAccountNumber(toLockAccountNumber, AccountStatus.LOCKED);
            if (isSuccess)
            {
                Console.WriteLine($"Đã khóa tài khoản với số tài khoản {toLockAccountNumber} thành công");
                return true;
            }

            Console.WriteLine("Khóa tài khoản " + toLockAccountNumber + " thất bại !");
            return false;
        }

        // 7.2. Mở tài khoản người dùng
        public bool UnLockAccount()
        {
            Console.WriteLine("Nhập vào số tài khoản muốn mở khóa");
            var accountNumber = Console.ReadLine();
            var isSuccess = _accountModel.UpdateAccountStatusByAccountNumber(accountNumber, AccountStatus.ACTIVE);
            if (isSuccess)
            {
                Console.WriteLine($"Đã mở khóa tài khoản với số tài khoản {accountNumber} thành công !");
                return true;
            }

            Console.WriteLine($"mở khóa tài khoản {accountNumber} thất bại !");
            return false;
        }

        public List<SHBTransaction> GetTransactionsByAccountNumber()
        {
            Console.WriteLine("Nhập số tài khoản muốn tra cứu lịch sử giao dịch");
            var accountNumber = Console.ReadLine();
            return _transactionModel.GetTransactionsByAccountNumber(accountNumber);
        }
    }
}