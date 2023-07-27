using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppReorderingProblem.Domain
{
    public interface IChainByPreviousId
    {
        int Id { get; set; }
        int? PreviousDeviceId { get; set; }
    }

    public class RegisteredDevice : IChainByPreviousId
    {
        public int Id { get; set; }

        public int? PreviousDeviceId { get; set; }
        public int? Position { get; set; }

        public string DeviceName { get; set; }
        public string ModelNumber { get; set; }

        public virtual RegisteredDevice? PreviousDevice { get; set; }
    }
}
