using Store_Management.Data.Models;
using Store_Management.Services;
using Store_Management.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.ViewModels.EmployeeVM
{
    public class EmployeeListVM : ViewModelBase
    {
        private Employee _selectedEmployee;
        private EmployeeService EmployeeService { get; }
        private ObservableCollection<Employee> _employeeList;

        #region Properties
        public Employee SelectedEmployee { get => _selectedEmployee; set => SetProperty(ref _selectedEmployee, value); }
        public ObservableCollection<Employee> EmployeeList
        {
            get { return _employeeList; }
            set { SetProperty(ref _employeeList, value); }
        }
        #endregion
        public EmployeeListVM()
        {
            EmployeeService = new EmployeeService();
            ToEmployeeDetailsCommand = new(ToEmployeeDetailCommandHandler, obj => SelectedEmployee != null);

            Init();
        }

        public void ToEmployeeDetailCommandHandler(object? obj)
        {
            Navigator.INSTANCE.ToEmployeeDetail(SelectedEmployee.Id);
            Init();
        }
        public async void Init()
        {
            EmployeeList = new ObservableCollection<Employee>(await EmployeeService.FindAll(excludeId: StoreSession.Instance.ActiveEmployee.Id));


        }
        public RelayCommand ToEmployeeDetailsCommand { get; set; } 

    }
}
