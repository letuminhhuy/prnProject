using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Store_Management.Data.Models;
using Store_Management.ViewModels;
using Store_Management.ViewModels.AuthorVM;
using Store_Management.ViewModels.EmployeeVM;
using Store_Management.ViewModels.ProductVM;
using Store_Management.ViewModels.SaleVM;
using Store_Management.ViewModels.Utils;
using Store_Management.Views;
using Store_Management.Views.EmployeeView;

namespace Store_Management.Utils
{
    public class Navigator
    {
        public static Navigator INSTANCE { get; private set; } = null!;
        private NavigatableViewModel? _navigationVM { get; set; }

        private Frame Frame { get; set; }

        public Window? HostWindow { get; private set; }
        public Navigator(Window? hostWindow = null)
        {
            if (hostWindow != null)
            {
                HostWindow = hostWindow;
            }
        }

        public void RegisterFrame(Frame frame)
        {
            this.Frame = frame;
        }

        public enum OpenAction
        {
            DEFAULT,
            CLOSE_CURRENT
        }

        public void OpenDialog(ViewModelBase vm)
        {
            Window window = new Window();
            window.Owner = HostWindow;
            window.Width = HostWindow.Width * 0.8d;
            window.Height = HostWindow.Height * 0.8d;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Content = vm;
            window.ShowDialog();
        }
        public void OpenWindow(ViewModelBase vm)
        {
            var window = new Window();
            window.Content = vm;

            window.Show();
        }

        public void OpenWindow(Window window)
        {
            window.Show();
        }
        public static void CreateInstance(Window? host = null)
        {
            INSTANCE = new Navigator(host);
        }


        #region Windows
        public void OpenEmployeeStartWindow(OpenAction? openAction = OpenAction.DEFAULT)
        {
            EmployeeWindow window = new EmployeeWindow(new EmployeeWindowVM());
            window.Show();
            if (openAction == OpenAction.CLOSE_CURRENT) HostWindow?.Close();
            this.HostWindow = window;

        }

        public void OpenStoreMainWindow(OpenAction? openAction = OpenAction.DEFAULT)
        {
            Window window = new MainWindow(new NavigationPanelVM());
            window.Show();
            if (openAction == OpenAction.CLOSE_CURRENT) HostWindow?.Close();
            this.HostWindow = window;

        }
        #endregion

        #region Pages
        public void ToSignUp()
        {
            _navigationVM.CurrentPage = new SignupVM();
        }
        public void ToUpdateBook(int id, Action<int>? onAddSuccessHandler = null)
        {
            var vm = new BookDetailVM(id);
            vm.BookChangeEvent += onAddSuccessHandler;
            Window window = new Window();
            window.Owner = HostWindow;
            window.Content = vm;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Show();
        }

        public void ToAddBook(Action<int>? onAddSuccessHandler = null)
        {
            var vm = new BookDetailVM();
            vm.BookChangeEvent += onAddSuccessHandler;

            Window window = new Window();
            window.Owner = HostWindow;
            window.Content = vm;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Show();

        }


        public void ToEmployeeProfile(int id)
        {
            _navigationVM.CurrentPage = new EmployeeDetailsVM(id);
        }

        public void ToEmployeeDetail(int id)
        {
            var vm = new EmployeeDetailsVM(id);
            Window window = new Window();
            window.Owner = HostWindow;
            window.Width = 800;
            window.Height = 450;
            window.Content = vm;
            window.ShowDialog();
        }

        public void RegisterNavigationViewModel(NavigatableViewModel vm)
        {
            this._navigationVM = vm;
        }

        public void ToLogin()
        {
            _navigationVM.CurrentPage = new LoginVM();
        }

        public void ToHome()
        {
            _navigationVM.CurrentPage = new HomeVM();
        }

        public void ToProductList()
        {
           _navigationVM.CurrentPage = new ListProductVM();
        }

        public void ToEmployeeList()
        {
            _navigationVM.CurrentPage = new EmployeeListVM();
        }

        public void ToAuthorList()
        {
            _navigationVM.CurrentPage = new AuthorDetailVM();
        }
        public void ToCreateTransaction()
        {
            _navigationVM.CurrentPage = new CreateSaleVM();
        }
        public SaleItem ToAddSaleItem(Book book)
        {
            var vm = new CreateSaleItemVM(book);
            Window window = new Window();
            window.Owner = HostWindow;
            window.Content = vm;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            vm.OnCloseDialog += () => window.DialogResult = true;
            if (window.ShowDialog() == true)
            {
                return vm.CreatedSaleItem;
            }
            return null;
        }


        public void ToSaleHistory()
        {
            _navigationVM.CurrentPage = new SaleHistoryVM();
        }
        #endregion

    }
}
