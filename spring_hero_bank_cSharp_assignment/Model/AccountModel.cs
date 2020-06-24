using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
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
            try
            {
                cnn.Open();
                var selectAccountsStringCmd = "SELECT * FROM accounts";
                var selectAccountsSqlCmd = new MySqlCommand(selectAccountsStringCmd, cnn);
                var accountReader = selectAccountsSqlCmd.ExecuteReader();
                while (accountReader.Read())
                {
                    var account = new Account()
                    {
                        FullName = accountReader.GetString("fullName"),
                        AccountNumber = accountReader.GetString("accountNumber"),
                        PhoneNumber = accountReader.GetString("phoneNumber"),
                        Email = accountReader.GetString("email"),
                        Salt = accountReader.GetString("salt"),
                        PasswordHash = accountReader.GetString("hashPassword"),
                        Username = accountReader.GetString("username"),
                        Role = (AccountRole) accountReader.GetInt32("role"),
                        Status = (AccountStatus) accountReader.GetInt32("status"),
                        Balance = accountReader.GetDouble("balance")
                    };
                    listAccount.Add(account);
                }

                accountReader.Close();
                cnn.Close();
                return listAccount;
            }
            catch (Exception e)
            {
                Console.WriteLine("Lấy danh sách tài khoản thất bại, " + e.Message);
                cnn.Close();
                return null;
            }
        }
        
        public List<Account> GetAccountsByName(string fullName) // Tìm kiếm người dùng theo tên
        {
            var listAccount = new List<Account>();
            var connection = ConnectionHelper.GetConnection();
            try
            {
                connection.Open();
                string stringCmd = $"SELECT * FROM accounts WHERE fullName = '{fullName}'";
                var sqlCmd = new MySqlCommand(stringCmd, connection);
                var accountReader = sqlCmd.ExecuteReader();
                while (accountReader.Read())
                {
                    var account = new Account()
                    {
                        FullName = accountReader.GetString("fullName"),
                        AccountNumber = accountReader.GetString("accountNumber"),
                        PhoneNumber = accountReader.GetString("phoneNumber"),
                        Email = accountReader.GetString("email"),
                        Salt = accountReader.GetString("salt"),
                        PasswordHash = accountReader.GetString("hashPassword"),
                        Username = accountReader.GetString("username"),
                        Role = (AccountRole) accountReader.GetInt32("role"),
                        Status = (AccountStatus) accountReader.GetInt32("status"),
                        Balance = accountReader.GetDouble("balance")
                    };
                    listAccount.Add(account);
                }

                accountReader.Close();
                connection.Close();
                return listAccount;
            }
            catch (Exception e)
            {
                Console.WriteLine("Tìm tài khoản thất bại, " + e.Message);
                connection.Close();
                return null;
            }
        }


        public Account GetAccountByAccountNumber(string accountNumber) // Tìm kiếm người dùng theo số tài khoản
        {
            Account account = new Account();
            var cnn = ConnectionHelper.GetConnection();

            try
            {
                cnn.Open();
                var cmd = new MySqlCommand(
                    $"select * from accounts where accountNumber = '{accountNumber}';",
                    cnn);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    account.FullName = reader.GetString("fullName");
                    account.AccountNumber = reader.GetString("accountNumber");
                    account.PhoneNumber = reader.GetString("phoneNumber");
                    account.Email = reader.GetString("email");
                    account.Salt = reader.GetString("salt");
                    account.PasswordHash = reader.GetString("hashPassword");
                    account.Username = reader.GetString("username");
                    account.Role = (AccountRole) reader.GetInt32("role");
                    account.Status = (AccountStatus) reader.GetInt32("status");
                    account.Balance = reader.GetDouble("balance");
                }
                else
                {
                    throw new Exception("Tài khoản không tồn tại");
                }

                reader.Close();
                cnn.Close();
                return account;
            }
            catch (Exception e)
            {
                Console.WriteLine("Tìm tài khoản thất bại, " + e.Message);
                cnn.Close();
                return null;
            }
        }


        public Account GetAccountByPhoneNumber(string phoneNumber) // Tìm kiếm người dùng theo số điện thoại
        {
            Account account = new Account();
            var cnn = ConnectionHelper.GetConnection();

            try
            {
                cnn.Open();
                var cmd = new MySqlCommand(
                    $"select * from accounts where phoneNumber = '{phoneNumber}'",
                    cnn);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    account.FullName = reader.GetString("fullName");
                    account.AccountNumber = reader.GetString("accountNumber");
                    account.PhoneNumber = reader.GetString("phoneNumber");
                    account.Email = reader.GetString("email");
                    account.Salt = reader.GetString("salt");
                    account.PasswordHash = reader.GetString("hashPassword");
                    account.Username = reader.GetString("username");
                    account.Role = (AccountRole) reader.GetInt32("role");
                    account.Status = (AccountStatus) reader.GetInt32("status");
                    account.Balance = reader.GetDouble("balance");
                }
                else
                {
                    throw new Exception("Tài khoản không tồn tại");
                }

                reader.Close();
                cnn.Close();
                return account;
            }
            catch (Exception e)
            {
                Console.WriteLine("Tìm tài khoản thất bại, " + e.Message);
                cnn.Close();
                return null;
            }
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
            Account account = new Account();
            var cnn = ConnectionHelper.GetConnection();

            try
            {
                cnn.Open();
                var cmd = new MySqlCommand(
                    $"select * from accounts where username = '{username}' and status = {(int) AccountStatus.ACTIVE}",
                    cnn);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    account.FullName = reader.GetString("fullName");
                    account.AccountNumber = reader.GetString("accountNumber");
                    account.PhoneNumber = reader.GetString("phoneNumber");
                    account.Email = reader.GetString("email");
                    account.Salt = reader.GetString("salt");
                    account.PasswordHash = reader.GetString("hashPassword");
                    account.Username = reader.GetString("username");
                    account.Role = (AccountRole) reader.GetInt32("role");
                    account.Status = (AccountStatus) reader.GetInt32("status");
                    account.Balance = reader.GetDouble("balance");
                }
                else
                {
                    throw new Exception("tài khoản không tồn tại hoặc đã bị khóa");
                }

                reader.Close();
                cnn.Close();
                return account;
            }
            catch (Exception e)
            {
                Console.WriteLine("Lấy thông tin tài khoản thất bại " + e.Message);
                cnn.Close();
                return null;
            }
        }

        public bool CheckExistAccountByUsername(string username)
        {
            var cnn = ConnectionHelper.GetConnection();
            bool result = false;

            try
            {
                cnn.Open();
                var stringCmd = $"SELECT userName FROM accounts WHERE username = '{username}'";
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
                Console.WriteLine("Kiểm tra tài khoản thất bại " + e.Message);
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }

        public bool CheckExistAccountNumber(string accountNumber)
        {
            var connection = ConnectionHelper.GetConnection();
            try
            {
                connection.Open();
                var stringCmd = $"SELECT * FROM accounts WHERE accountNumber = '{accountNumber}'";
                var sqlCmd = new MySqlCommand(stringCmd, connection);
                var reader = sqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Kiểm tra tài khoản theo số tài khoản thất bại " + e.Message);
                return false;
            }

            return false;
        }

        public bool SaveAccount(Account newAccount)
        {
            var cnn = ConnectionHelper.GetConnection();


            try
            {
                cnn.Open();
                string insertAccountStringCmd =
                    $"INSERT INTO `accounts`(`accountNumber`, `phoneNumber`, `fullName`, `email`, `username`, " +
                    $"`hashPassword`, `salt`, `balance`, `status`, `role`) " +
                    $"VALUES ('{newAccount.AccountNumber}','{newAccount.PhoneNumber}','{newAccount.FullName}','{newAccount.Email}'," +
                    $"'{newAccount.Username}','{newAccount.PasswordHash}','{newAccount.Salt}',{newAccount.Balance}," +
                    $"{(int) newAccount.Status},{(int) newAccount.Role})";
                var insertSqlCmd = new MySqlCommand(insertAccountStringCmd, cnn);
                insertSqlCmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Lưu người dùng thất bại," + e.Message);
                cnn.Close();
                return false;
            }
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
        }

        public double GetCurrentBalanceByAccountNumber(string accountNumber)
        {
            double currentBalance;
            var cnn = ConnectionHelper.GetConnection();

            try
            {
                cnn.Open();
                var stringCmdGetAccount =
                    $"select balance from `accounts` where accountNumber = '{accountNumber}' and status = {(int) AccountStatus.ACTIVE}";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    throw new Exception("Tài khoản không tồn tại hoặc đã bị khóa");
                }

                currentBalance = accountReader.GetDouble("balance");

                accountReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Lấy số dư thất bại, " + e.Message);
                currentBalance = -1;
            }
            finally
            {
                cnn.Close();
            }

            return currentBalance;
        }


        public bool Withdraw(string accountNumber, double amount)
        {
            var minBalance = 50000;
            var cnn = ConnectionHelper.GetConnection();

            cnn.Open();
            var transaction = cnn.BeginTransaction();
            try
            {
                var stringCmdGetAccount =
                    $"SELECT balance from `accounts` WHERE accountNumber = '{accountNumber}' and status = {(int) AccountStatus.ACTIVE}";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    accountReader.Close();
                    throw new Exception("Không tìm thấy tài khoản hoặc tài khoản đã bị khóa");
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
                //TODO: change sql command
                string insertShbTransactionStringCmd =
                    $"INSERT INTO `shb-transactions` VALUES ('{shbTransaction.Code}','{shbTransaction.SenderAccountNumber}','{shbTransaction.ReceiverAccountNumber}','{shbTransaction.Message}',{shbTransaction.Amount},{shbTransaction.Fee},'{shbTransaction.CreateAt:yyyy-MM-dd hh:mm:ss}','{shbTransaction.UpdateAt:yyyy-MM-dd hh:mm:ss}',{(int) shbTransaction.Status},{(int) shbTransaction.Type}) ";
                var insertShbTransactionCmd = new MySqlCommand(insertShbTransactionStringCmd, cnn);
                insertShbTransactionCmd.ExecuteNonQuery();
                //update so du

                currentBalence = currentBalence - amount - shbTransaction.Fee;
                //new so du khong hop le thi commit 1 failed transaction vao database
                if (currentBalence < minBalance)
                {
                    var updateTime = DateTime.Now;
                    var updateTransactionFailStringCmd =
                        $"UPDATE `shb-transactions` SET status =' {(int) TransactionStatus.FAILED}', updateAt = '{updateTime:yyyy-MM-dd hh:mm:ss}' WHERE code = '{shbTransactionCode}'";
                    var updateTransactionFailCmd = new MySqlCommand(updateTransactionFailStringCmd, cnn);
                    updateTransactionFailCmd.ExecuteNonQuery();

                    transaction.Commit();
                    cnn.Close();
                    Console.WriteLine("Số dư tài khoản không đủ để thực hiện giao địch");
                    return false;
                }

                var updateBalanceStringCmd =
                    $"UPDATE accounts SET balance = {currentBalence} WHERE accountNumber = '{accountNumber}'";
                var updateBalanceCmd = new MySqlCommand(updateBalanceStringCmd, cnn);
                int affectedRecord = updateBalanceCmd.ExecuteNonQuery();
                if (affectedRecord == 0)
                {
                    throw new Exception("cập nhật số dư mới thất bại");
                }

                //update transaction thành done
                string updateShbTransactionStatusCmdString =
                    $"UPDATE `shb-transactions` SET status =' {(int) TransactionStatus.DONE}' WHERE code = '{shbTransactionCode}'";
                var updateShbTransactionStatusCmd = new MySqlCommand(updateShbTransactionStatusCmdString, cnn);
                var updated = updateShbTransactionStatusCmd.ExecuteNonQuery();
                if (updated == 0)
                {
                    throw new Exception("cập nhật lịch sử giao dịc thất bại");
                }

                transaction.Commit();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("rút tiền thất bại " + e.Message);
                Console.WriteLine(e);
                transaction.Rollback();
                cnn.Close();
                return false;
            }
        }

        public bool Deposit(string accountNumber, double amount)
        {
            //amount da dc validate > 0 o controller
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var transaction = cnn.BeginTransaction();
            try
            {
                var stringCmdGetAccount =
                    $"SELECT balance from `accounts` WHERE accountNumber = {accountNumber} and status = {(int) AccountStatus.ACTIVE};";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    accountReader.Close();
                    throw new Exception("Tài khoản không tồn tại hoặc đã bị xóa");
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
                    UpdateAt = DateTime.Now,
                    Status = TransactionStatus.PENDING
                };
                //lưu transaction pending vào database
                //TODO: change sql command
                string insertShbTransactionStringCmd =
                    $"INSERT INTO `shb-transactions` VAlUES ('{shbTransaction.Code}','{shbTransaction.SenderAccountNumber}','{shbTransaction.ReceiverAccountNumber}','{shbTransaction.Message}',{shbTransaction.Amount},{shbTransaction.Fee},'{shbTransaction.CreateAt:yyyy-MM-dd hh:mm:ss}','{shbTransaction.UpdateAt:yyyy-MM-dd hh:mm:ss}',{(int) shbTransaction.Status},{(int) shbTransaction.Type}) ";
                var insertShbTransactionCmd = new MySqlCommand(insertShbTransactionStringCmd, cnn);
                insertShbTransactionCmd.ExecuteNonQuery();
                //update so du moi
                currentBalence = currentBalence + amount - shbTransaction.Fee;

                var updateBalanceStringCmd =
                    $"UPDATE accounts SET balance = {currentBalence} WHERE accountNumber = '{accountNumber}'";
                var updateBalanceCmd = new MySqlCommand(updateBalanceStringCmd, cnn);
                int affectedRecord = updateBalanceCmd.ExecuteNonQuery();
                if (affectedRecord == 0)
                {
                    throw new Exception("cập nhật số dư mới thất bại");
                }

                //update trạng thái transaction pending -> done
                var updateTime = DateTime.Now;
                string updateShbTransactionStatusCmdString =
                    $"UPDATE `shb-transactions` SET status = {(int) TransactionStatus.DONE}, updateAt = '{updateTime:yyyy-MM-dd hh:mm:ss}' WHERE code = '{shbTransactionCode}'";
                var updateShbTransactionStatusCmd = new MySqlCommand(updateShbTransactionStatusCmdString, cnn);
                var updated = updateShbTransactionStatusCmd.ExecuteNonQuery();
                if (updated == 0)
                {
                    throw new Exception("Cập nhật lịch sử giao dịch thất bại");
                }

                //commmit transaction -> close connection -> return fuction
                transaction.Commit();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("gửi tiền thất bại " + e.Message);
                Console.WriteLine(e);
                transaction.Rollback();
                cnn.Close();
                return false;
            }
        }


        public bool Transfer(string senderAccountNumber, string receiverAccountNumber, double amount)
        {
            double minBalance = 50000.0;
            //1. Open connection
            var connection = ConnectionHelper.GetConnection();
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                //. get balacne of the sender
                string getSenderBalanceStringCmd =
                    $"SELECT balance from `accounts` WHERE accountNumber = {senderAccountNumber} and status = {(int) AccountStatus.ACTIVE};";
                var getSenderBalanceSqlCmd = new MySqlCommand(getSenderBalanceStringCmd, connection);
                var senderBalanceReader = getSenderBalanceSqlCmd.ExecuteReader();
                if (!senderBalanceReader.Read())
                {
                    senderBalanceReader.Close();
                    throw new Exception("Tài khoản người gửi không tồn tại hoặc đã bị khóa");
                }

                var senderBalance = senderBalanceReader.GetDouble("balance");
                //close reader
                senderBalanceReader.Close();
                //. get balacne of the receiver
                string getReceiverBalanceStringCmd =
                    $"SELECT balance from `accounts` WHERE accountNumber = {receiverAccountNumber} and status = {(int) AccountStatus.ACTIVE};";
                var getReceiverBalanceSqlCmd = new MySqlCommand(getReceiverBalanceStringCmd, connection);
                var receiverBalanceReader = getReceiverBalanceSqlCmd.ExecuteReader();
                if (!receiverBalanceReader.Read())
                {
                    receiverBalanceReader.Close();
                    throw new Exception("Tài khoản người nhận không tồn tại hoặc đã bị khóa");
                }

                var receiverBalance = receiverBalanceReader.GetDouble("balance");
                //close reader
                receiverBalanceReader.Close();
                //khởi tạo transaction với trạng thái penđing
                var shbTransactionCode = Guid.NewGuid().ToString();
                var shbTransaction = new SHBTransaction()
                {
                    Code = shbTransactionCode,
                    SenderAccountNumber = senderAccountNumber,
                    ReceiverAccountNumber = receiverAccountNumber,
                    Type = TransactionType.TRANSFER,
                    Amount = amount,
                    Fee = 1100,
                    Message = "Transer " + amount,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Status = TransactionStatus.PENDING
                };
                //lưu transaction pending vào database
                //TODO: change sql cmd
                string insertShbTransactionStringCmd =
                    $"INSERT INTO `shb-transactions` VAlUES ('{shbTransaction.Code}','{shbTransaction.SenderAccountNumber}','{shbTransaction.ReceiverAccountNumber}','{shbTransaction.Message}',{shbTransaction.Amount},{shbTransaction.Fee},'{shbTransaction.CreateAt:yyyy-MM-dd hh:mm:ss}','{shbTransaction.UpdateAt:yyyy-MM-dd hh:mm:ss}',{(int) shbTransaction.Status},{(int) shbTransaction.Type});";
                var insertShbTransactionCmd = new MySqlCommand(insertShbTransactionStringCmd, connection);
                insertShbTransactionCmd.ExecuteNonQuery();
                //update số dư của ng nhận và ng gửi
                senderBalance = senderBalance - amount - shbTransaction.Fee;
                receiverBalance = receiverBalance + amount;
                if (senderBalance < minBalance)
                {
                    //update transaction status to failed -> commit dabatabase -> return false 
                    var updateTime = DateTime.Now;
                    var updateTransactionFailStringCmd =
                        $"UPDATE `shb-transactions` SET status =' {(int) TransactionStatus.FAILED}', updateAt = '{updateTime:yyyy-MM-dd hh:mm:ss}' WHERE code = '{shbTransactionCode}'";
                    var updateTransactionFailCmd = new MySqlCommand(updateTransactionFailStringCmd, connection);
                    var updatedRow = updateTransactionFailCmd.ExecuteNonQuery();
                    if (updatedRow == 0)
                    {
                        throw new Exception("Cập nhật transaction thất bại");
                    }

                    //commit and close connection
                    transaction.Commit();
                    connection.Close();
                    Console.WriteLine("Số dư tài khoản không đủ để thực hiện giao địch");
                    return false;
                }

                //update số dư của ng gửi và nhận trong database
                //ng gửi
                var updateSenderBalanceStringCmd =
                    $"UPDATE accounts SET balance = {senderBalance} WHERE accountNumber = '{senderAccountNumber}';";
                var updateSenderBalanceSqlCmd = new MySqlCommand(updateSenderBalanceStringCmd, connection);
                int updatedSenderRecord = updateSenderBalanceSqlCmd.ExecuteNonQuery();
                if (updatedSenderRecord == 0)
                {
                    throw new Exception("cập nhật số dư mới cho người gửi thất bại");
                }

                //ng nhận
                var updateReceiverBalanceStringCmd =
                    $"UPDATE accounts SET balance = {receiverBalance} WHERE accountNumber = '{receiverAccountNumber}';";
                var updateReceiverBalanceSqlCmd = new MySqlCommand(updateReceiverBalanceStringCmd, connection);
                int updatedReceiverRecord = updateReceiverBalanceSqlCmd.ExecuteNonQuery();
                if (updatedReceiverRecord == 0)
                {
                    throw new Exception("cập nhật số dư mới cho người nhận thất bại");
                }

                //update trạng thái transaction pending -> done
                var updateDoneTime = DateTime.Now;
                string updateShbTransactionStatusCmdString =
                    $"UPDATE `shb-transactions` SET status = {(int) TransactionStatus.DONE}, updateAt = '{updateDoneTime:yyyy-MM-dd hh:mm:ss}' WHERE code = '{shbTransactionCode}'";
                var updateShbTransactionStatusCmd =
                    new MySqlCommand(updateShbTransactionStatusCmdString, connection);
                var transactionUpdated = updateShbTransactionStatusCmd.ExecuteNonQuery();
                if (transactionUpdated == 0)
                {
                    throw new Exception("Cập nhật lịch sử giao dịch thất bại");
                }

                //commit -> close -> return
                transaction.Commit();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Chuyển khoản thất bại " + e.Message);
                //roll back if an error occured in try block
                transaction.Rollback();
                connection.Close();
                return false;
            }
        }
    }
}