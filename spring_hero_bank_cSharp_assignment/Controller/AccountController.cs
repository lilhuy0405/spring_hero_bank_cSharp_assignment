using System;
using System.Collections.Generic;
using spring_hero_bank_cSharp_assignment.Entity;
using spring_hero_bank_cSharp_assignment.Helper;
using spring_hero_bank_cSharp_assignment.Model;

namespace spring_hero_bank_cSharp_assignment.Controller
{
    public class AccountController
    {
        private PasswordHelper _passwordHelper = new PasswordHelper();
        private AccountModel _accountModel = new AccountModel();
        private AccountHelper _accountHelper = new AccountHelper();
        private ShbTransactionModel _transactionModel = new ShbTransactionModel();

        public bool LockAccount()
        {
            Console.WriteLine("Nhập vào số tài khoản bạn muốn khóa");
            var toLockAccountNumber = Console.ReadLine();
            var isSuccess = _accountModel.UpdateAccountStatusByAccountNumber(toLockAccountNumber, AccountStatus.LOCK);
            if (isSuccess)
            {
                Console.WriteLine($"Đã khóa tài khoản với số tài khoản {toLockAccountNumber} thành công");
                return true;
            }

            Console.WriteLine("Khóa tài khoản " + toLockAccountNumber + " thất bại !");
            return false;
        }

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


        public void Register()
        {
            bool check = true;

            var accountNumber = "";
            while (true)
            {
                accountNumber = _accountHelper.RamdomAccountNumber();
                var isExist = _accountModel.CheckExistAccountByUsername(accountNumber);
                if (isExist == false)
                {
                    break;
                }
            }

            var newAccount = new Account()
            {
                Balance = 0,
                Status = AccountStatus.ACTIVE,
                Salt = _passwordHelper.GenerateSalt(),
                AccountNumber = accountNumber,
                Role = AccountRole.GUEST
            };

            Console.WriteLine("--Đăng kí--"); //tieng viet
            Console.WriteLine("Nhập tên đăng nhập");
            string username = Console.ReadLine();
            while (_accountModel.CheckExistAccountByUsername(username))
            {
                Console.WriteLine("Tên đăng nhập đã tồn tại vui long chon tên đăng nhập khác");
                username = Console.ReadLine();
            }

            newAccount.Username = username;

            Console.WriteLine("Enter password");
            var password = Console.ReadLine();

            newAccount.PasswordHash = _passwordHelper.MD5Hash(newAccount.Salt + password);

            Console.WriteLine("Enter your full name");
            newAccount.FullName = Console.ReadLine();

            Console.WriteLine("Enter your email");
            newAccount.Email = Console.ReadLine();

            Console.WriteLine("Enter your phone number");
            newAccount.PhoneNumber = Console.ReadLine();

            //Console.WriteLine(newAccount.ToString());
            _accountModel.SaveAccount(newAccount);
        }

        public bool UpdatePhoneNumber(string accountNumber)
        {
            Console.WriteLine("Nhập số điện thoại mới của bạn");
            string newPhoneNumber = Console.ReadLine();
            //TODO: validate phoneNumber input
            var res = _accountModel.UpdateAccountByAccountNumber(accountNumber, "phoneNumber", newPhoneNumber);
            if (res == true)
            {
                Console.WriteLine($"Đã update số điện thoại của số tài khoản {accountNumber} thành công");
            }
            else
            {
                Console.WriteLine("Update thông tin thất bại");
                return false;
            }

            return false;
        }

        public bool UpdateFullName(string accountNumber)
        {
            Console.WriteLine("Nhập tên đầy đủ mới của bạn");
            string newFullName = Console.ReadLine();

            var res = _accountModel.UpdateAccountByAccountNumber(accountNumber, "fullName", newFullName);
            if (res == true)
            {
                Console.WriteLine($"Đã update  tên của số tài khoản {accountNumber} thành công");
            }
            else
            {
                Console.WriteLine("Update thông tin thất bại");
                return false;
            }

            return false;
        }

        public bool UpdateEmail(string accountNumber)
        {
            Console.WriteLine("Nhập email mới của bạn");
            string newEmail = "";
            while (true)
            {
                newEmail = Console.ReadLine();
                if (ValidateHelper.IsEmailValid(newEmail))
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            var res = _accountModel.UpdateAccountByAccountNumber(accountNumber, "email", newEmail);
            if (res == true)
            {
                Console.WriteLine($"Đã update email của số tài khoản {accountNumber} thành công");
            }
            else
            {
                Console.WriteLine("Update thông tin thất bại");
                return false;
            }

            return false;
        }

        public bool UpdatePassWord(string accountNumber)
        {
            Console.WriteLine("Nhập mật khẩu cũ của bạn: ");
            string oldPassWord = Console.ReadLine();
            return false;
        }
    }
}