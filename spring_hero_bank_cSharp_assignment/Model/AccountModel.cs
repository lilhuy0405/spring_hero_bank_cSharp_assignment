using System;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using spring_hero_bank_cSharp_assignment.Controller;
using spring_hero_bank_cSharp_assignment.Entity;

namespace spring_hero_bank_cSharp_assignment.Model
{
    public class AccountModel
    {
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
        
        public bool CheckExistAccountByUsername(string username)
        {
            var cnn = ConnectionHelper.GetConnection();
            bool result = false ;
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
    }
}