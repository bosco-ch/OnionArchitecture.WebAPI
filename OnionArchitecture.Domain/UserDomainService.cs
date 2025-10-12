using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Entities.ValueObject;

namespace OnionArchitecture.Domain
{
    public class UserDomainService
    {
        private IUserRepository userRepository;
        private ISmsCodeSend smsCodeSend;
        public UserDomainService(IUserRepository userRepository, ISmsCodeSend smsCodeSend)
        {
            this.userRepository = userRepository;
            this.smsCodeSend = smsCodeSend;
        }
        #region 
        public void ResetAccessFail(User user)
        {
            user.UserAccessFail.Reset();
        }
        public bool IsLockOut(User user)
        {
            return user.UserAccessFail.IsLocked();
        }
        public void AccessFail(User user)
        {
            user.UserAccessFail.Fail();
        }
        #endregion
        public async Task<UserAccessResult> CheckPasswordAsync(PhoneNumber number, string password)
        {
            UserAccessResult result;
            User? user = await userRepository.FindOneAsync(number);
            if (user == null)
            {
                result = UserAccessResult.PhoneNumberNotFound;
            }
            else if (IsLockOut(user))
            {
                result = UserAccessResult.LockOut;
            }
            else if (user.HasPassWord())
            {
                result = UserAccessResult.NoPassword;
            }
            else if (user.CheckPassword(password))
            {
                result = UserAccessResult.OK;
            }
            else
            {
                result = UserAccessResult.PasswordError;
            }
            if (user != null)
            {
                if (result == UserAccessResult.OK)
                {
                    ResetAccessFail(user);
                }
                else
                {
                    AccessFail(user);
                }
            }
            await userRepository.PublicEventAsync(new UserAccessResultEvent(number, result));//发布事件
            return result;
        }
        public async Task<CheckCodeResult> CheckCodeAsync(PhoneNumber number, String code)
        {
            CheckCodeResult result;
            User? user = await userRepository.FindOneAsync(number);
            if (user == null)
            {
                result = CheckCodeResult.PhoneNumberNotFound;
            }
            else if (IsLockOut(user))
            {
                result = CheckCodeResult.LockOut;
            }
            else
            {
                string? codeServer = await userRepository.FindPhoneNumberCodeAsync(number);
                if (codeServer == null)
                {
                    AccessFail(user);
                    result = CheckCodeResult.CodeError;
                }
                else
                {
                    if (codeServer == code)
                    {
                        result = CheckCodeResult.OK;
                    }
                    else
                    {
                        AccessFail(user);
                        result = CheckCodeResult.CodeError;
                    }
                }
            }
            //await userRepository.PublicEventAsync(new UserAccessResultEvent(number, result))
            return result;
        }

    }
}
