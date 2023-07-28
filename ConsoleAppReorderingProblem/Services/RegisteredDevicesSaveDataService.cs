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
            _dataContext.RegisteredDevices
                .Where(device => model.Select(dto => dto.DeviceId).Contains(device.Id))
                .ExecuteUpdate(up => up.SetProperty(device => device.PreviousDeviceId, (int?)null));
            var changedDevices = _dataContext.RegisteredDevices
                .Where(device => model.Select(dto => dto.DeviceId).Contains(device.Id))
                .ToList();
            for (int i = 0; i < model.Count; i++)
            {
                var item = model[i];
                var device = changedDevices.Where(a => a.Id == item.DeviceId).First();
                device.PreviousDeviceId = item.NewPreviousDeviceId;
            }
            _dataContext.SaveChanges();
            _dataContext.ChangeTracker.Clear();
            var localDevices = _dataContext.RegisteredDevices.ToList();
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
