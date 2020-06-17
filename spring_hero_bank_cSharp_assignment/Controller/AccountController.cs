using System;
using System.Collections.Generic;
using spring_hero_bank_cSharp_assignment.Entity;
using spring_hero_bank_cSharp_assignment.Model;

namespace spring_hero_bank_cSharp_assignment.Controller
{
    public class AccountController
    {
        private AccountModel _accountModel = new AccountModel();
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
            return  _transactionModel.GetTransactionsByAccountNumber(accountNumber);
        }
    }
}