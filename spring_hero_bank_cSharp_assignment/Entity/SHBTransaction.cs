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
                   $"Số tài khoản người gửi: {this.SenderAccountNumber}\n" +
                   $"Số tài khoản người nhận: {this.ReceiverAccountNumber}\n" +
                   $"Phí giao dịch: {this.Fee}\n" +
                   $"Số tiền giao dịch: {this.Amount}\n" +
                   $"Ngày tạo: {this.CreateAt}\n" +
                   $"Ngày cập nhật: {this.UpdateAt}\n" +
                   $"Loại giao dịch: {this.Type}\n" +
                   $"Trạng thái giao dịch: {this.Status.ToString()}\n";
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