using Store_Management.Data.Models;
using Store_Management.Services;
using Store_Management.Utils;
using Store_Management.ViewModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.ViewModels.EmployeeVM
{
    public class EmployeeWindowVM : NavigatableViewModel
    {

        EmployeeService EmployeeService { get; set; }
        public EmployeeWindowVM()
        {
            Navigator.INSTANCE.RegisterNavigationViewModel(this);

            EmployeeService = new EmployeeService();

            if (!EmployeeService.AnyEmployeesExist())
            {
                this.CurrentPage = new SignupVM(Employee.Role.ADMIN);
            }
            else
            {
                this.CurrentPage = new LoginVM();

            }
        }


    }
}
