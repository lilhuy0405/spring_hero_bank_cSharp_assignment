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
        //TODO: put code in try catch
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
                Console.WriteLine("Truy vấn database thất bại lỗi " + e.Message);
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
                Console.WriteLine("Lỗi Kết nối database " + e.Message);
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
            double currentBalane  ;
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

            return  currentBalane;
        }

        
        public bool Withdraw(string accountNumber, double amount)
        {
            var minBalance = 50000;
            var cnn = ConnectionHelper.GetConnection();

            // tao moi tracsaction -> save db
            cnn.Open();
            var transaction = cnn.BeginTransaction();
            try
            {
                var stringCmdGetAccount =
                    $"SELECT balance from `accounts` WHERE accountNumber = {accountNumber} and status = 1 ";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    accountReader.Close();
                    throw new Exception("Account  is not  found or has been deleted");
                }

                var currentBalence = accountReader.GetDouble("balance");
                accountReader.Close();
                //khởi tạo transaction với trạng thái penđing
                var shbTransactionCode = Guid.NewGuid().ToString();
                var shbTransaction = new SHBTransaction()
                {
                    Code = shbTransactionCode,
                    SenderAccountNumber = accountNumber,
                    ReceiverAccountNumber = accountNumber,
                    Type = TransactionType.WITHDRAW,
                    Amount = amount,
                    Fee = 1100,
                    Message = "Withdraw " + amount,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Status = TransactionStatus.PENDING
                };
                //lưu transaction pending vào database
                // var check= _shbTransactionModel.InsertNewShbTransaction(shbTransaction);
                string insertShbTransactionStringCmd =
                    $"INSERT INTO `shb-transactions` VAlUES ('{shbTransaction.Code}','{shbTransaction.SenderAccountNumber}','{shbTransaction.ReceiverAccountNumber}','{shbTransaction.Message}',{shbTransaction.Amount},{shbTransaction.Fee},'{shbTransaction.CreateAt:yyyy-MM-dd hh:mm:ss}','{shbTransaction.UpdateAt:yyyy-MM-dd hh:mm:ss}',{(int) shbTransaction.Status},{(int) shbTransaction.Type}) ";
                var insertShbTransactionCmd = new MySqlCommand(insertShbTransactionStringCmd, cnn);
                insertShbTransactionCmd.ExecuteNonQuery();
                //update so du

                currentBalence = currentBalence - amount - shbTransaction.Fee;
                if (currentBalence < minBalance)
                {
                   //TODO: UPDATE TIME
                    var updateTransactionFailStringCmd =
                        $"UPDATE `shb-transactions` SET status =' {(int) TransactionStatus.FAILED}' WHERE code = '{shbTransactionCode}'";
                    var  updateTransactionFailCmd =  new MySqlCommand(updateTransactionFailStringCmd,cnn);              
                    transaction.Commit();
                    cnn.Close();
                    Console.WriteLine("Số dư tìa khoản không đu để thực hiện giao địch");
                    return false;
                }

                var updateBalanceStringCmd =
                        $"UPDATE accounts SET balance = {currentBalence} WHERE accountNumber = '{accountNumber}'";
                var updateBalanceCmd = new MySqlCommand(updateBalanceStringCmd, cnn);
                int affectedRecord = updateBalanceCmd.ExecuteNonQuery();
                if (affectedRecord == 0)
                {
                    throw new Exception("Lưu so dư mới thất bại");
                }

                string updatShbTransactionStatusCmdString =
                    $"UPDATE `shb-transactions` SET status =' {(int) TransactionStatus.DONE}' WHERE code = '{shbTransactionCode}'";
                var updateShbTransactionStatusCmd = new MySqlCommand(updatShbTransactionStatusCmdString, cnn);
                var updated = updateShbTransactionStatusCmd.ExecuteNonQuery();
                if (updated == 0)
                {
                    throw new Exception("Update transaction thất bại");
                }

                transaction.Commit();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("gửi tiền thất bại " + e.Message);
                Console.WriteLine(e);
                transaction.Rollback();
                // cnn.Close();
                return false;
            }
        }

        public bool Deposit(string accountNumber, double amount)
        {
            var cnn = ConnectionHelper.GetConnection();

            // tao moi tracsaction -> save db
            cnn.Open();
            var transaction = cnn.BeginTransaction();
            try
            {
                var stringCmdGetAccount =
                    $"SELECT balance from `accounts` WHERE accountNumber = {accountNumber} and status = 1 ";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    accountReader.Close();
                    throw new Exception("Account  is not  found or has been deleted");
                }

                var currentBalence = accountReader.GetDouble("balance");
                accountReader.Close();
                //khởi tạo transaction với trạng thái penđing
                var shbTransactionCode = Guid.NewGuid().ToString();
                var shbTransaction = new SHBTransaction()
                {
                    Code = shbTransactionCode,
                    SenderAccountNumber = accountNumber,
                    ReceiverAccountNumber = accountNumber,
                    Type = TransactionType.DEPOSIT,
                    Amount = amount,
                    Fee = 1100,
                    Message = "Deposit" + amount,
                    CreateAt = DateTime.Now,
                    // UpdateAt = DateTime.Now,
                    Status = TransactionStatus.PENDING
                };
                //lưu transaction pending vào database
                // var check= _shbTransactionModel.InsertNewShbTransaction(shbTransaction);
                string insertShbTransactionStringCmd =
                    $"INSERT INTO `shb-transactions` VAlUES ('{shbTransaction.Code}','{shbTransaction.SenderAccountNumber}','{shbTransaction.ReceiverAccountNumber}','{shbTransaction.Message}',{shbTransaction.Amount},{shbTransaction.Fee},'{shbTransaction.CreateAt:yyyy-MM-dd hh:mm:ss}','{shbTransaction.UpdateAt:yyyy-MM-dd hh:mm:ss}',{(int) shbTransaction.Status},{(int) shbTransaction.Type}) ";
                var insertShbTransactionCmd = new MySqlCommand(insertShbTransactionStringCmd, cnn);
                insertShbTransactionCmd.ExecuteNonQuery();
                //update so du

                currentBalence = currentBalence + amount - shbTransaction.Fee;

                var updateBalanceStringCmd =
                    $"UPDATE accounts SET balance = {currentBalence} WHERE accountNumber = '{accountNumber}'";
                var updateBalanceCmd = new MySqlCommand(updateBalanceStringCmd, cnn);
                int affectedRecord = updateBalanceCmd.ExecuteNonQuery();
                if (affectedRecord == 0)
                {
                    throw new Exception("Lưu so dư mới thất bại");
                }

                //update trang thai transaction
                // var updateSuccess = _shbTransactionModel.UpdateShbTransactionStatus(shbTrasnasctionCode, TransactionStatus.DONE);
                // if (!updateSuccess)
                // {
                //     throw new Exception("cập nhật transaction thất bại");
                // }
                string updatShbTransactionStatusCmdString =
                    $"UPDATE `shb-transactions` SET status =' {(int) TransactionStatus.DONE}' WHERE code = '{shbTransactionCode}'";
                var updateShbTransactionStatusCmd = new MySqlCommand(updatShbTransactionStatusCmdString, cnn);
                var updated = updateShbTransactionStatusCmd.ExecuteNonQuery();
                if (updated == 0)
                {
                    throw new Exception("Update transaction thất bại");
                }

                transaction.Commit();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("gửi tiền thất bại " + e.Message);
                Console.WriteLine(e);
                transaction.Rollback();
                // cnn.Close();
                return false;
            }
        }
    }
    
}