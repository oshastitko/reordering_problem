using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleAppReorderingProblem.Domain
{
    public class RegisteredDeviceMap : IEntityTypeConfiguration<RegisteredDevice>
    {
        public void Configure(EntityTypeBuilder<RegisteredDevice> builder)
        {
            builder.ToTable(nameof(RegisteredDevice));
            builder.HasKey(p => p.Id);

            builder.HasOne(a => a.PreviousDevice)
                .WithOne()
                .HasForeignKey<RegisteredDevice>(p => p.PreviousDeviceId)
                .OnDelete(DeleteBehavior.NoAction);

            string data = "[{\"Id\":1,\"ModelNumber\":\"20I\",\"CompanyName\":\"J. J. Keller \\u0026 Associates, Inc.\",\"DeviceName\":\"J. J. Keller ELD - iOS 2.0\",\"PreviousDeviceId\":null,\"Position\":0},{\"Id\":2,\"ModelNumber\":\"25I\",\"CompanyName\":\"J. J. Keller \\u0026 Associates, Inc.\",\"DeviceName\":\"J. J. Keller ELD - iOS 2.5\",\"PreviousDeviceId\":1,\"Position\":1},{\"Id\":3,\"ModelNumber\":\"FLT3\",\"CompanyName\":\"HOS247 LLC\",\"DeviceName\":\"#1 ELD by HOS247\",\"PreviousDeviceId\":2,\"Position\":2},{\"Id\":4,\"ModelNumber\":\"N775G\",\"CompanyName\":\"XPERT-IT SOLUTIONS INC.\",\"DeviceName\":\"Xpert ELD\",\"PreviousDeviceId\":3,\"Position\":3},{\"Id\":5,\"ModelNumber\":\"PMG001\",\"CompanyName\":\"PeopleNet\",\"DeviceName\":\"PeopleNet Mobile Gateway - Trimble Driver ELD\",\"PreviousDeviceId\":4,\"Position\":4}]";

            var devices = (List<RegisteredDevice>)JsonSerializer.Deserialize(data, typeof(List<RegisteredDevice>));
            int id = 1;
            int? previousDeviceId = null;
            foreach (var device in devices)
            {
                builder.HasData(new RegisteredDevice
                {
                    Id = id,
                    DeviceName = device.DeviceName,
                    ModelNumber = device.ModelNumber,
                    Position = id - 1,
                    PreviousDeviceId = previousDeviceId
                });
                id++;
                previousDeviceId = device.Id;
            }
        }
    }
}
