using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppReorderingProblem.Models
{
    public class DeviceReorderedDto
    {
        public int DeviceId { get; set; }
        public int? NewPreviousDeviceId { get; set; }
    }
}
