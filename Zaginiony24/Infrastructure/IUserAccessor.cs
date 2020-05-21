using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zaginiony24.Infrastructure
{
    public interface IUserAccessor
    {
        string GetCurrentUsername();
    }
}
