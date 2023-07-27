using Castle.Core.Configuration;
using ConsoleAppReorderingProblem.Domain;
using ConsoleAppReorderingProblem.Models;
using ConsoleAppReorderingProblem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;


//static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
//{
//    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json", false, true).Build();
//    var connectionString = configuration.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.");

//    services.AddDbContext<DataContext>(options => options
//                .UseLazyLoadingProxies()
//                .UseSqlServer(connectionString)
//                , ServiceLifetime.Transient
//                );

//    services.AddTransient<ISaveDataService, RegisteredDevicesSaveDataService>();
//}

var configuration = new ConfigurationBuilder()
    //.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
    .AddJsonFile("appsettings.test.json", false, true).Build();

var connectionString = configuration.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.");

var services = new ServiceCollection();

services.AddDbContext<DataContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(connectionString)
            , ServiceLifetime.Transient
            );

services.AddTransient<ISaveDataService, RegisteredDevicesSaveDataService>();

var sp = services.BuildServiceProvider();


var _dataContext = sp.GetService<DataContext>();
var applyService = sp.GetService<ISaveDataService>();

_dataContext.Database.Migrate();
_dataContext.SaveChanges();

List<DeviceReorderedDto> changes = new List<DeviceReorderedDto>
{
      new DeviceReorderedDto{
    DeviceId = 3,
    NewPreviousDeviceId = 1
  },
  new DeviceReorderedDto{
    DeviceId = 2,
    NewPreviousDeviceId = 4
  },
  new DeviceReorderedDto{
    DeviceId = 5,
    NewPreviousDeviceId = 2
  }
};

applyService.ApplyChanges(changes);


