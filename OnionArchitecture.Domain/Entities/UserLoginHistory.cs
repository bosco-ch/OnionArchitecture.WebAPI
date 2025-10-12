using OnionArchitecture.Domain.Entities.ValueObject;

namespace OnionArchitecture.Domain.Entities
{
    /// <summary>
    /// 用户历史登录信息
    /// </summary>
    public record UserLoginHistory : IAggregateRoot
    {
        public long id { get; init; }
        public Guid? UserId { get; init; }//另一个聚合根的主键
        public PhoneNumber phoneNumber { get; private set; }
        public DateTime CreateTime { get; init; }
        public string? Message { get; init; }
        public UserLoginHistory() { }
        public UserLoginHistory(Guid? userid, PhoneNumber phonenumber, string message)
        {
            this.phoneNumber = phonenumber;
            this.Message = message;
            CreateTime = DateTime.Now;
            this.UserId = userid;
        }
    }
}
