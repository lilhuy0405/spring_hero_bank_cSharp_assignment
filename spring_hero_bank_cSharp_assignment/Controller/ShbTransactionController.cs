using System;
using spring_hero_bank_cSharp_assignment.Entity;
using spring_hero_bank_cSharp_assignment.Model;

namespace spring_hero_bank_cSharp_assignment.Controller
{
    public class ShbTransactionController
    {
         private ShbTransactionModel _shbTransactionModel = new ShbTransactionModel();
        private AccountModel _accountModel = new AccountModel();
        
        public void CreateAndSaveDepositTransaction(string accountNumber,double amount)
        {
            var shbTransaction  =new SHBTransaction()
            {
                Code = Guid.NewGuid().ToString(),
                SenderAccountNumber = accountNumber,
                ReceiverAccountNumber =  accountNumber,
                Type = TransactionType.DEPOSIT,
                Amount = amount,
                Fee = 0,
                Message = "Deposit" +amount,
                CreateAt = DateTime.Now,
                // UpdateAt = DateTime.Now,
                Status = TransactionStatus.PENDING
            };

            _shbTransactionModel.InsertNewShbTransaction(shbTransaction);
            var currentBlance = _accountModel.GetCurrentBlanceByAccountNumber(accountNumber);

                

            var statusTransaction =_accountModel.UpdateIncreaseBalanceByAccountNumber(accountNumber,amount);
            if (statusTransaction==true)
            {
                shbTransaction.Status = TransactionStatus.DONE;
                shbTransaction.UpdateAt= DateTime.Now;
            }
            else
            {
                shbTransaction.Status = TransactionStatus.FAILED;
                shbTransaction.UpdateAt= DateTime.Now;
            }
                 
            _shbTransactionModel.UpdateShbTransaction(shbTransaction);
        }

        public void CreateAndSaveWithdrawTransaction(string accountNumber,double amount)
        {
            try
            {
                var shbTransaction  =new SHBTransaction()
                {
                    Code = Guid.NewGuid().ToString(),
                    SenderAccountNumber = accountNumber,
                    ReceiverAccountNumber =  accountNumber,
                    Type = TransactionType.DEPOSIT,
                    Amount = amount,
                    Fee = 0,
                    Message = "Deposit" +amount,
                    CreateAt = DateTime.Now,
                    // UpdateAt = DateTime.Now,
                    Status = TransactionStatus.PENDING
                };
                _shbTransactionModel.InsertNewShbTransaction(shbTransaction);
                var currentBlance = _accountModel.GetCurrentBlanceByAccountNumber(accountNumber);
                var decreaseAmountAfterFee = amount - shbTransaction.Fee;
                var balaneAfterWithdraw = currentBlance - decreaseAmountAfterFee; 
                if (balaneAfterWithdraw < AccountController.MIN_BALANCE )
                {
                    shbTransaction.Status = TransactionStatus.FAILED;
                    shbTransaction.UpdateAt= DateTime.Now;
                    throw  new Exception("Tài khoản của quý khách không đủ để thực hiện giao dịch, vui lòng tạo giao dịch khác.");
                }
                var statusTransaction =_accountModel.UpdateDecreaseBalanceByAccountNumber(accountNumber, balaneAfterWithdraw);
                if (statusTransaction)
                {
                    shbTransaction.Status = TransactionStatus.DONE;
                    shbTransaction.UpdateAt= DateTime.Now;
                }
                else
                {
                    shbTransaction.Status = TransactionStatus.FAILED;
                    shbTransaction.UpdateAt= DateTime.Now;
                }
                 
                _shbTransactionModel.UpdateShbTransaction(shbTransaction);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            
            }
        }
    }
}