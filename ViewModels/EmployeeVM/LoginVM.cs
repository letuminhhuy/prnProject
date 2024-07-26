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
    public class LoginVM : ViewModelBase
    {

        private string _email;
        private string _password;

        #region Properties
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }


        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        #endregion

        private EmployeeService Service { get; set; }
        public LoginVM()
        {
            Service = new EmployeeService();


            OnPasswordChangedCmd = new RelayCommand(obj => OnPasswordChange(obj));
            LoginCommand = new RelayCommand(obj => Login());
        }
        private async void Login()
        {
            try
            {
                Employee emp = await Service.Login(Email, Password);
                if (emp == null)
                {
                    throw new Exception("Account not exist! Please contact the admin");
                }
                Notification.Success("Login successfully!");
                StoreSession.Instance.SetActiveEmployee(emp);
                Navigator.INSTANCE.OpenStoreMainWindow(Navigator.OpenAction.CLOSE_CURRENT);
            }
            catch (Exception ex)
            {
                Notification.Error(ex.Message, "Error");
            }


        }

        private void OnPasswordChange(Object obj)
        {
            PasswordBox passwordBox = (PasswordBox)obj;
            Password = passwordBox.Password;
        }

        public RelayCommand OnPasswordChangedCmd {  get; set; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand ToSignUp { get; set; } = new RelayCommand((obj) => Navigator.INSTANCE.ToSignUp());
        public RelayCommand ToForgotPassword { get; set; } = RelayCommand.From((obj) => Navigator.INSTANCE.OpenDialog(new SignupVM()));

    }
}
