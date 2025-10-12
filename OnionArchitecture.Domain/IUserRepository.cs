using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Entities.ValueObject;

namespace OnionArchitecture.Domain
{
    public interface IUserRepository
    {
        public Task<User?> FindOneAsync(PhoneNumber phoneNumber);
        public Task<User?> FindOneAsync(Guid userid);
        public Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string message);
        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code);
        public Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber);
        public Task PublicEventAsync(UserAccessResultEvent _event);
    }
}
