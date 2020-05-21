using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zaginiony24.Models.View
{
    public class NoticeShortcutVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfDisappearance { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string LastSeenPlace { get; set; }
        public string District { get; set; }
    }
}
