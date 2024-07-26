using Store_Management.Data.Models;
using Store_Management.Services;
using Store_Management.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Store_Management.ViewModels.EmployeeVM
{
    public class SignupVM : ViewModelBase
    {
        EmployeeService EmployeeService { get; set; }

        private Employee.Role RoleToSignUp { get; }

        private string _fullName;
        private string _phone;
        private string _email;
        private string _password;
        public string _confirmPassword;

        #region Properties
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value; OnPropertyChanged();
            }
        }
        public string Phone { get => _phone; set => SetProperty(ref _phone, value); }
        #endregion

        public SignupVM(Employee.Role role = Employee.Role.STAFF)
        {
            RoleToSignUp = role;

            EmployeeService = new EmployeeService();

            OnPasswordChangedCmd = new(OnPasswordChange);
            OnConfirmPasswordChangedCmd = new(OnConfirmPasswordChanged);
            SignUpCmd = new(async obj => await Signup());

        }

        public async Task Signup()
        {
            try
            {

                Employee emp = await EmployeeService.Signup(FullName, Email, Phone, Password, ConfirmPassword, RoleToSignUp);
                Notification.Success("Sign up successfully!");
                StoreSession.Instance.SetActiveEmployee(emp);

                Navigator.INSTANCE.OpenStoreMainWindow(Navigator.OpenAction.CLOSE_CURRENT);
            }
            catch (Exception ex)
            {
                Notification.Error(ex.Message, "Error");
            }
        }

        private void OnPasswordChange(Object? obj)
        {
            PasswordBox box = (PasswordBox)obj!;
            Password = box.Password;
        }
        private void OnConfirmPasswordChanged(Object? obj)
        {
            PasswordBox box = (PasswordBox)obj!;
            ConfirmPassword = box.Password;
        }
        public RelayCommand OnPasswordChangedCmd { get; set; }
        public RelayCommand OnConfirmPasswordChangedCmd { get; set; }
        public RelayCommand ToLoginCmd { get; set; } = new(obj => Navigator.INSTANCE.ToLogin());
        public RelayCommand SignUpCmd { get; set; }
    }
}
