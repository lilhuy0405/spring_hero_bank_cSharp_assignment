namespace spring_hero_bank_cSharp_assignment.Entity
{
    public class Account
    {
        public string FullName { get; set; }
        public string AccountNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
        public string Username { get; set; }
        public AccountRole Role { get; set; }
        public AccountStatus Status { get; set; }
        public double Balance { get; set; }

        public override string ToString()
        {
            return
                $"Họ và tên: {FullName}\n" +
                $"Số tài khoản: {AccountNumber}\n" +
                $"Số điện thoại: {PhoneNumber}\n" +
                $"Email: {Email}\n" +
                $"Salt: {Salt}\n" +
                $"PasswordHash: {PasswordHash}\n" +
                $"Username: {Username}\n" +
                $"Quyền truy cập: {Role}\n" +
                $"Tình trạng: {Status}\n" +
                $"Số dư: {Balance}\n";
        }
    }

    public enum AccountStatus
    {
        ACTIVE = 1,
        DEACTIVE = -1,
        LOCKED = 0
    }

    public enum AccountRole
    {
        ADMIN = 1,
        GUEST = 0
    }
}