using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Domain.Entities.ValueObject
{
    public enum CheckCodeResult
    {
        OK, IsLockOut, PhoneNumberNotFound, LockOut, CodeError
    }
}
