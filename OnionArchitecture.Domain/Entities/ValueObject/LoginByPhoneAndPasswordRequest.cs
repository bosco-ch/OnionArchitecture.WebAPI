using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Domain.Entities.ValueObject
{
    public record class LoginByPhoneAndPasswordRequest(PhoneNumber number, string Password);
}
