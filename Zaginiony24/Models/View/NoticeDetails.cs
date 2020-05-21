using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zaginiony24.Models.View
{
    public class NoticeDetails
    {
        public Notice Notice { get; set; }
        public UserInfo User { get; set; }
    }
}
