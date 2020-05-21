using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Zaginiony24.Infrastructure
{
    public class ApiResult<TResult>
    {
        public TResult Result { get; set; }
        public object Errors { get; set; }
        public bool IsSuccess => Errors == null;

        public ApiResult(object errors = null)
        {
            Errors = errors;
        }

    }
}
