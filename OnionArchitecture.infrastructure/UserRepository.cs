using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using OnionArchitecture.Domain;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Entities.ValueObject;
namespace OnionArchitecture.infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext _dbcontext;
        private readonly IDistributedCache distributedCache;//分布式缓存
        private readonly IMediator mediator;
        public UserRepository(UserDBContext dbContext, IDistributedCache distributedCache, IMediator mediator)
        {
            _dbcontext = dbContext;
            this.distributedCache = distributedCache;
            this.mediator = mediator;
        }
        public async Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string message)
        {
            User? user = await FindOneAsync(phoneNumber);
            Guid? userid = null;
            if (user != null)
            {
                userid = user.Id;
            }
            _dbcontext.userLoginHistories.Add(new UserLoginHistory(userid, phoneNumber, message));
        }

        public async Task<User?> FindOneAsync(PhoneNumber phoneNumber)
        {
            //User user = _dbcontext.Users.SingleOrDefault(b => b.PhoneNumber.Equals(phoneNumber));//比较两个值对象
            //你也可以这么写 User user = _dbcontext.Users.SingleOrDefault(b=> b.PhoneNumber.number.Equals(phoneNumber.number) && b.PhoneNumber.RegionNumber.Equals(phoneNumber.RegionNumber))
            //这边其实在构建表达式，主要是为了完全相等，无非也就是equels
            User? user = await _dbcontext.Users.SingleOrDefaultAsync(ExpressionHelper.ToEquel((User u) => u.PhoneNumber, phoneNumber));
            return user;
        }

        public Task<User?> FindOneAsync(Guid userid)
        {
            User? user = _dbcontext.Users.SingleOrDefault(u => u.Id == userid);
            return Task.FromResult(user);//可以对比上面的findoneasync 参数是phontnumber，一个是使用await，一个是使用task.result
        }

        public async Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber)
        {
            string key = $"phoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.number}";
            string? code = await distributedCache.GetStringAsync(key);
            return code;
        }
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="_event"></param>
        /// <returns></returns>
        public Task PublicEventAsync(UserAccessResultEvent _event)
        {
            return mediator.Publish(_event);
        }
        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code)
        {
            //这边可以保存到数据库里面，也可以放到别的地方例如缓存中
            //这次应用在缓存中
            string key = $"phoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.number}";
            return distributedCache.SetStringAsync(key, code, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }
    }
}



