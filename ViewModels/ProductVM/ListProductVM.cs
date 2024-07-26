using Store_Management.Data;
using Store_Management.Data.Models;
using Store_Management.Services;
using Store_Management.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.ViewModels.ProductVM
{
    public class ListProductVM : ViewModelBase
    {
        private Navigator Navigator { get; }

        private BookService BookService;
        private PublisherService PublisherService;

        private Book _selectedBook;
        private ObservableCollection<Book> _books;
        private ObservableCollection<Publisher> _publisher;




        #region Properties

        public ObservableCollection<Publisher> Publishers { get => _publisher; set => SetProperty(ref _publisher, value); }
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set { _selectedBook = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Book> Books { get => _books; set => SetProperty(ref _books, value); }

        #endregion

        public ListProductVM()
        {
            Navigator = Navigator.INSTANCE;
            BookService = new BookService();
            PublisherService = new PublisherService();


            ShowProductDetailsCommand = new RelayCommand(async (obj) => await ShowProductDetails(), (obj) => SelectedBook != null);
            GoToAddBookCmd = new RelayCommand(obj => GoToAddBook());
            SearchBookCommand = new(SearchBook);

            //Init
            Task t = Init();
        }

        public async Task Init()
        {
            await LoadDataList();
            this.Publishers = new ObservableCollection<Publisher>(await PublisherService.GetAll());
        }

        public async Task LoadDataList(List<Book>? books = null)
        {
            if (books == null)
            {

                books = await BookService.FindAllBook();
            }
            Books = new ObservableCollection<Book>(books);
        }

        public async Task ShowProductDetails()
        {
            //Navigator.INSTANCE.NavigateTo(new ProductDetailVM(SelectedProduct.Id));

            Navigator.ToUpdateBook(SelectedBook.Id, async bookId => await LoadDataList());

        }


        public async void GoToAddBook()
        {
            Navigator.ToAddBook(async bookId => await LoadDataList());
        }

        public async void SearchBook(object? searchInput)
        {
            string? keyword = searchInput as string;
            if (keyword == null) return;
            var books = await BookService.SearchBooks(keyword);
            await LoadDataList(books);
        }


        public RelayCommand SearchBookCommand { get; set; }
        public RelayCommand ShowProductDetailsCommand { get; set; }
        public RelayCommand GoToAddBookCmd { get; set; }

    }
}
