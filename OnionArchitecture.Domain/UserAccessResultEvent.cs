using MediatR;
using OnionArchitecture.Domain.Entities.ValueObject;

namespace OnionArchitecture.Domain
{
    public record UserAccessResultEvent(PhoneNumber phoneNumber, UserAccessResult result) : INotification;
}
