using Store_Management.Data.Models;
using Store_Management.Utils;
using Store_Management.ViewModels.ProductVM;
using Store_Management.ViewModels.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Store_Management.ViewModels
{
    public class NavigationPanelVM : NavigatableViewModel
    {
        

        private Navigator Navigator {  get; }
        private Employee _currentEmployee;
        public string StoreName { get; set; } = "Book Store";


        private ObservableCollection<NavigationItem> _navigationItems;

        #region Properties

        public Employee CurrentEmployee { get => _currentEmployee; set => SetProperty(ref _currentEmployee, value);}
        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; OnPropertyChanged(); }
        }

        #endregion

        public List<string> STAFF_FUNCTION = new List<string> {"Create transaction", "Profile", "Products Management", "Authors Management" };
        public NavigationPanelVM()
        {

            IEnumerable<NavigationItem> functionList = new List<NavigationItem>
            {
                new("Home", (obj) => Navigator.ToHome(), isActive: true),
                new("Create transaction", (obj) => Navigator.ToCreateTransaction()),
                new("Products Management", (obj) => Navigator.ToProductList()),
                new("Authors Management", (obj) => Navigator.ToAuthorList()),
                new("Transaction history", (obj) => Navigator.ToSaleHistory()),
                new("Work Management", (obj) => Navigator.ToProductList()),
                new("Employess Management", (obj) => Navigator.ToEmployeeList()),
                new("Profile", (obj) => Navigator.ToEmployeeProfile(StoreSession.Instance.ActiveEmployee.Id)),

            };


            Navigator = Navigator.INSTANCE;
            Navigator.RegisterNavigationViewModel(this);

            //Employee
            CurrentEmployee = StoreSession.Instance.ActiveEmployee;
            if(CurrentEmployee.RoleId == (int)Employee.Role.STAFF)
            {
                functionList = functionList.Where(f => STAFF_FUNCTION.Contains(f.Name));
            }

            NavigationItems = new ObservableCollection<NavigationItem>(functionList);
           

        
            //Command
            LogoutCommand = new(obj => Logout());



            if (CurrentEmployee.IsActive == false)
            {
                Notification.Info("Your account is not activated. Please provide all details and contact the admin");
                Navigator.INSTANCE.ToEmployeeProfile(CurrentEmployee.Id);
                return;
            }

            //Inital page
            Navigator.ToHome();

            
        }
        private void Logout()
        {
            StoreSession.Instance.ActiveEmployee = null;
            Navigator.INSTANCE.OpenEmployeeStartWindow(Navigator.OpenAction.CLOSE_CURRENT);
        }
        public RelayCommand LogoutCommand { get; set; }

    }



    public struct NavigationItem
    {
        public NavigationItem(string name, Action<object?> navigateAction, bool isActive = false)
        {
            this.Name = name;
            this.Command = new RelayCommand(navigateAction);
            this.IsActive = isActive;
        }
        public bool IsActive {  get; set; }
        public string Name { get; set; }
        public RelayCommand Command { get; set; }
    }
}

