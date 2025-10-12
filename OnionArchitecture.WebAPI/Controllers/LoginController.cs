using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Domain;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Entities.ValueObject;
using OnionArchitecture.infrastructure;

namespace OnionArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly UserDomainService userDomainService;
        private readonly UserDBContext context;
        public LoginController(UserRepository userRepository, UserDomainService userDomainService)
        {
            this.userRepository = userRepository;
            this.userDomainService = userDomainService;
        }
        /// <summary>
        /// 登录 用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        [UnitOfWork(typeof(UserDBContext))]//这边部分操作是有可能会有savechange的
        public async Task<ActionResult> LoginByPhoneAndPassword([FromBody] LoginByPhoneAndPasswordRequest req)
        {

            var Result = await userDomainService.CheckPasswordAsync(req.number, req.Password);
            //if(userAccessResult == UserAccessResult.PasswordError)
            //{
            //    return BadRequest("password error");
            //}
            switch (Result)
            {
                case UserAccessResult.PasswordError:

                    return BadRequest("Password Error");
                case UserAccessResult.NoPassword:
                    return BadRequest("Password Error");
                case UserAccessResult.PhoneNumberNotFound:
                    return BadRequest("Password Error");
                case UserAccessResult.LockOut:
                    return BadRequest("account is lock");
                case UserAccessResult.OK:
                    return Ok("login");
                default:
                    //return BadRequest("Accident Reason");
                    throw new ApplicationException($"Accident Reason:{Result}");
            }
        }
        [UnitOfWork(typeof(UserDBContext))]
        public async Task<ActionResult> Addnew(PhoneNumber phoneNumber)
        {
            var user = await userRepository.FindOneAsync(phoneNumber);
            if (user != null)
                return BadRequest("this phonenumber have exit");
            ///这种单纯的增删改查 就不要拘泥于 DDD了
            user = new User(phoneNumber);
            context.Add(user);
            context.SaveChanges();
            return Ok("Add Success");
        }

    }
}
