using System;

namespace spring_hero_bank_cSharp_assignment.Entity
{
    public class SHBTransaction
    {
        /*for transaction histories*/
        public string Code { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public string Message { get; set; }
        public double Amount { get; set; }
        public double Fee { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public TransactionStatus Status { get; set; }
        public TransactionType Type { get; set; }
        public override string ToString()
        {
            return $"Mã giao dịch: {this.Code}\n" +
                   $"số tài khoản người gửi {this.SenderAccountNumber}\n" +
                   $"số tài khoản người nhận {this.ReceiverAccountNumber}\n" +
                   $"phí giao dịch {this.Fee}\n" +
                   $"Số tiền giao dịch {this.Amount} ngày tạo {this.CreateAt}\n" +
                   $"ngày cập nhật {this.UpdateAt}\n" +
                   $"loại giao dịch {this.Type}\n" +
                   $"trạng thái giao dịch {this.Status.ToString()}";
        }
    }
    
    public enum TransactionType
    {
        WITHDRAW = 1, //rut tien
        DEPOSIT = 2, // buff tien vao tk
        TRANSFER = 3 //chuyen khoan
    }

    public enum TransactionStatus
    {
        PENDING = 1,
        DONE = 2,
        FAILED = 0
    }
 
}