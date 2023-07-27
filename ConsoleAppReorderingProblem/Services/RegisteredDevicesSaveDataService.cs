using ConsoleAppReorderingProblem.Domain;
using ConsoleAppReorderingProblem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppReorderingProblem.Services
{
    internal class RegisteredDevicesSaveDataService : ISaveDataService
    {
        protected readonly DataContext _dataContext;

        public RegisteredDevicesSaveDataService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void ApplyChanges(List<DeviceReorderedDto> model)
        {
            var localDevices = _dataContext.RegisteredDevices.ToList();
            for (int i = 0; i < model.Count; i++)
            {
                var item = model[i];
                var device = localDevices.Where(a => a.Id == item.DeviceId).First();
                var previousDevice = localDevices.Where(a => a.Id == item.NewPreviousDeviceId).FirstOrDefault();
                device.PreviousDevice = previousDevice;
                //device.PreviousDeviceId = previousDevice?.Id;
            }

            int? previousId = null;
            var liveRecordsCount = localDevices.Where(a => a.Position.HasValue).Count();

            for (int i = 0; i < liveRecordsCount; i++)
            {
                var device = localDevices.Where(a => a.PreviousDeviceId == previousId).First();
                device.Position = i;
                previousId = device.Id;
            }

            localDevices = localDevices.OrderBy(a => a.Position).ToList();

            _dataContext.SaveChanges();
        }
    }
}
