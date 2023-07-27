using ConsoleAppReorderingProblem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppReorderingProblem.Services
{
    internal interface ISaveDataService
    {
        void ApplyChanges(List<DeviceReorderedDto> model);
    }
}
