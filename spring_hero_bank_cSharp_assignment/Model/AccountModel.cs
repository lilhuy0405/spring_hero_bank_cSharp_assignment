using System;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using spring_hero_bank_cSharp_assignment.Controller;
using spring_hero_bank_cSharp_assignment.Entity;

namespace spring_hero_bank_cSharp_assignment.Model
{
    public class AccountModel
    {
        public List<Account> GetListAccount() // Lấy danh sách người dùng 
        {
            var listAccount = new List<Account>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand("select * from accounts", cnn);
            cnn.Close();
            return listAccount;
        }
        
        public List<SHBTransaction> GetListTransaction() // Lấy danh sách giao dịch 
        {
            var listTransaction = new List<SHBTransaction>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand("select * from shb-transactions", cnn);
            cnn.Close();
            return listTransaction;
        }
        
        public bool UpdateAccountStatusByAccountNumber(string accountNumber, AccountStatus status)
        {
            var connection = ConnectionHelper.GetConnection();
            try
            {
                connection.Open();
                var updateStringCmd =
                    $"UPDATE `accounts` SET `status` = {(int) status} WHERE `accounts`.`accountNumber` = {accountNumber};";
                var sqlCmd = new MySqlCommand(updateStringCmd, connection);
                int numberOfRecord = sqlCmd.ExecuteNonQuery();
                if (numberOfRecord == 0)
                {
                    Console.WriteLine($"số tài khoản {accountNumber} không tồn tại !");
                    return false;
                }

                Console.WriteLine("Đã cập nhật trạng thái của tài khoản thành công !");
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Lỗi khi truy vấn database " + e.Message);
                connection.Close();
                return false;
            }
        }

        public Account GetActiveAccountByUsername(string username)
        {
            Account account = null;
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"select * from accounts where username = '{username}' and status = '{(int) AccountStatus.ACTIVE}'",
                cnn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                account = new Account()
                {
                    FullName = reader.GetString("fullname"),
                    AccountNumber = reader.GetString("accountNumber"),
                    PhoneNumber = reader.GetString("phoneNumber"),
                    Email = reader.GetString("email"),
                    Salt = reader.GetString("salt"),
                    PasswordHash = reader.GetString("passwordHash"),
                    Username = reader.GetString("username"),
                    Role = (AccountRole) reader.GetInt32("role"),
                    Status = (AccountStatus) reader.GetInt32("status"),
                    Balance = reader.GetDouble("balance")
                };
            }

            cnn.Close();
            return account;
        }
    }
}