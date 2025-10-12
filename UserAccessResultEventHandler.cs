using MediatR;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain;
using OnionArchitecture.infrastructure;

namespace OnionArchitecture.WebAPI
{
    public class UserAccessResultEventHandler : INotificationHandler<UserAccessResultEvent>
    {
        private readonly IUserRepository userRepository;
        private readonly DbContext dbContext;

        public UserAccessResultEventHandler(IUserRepository userRepository, DbContext dbContext)
        {
            this.userRepository = userRepository;
            this.dbContext = dbContext;
        }

        public async Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
        {
            await userRepository.AddNewLoginHistoryAsync(notification.phoneNumber, $"登录结果是：{notification.result}");
            //手动调用savechanges
            await dbContext.SaveChangesAsync();
        }
    }
}
