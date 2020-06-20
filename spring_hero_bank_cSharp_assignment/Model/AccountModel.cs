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
            try
            {
                var listAccount = new List<Account>();
                var cnn = ConnectionHelper.GetConnection();
                cnn.Open();
                var cmd = new MySqlCommand("select * from accounts", cnn);
                cnn.Close();
                return listAccount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        //TODO: put code in try catch
        public List<SHBTransaction> GetListTransaction() // Lấy danh sách giao dịch 
        {
            try
            {
                var listTransaction = new List<SHBTransaction>();
                var cnn = ConnectionHelper.GetConnection();
                cnn.Open();
                var cmd = new MySqlCommand("select * from shb-transactions", cnn);
                cnn.Close();
                return listTransaction;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
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

        public bool CheckExistAccountByUsername(string username)
        {
            var cnn = ConnectionHelper.GetConnection();
            bool result = false;
            cnn.Open();
            try
            {
                var stringCmd = $"SELECT userName FROM accounts WHERE userName = '{username}'";
                //Console.WriteLine(cmdQuery);
                var cmd = new MySqlCommand(
                    stringCmd, cnn);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = true;
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }


        public void SaveAccount(Account newAccount)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();

            try
            {
                var cmd = new MySqlCommand(
                    $"insert into accounts values('{newAccount.AccountNumber}','{newAccount.PhoneNumber}','{newAccount.FullName}','{newAccount.Email}','{newAccount.Username}','{newAccount.PasswordHash}','{newAccount.Salt}','{newAccount.Balance}','{newAccount.Status}','{newAccount.Role}')",
                    cnn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Loi ket noi dâtbase");
                throw;
            }
            finally
            {
                cnn.Close();
            }

            Console.WriteLine("Created new account success");
        }

        public bool UpdateAccountByAccountNumber(string accountNumber, string field, string newData)
        {
            var connection = ConnectionHelper.GetConnection();
            try
            {
                connection.Open();
                var stringUpdateCmd =
                    $"UPDATE `accounts` SET `{field}` = '{newData}' WHERE `accounts`.`accountNumber` = '{accountNumber}'; ";
                MySqlCommand updateCmd = new MySqlCommand(stringUpdateCmd, connection);
                int record = updateCmd.ExecuteNonQuery();
                if (record == 0)
                {
                    throw new Exception("Lỗi khi thao tác với Database");
                }

                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("update thông tin vào database thất bại lỗi: " + e.Message);
                connection.Close();
                return false;
            }

            return false;
        }

        public double GetCurrentBlanceByAccountNumber(string accountNumber) //TODO: fix typo
        {
            double currentBalane;
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            try
            {
                var stringCmdGetAccount =
                    $"select balance from `accounts` where accountNumber = {accountNumber} and status = 1";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    throw new Exception("tai khoan da bi vi hieu hoa ");
                }

                currentBalane = accountReader.GetDouble("balance");

                accountReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                cnn.Close();
            }

            return currentBalane;
        }

        public bool UpdateIncreaseBalanceByAccountNumber(string accountNumber, double newBalance)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var result = false;
            try
            {
                var stringCmdUpdateAccount =
                    $"update `accounts` set blance = {newBalance} where accountNumber = {accountNumber} and status = 1";
                var cmdUpdateAccount = new MySqlCommand(stringCmdUpdateAccount, cnn);
                var successOrNot = cmdUpdateAccount.ExecuteNonQuery();
                if (successOrNot == 0)
                {
                    throw new Exception("Tài khoản của quý khách dã bị vô hiệu hóa");
                }

                Console.WriteLine("Giao dịch thành công");
                Console.WriteLine($"Số dư mới là : {GetCurrentBlanceByAccountNumber(accountNumber)}");
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Giao dịch không thành công");
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }

        public bool UpdateDecreaseBalanceByAccountNumber(string accountNumber, double decreaseAmountAfterFee) //typo
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var currentBlance = GetCurrentBlanceByAccountNumber(accountNumber);
            var newBalance = currentBlance - decreaseAmountAfterFee;
            bool result = false;
            try
            {
                var stringCmdUpdateAccount =
                    $"update `accounts` set blance = {newBalance} where accountNumber = {accountNumber} and status = 1";
                var cmdUpdateAccount = new MySqlCommand(stringCmdUpdateAccount, cnn);
                var successOrNot = cmdUpdateAccount.ExecuteNonQuery();
                if (successOrNot == 0)
                {
                    throw new Exception("tai khan da bij vo hieu hoa");
                }

                Console.WriteLine("giao dich thanh cong");
                result = true;
            }
            catch (Exception e)
            {
                result = false;
                Console.WriteLine("giao dich ko thanh cong");
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }
    }
}