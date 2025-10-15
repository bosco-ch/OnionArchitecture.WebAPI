using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Domain.Entities.ValueObject
{
    public record AppSettings(string Connection, string APIKey);
}
