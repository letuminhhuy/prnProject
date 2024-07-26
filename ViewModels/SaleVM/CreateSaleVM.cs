using Microsoft.IdentityModel.Tokens;
using Store_Management.Data.Models;
using Store_Management.Services;
using Store_Management.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.ViewModels.SaleVM
{
    public class CreateSaleVM : ViewModelBase
    {
        private BookService BookService { get; set; }
        private CustomerService CustomerService { get; set; }
        private SaleService SaleService { get; set; }
        public CreateSaleVM()
        {
            BookService = new BookService();
            SaleService = new SaleService();
            CustomerService = new CustomerService();

            ToAddSaleItemCommand = new(obj =>
            {
                AddSaleItem(SelectedBookToOrder);
            }, (obj) => SelectedBookToOrder != null);

            FinishTransactionCommand = new(async obj =>
            {
                await FinishTransaction();
            }, obj => !SaleItems.IsNullOrEmpty());

            SearchBookCommand = new(SearchBook);


            Init();
        }

        private async void Init()
        {
            Books = new ObservableCollection<Book>(await BookService.FindAllAvailableBook());
        }

        private ObservableCollection<Book> _books;
        private ObservableCollection<SaleItem> _saleItems = new ObservableCollection<SaleItem>();

        private Book _selectedBookToOrder;
        private string? _customerPhone;
        private string? _customerName;
        private int _totalPrice;



        #region Properties
        public ObservableCollection<Book> Books { get => _books; set => SetProperty(ref _books, value); }
        public Book SelectedBookToOrder { get => _selectedBookToOrder; set => SetProperty(ref _selectedBookToOrder, value); }

        public string? CustomerPhone { get => _customerPhone; set => SetProperty(ref _customerPhone, value); }
        public string? CustomerName { get => _customerName; set => SetProperty(ref _customerName, value); }
        public int TotalPrice { get => _totalPrice; set => SetProperty(ref _totalPrice, value); }
        public ObservableCollection<SaleItem> SaleItems { get => _saleItems; set => SetProperty(ref _saleItems, value); }
        #endregion


        public RelayCommand ToAddSaleItemCommand { get; }
        public RelayCommand SearchBookCommand { get; }
        public RelayCommand FinishTransactionCommand {  get; }

        public void AddSaleItem(Book book)
        {
            SaleItem item = Navigator.INSTANCE.ToAddSaleItem(book);
            this.SaleItems.Add(item);
            TotalPrice = SaleService.CalculateTotalPrice(SaleItems.ToList());
        }


        public async Task FinishTransaction()
        {
            Sale sale = new Sale
            {
                EmployeeId = StoreSession.Instance.ActiveEmployee.Id,
                IsActive = true,
                SaleDetails = SaleItems.ToList(),
                TotalPrice = TotalPrice,
            };
            await SaleService.AddTransaction(sale, CustomerPhone, CustomerName);

            Notification.Success("Complete transaction successfully!");
            Navigator.INSTANCE.ToCreateTransaction();
        }
        public async void SearchBook(object? searchInput)
        {
            string? keyword = searchInput as string;
            if (keyword == null) return;
            var books = await BookService.SearchBooks(keyword);
            Books = new ObservableCollection<Book>(books);
        }

    }
}
