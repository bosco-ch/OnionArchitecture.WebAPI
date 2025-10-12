using OnionArchitecture.Domain.Entities.ValueObject;

namespace OnionArchitecture.Domain
{
    /// <summary>
    /// 防腐层接口   
    /// </summary>
    public interface ISmsCodeSend
    {
        Task SendAsnyc(PhoneNumber
            phoneNumber, string code);
    }
}
