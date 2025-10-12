using OnionArchitecture.Domain.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zack.Commons;

namespace OnionArchitecture.Domain.Entities
{
    public record User : IAggregateRoot
    {
        public Guid Id { get; init; }
        public PhoneNumber PhoneNumber { get; private set; }
        private string? passwordHash;
        public UserAccessFail UserAccessFail { get; private set; }
        private User()
        {

        }
        public User(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
            this.Id = Guid.NewGuid();
            this.UserAccessFail = new UserAccessFail(this);
        }
        public bool HasPassWord()
        {
            return !string.IsNullOrEmpty(this.passwordHash);
        }
        public void ChangePassword(string password)
        {
            if (password.Length <= 3)
            {
                throw new ArgumentException("the length of words mush bigger than 3");
            }
            this.passwordHash = HashHelper.ComputeMd5Hash(password);
        }
        public bool CheckPassword(string password)
        {
            return this.passwordHash == HashHelper.ComputeMd5Hash(password);
        }
        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }
    }

}
