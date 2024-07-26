using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store_Management.Data;
using Store_Management.Services;
using Store_Management.Utils;
using Store_Management.ViewModels.EmployeeVM;
using Store_Management.Views.EmployeeView;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Store_Management
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        private IConfiguration _configuration;

        public App()
        {
            var services = new ServiceCollection();
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            ConfigureService(services);
            
            _serviceProvider = services.BuildServiceProvider();
        }
        
        private void ConfigureService(IServiceCollection services)
        {

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("StoreDbContext"));
                options.LogTo((msg) => Debug.WriteLine(msg));
            });
           

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            Navigator.CreateInstance();
            Navigator.INSTANCE.OpenEmployeeStartWindow();
            //StoreSession.Instance.ActiveEmployee = new StoreDbContext().Employees.Find(1);
            //Navigator.INSTANCE.OpenStoreMainWindow();

        }

    }

}
