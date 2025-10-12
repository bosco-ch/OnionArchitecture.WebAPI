using OnionArchitecture.Domain;
using OnionArchitecture.Domain.Entities.ValueObject;

namespace OnionArchitecture.infrastructure
{
    public class MockSmsCodeSend : ISmsCodeSend
    {
        public Task SendAsnyc(PhoneNumber phoneNumber, string code)
        {
            Console.WriteLine($"向{phoneNumber}发送验证码{code}");
            return Task.CompletedTask;
        }
    }
}
