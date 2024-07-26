using Microsoft.EntityFrameworkCore;
using Store_Management.Data;
using Store_Management.Data.Models;
using Store_Management.Services;
using Store_Management.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Store_Management.ViewModels.ProductVM
{
    public class BookDetailVM : ViewModelBase
    {
        public event Action<int> BookChangeEvent;

        //Dependency
        private BookService BookService { get; }
        private PublisherService PublisherService { get; }
        private AuthorService AuthorService { get; }
        private CategoryService CategoryService { get; }


        //
        private ObservableCollection<Publisher> _publisher;
        private ObservableCollection<Author> _authors;
        private ObservableCollection<Category> _categories;


        //Option
        private bool _isUpdate;
        private bool _isAdd;


        private int _id;
        private int _authorId;
        private int _publisherId;
        private string _ISBN;
        private int _publicationYear;
        private int _stock;
        private string _title;
        private int _categoryId;
        private int _numOfPage;
        private string? _pageSize;
        private string _description;
        private decimal _price;
        private string _imageUrl;
        private bool _isActive;

        #region Properties
        public bool IsUpdate { get =>  _isUpdate; set => SetProperty(ref _isUpdate, value); }
        public bool IsAdd { get =>  _isAdd; set => SetProperty(ref _isAdd, value); }
        public ObservableCollection<Publisher> Publishers { get => _publisher; set => SetProperty(ref _publisher, value); }

        public ObservableCollection<Category> Categories { get => _categories; set => SetProperty(ref _categories, value); }
        public ObservableCollection<Author> Authors { get => _authors; set => SetProperty(ref _authors, value); }
        public int Id { get => _id; set => SetProperty(ref _id, value); }
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public decimal Price { get => _price; set => SetProperty(ref _price, value); }
        public string ISBN { get => _ISBN; set => SetProperty(ref _ISBN, value); }
        public int PublicationYear { get => _publicationYear; set => SetProperty(ref _publicationYear, value); }
        public int AuthorId { get => _authorId; set => SetProperty(ref _authorId, value); }
        public int PublisherId { get => _publisherId; set => SetProperty(ref _publisherId, value); }
        public int CategoryId { get => _categoryId; set => SetProperty(ref _categoryId, value); }
        public string? ImageUrl { get => _imageUrl; set => SetProperty(ref _imageUrl, value); }
        public int Stock { get => _stock; set => SetProperty(ref _stock, value); }
        public bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value); }

        public int NumOfPage { get => _numOfPage; set => SetProperty(ref _numOfPage, value); }
        public string BookSize { get => _pageSize; set => SetProperty(ref _pageSize, value); }
        //public bool IsUpdateDisabled { get => _isUpdateDisabled; set => SetProperty(ref _isUpdateDisabled, value); }
        #endregion

        public BookDetailVM(int? bookId = null)
        {

            BookService = new BookService();
            PublisherService = new PublisherService();
            CategoryService = new CategoryService();
            AuthorService = new AuthorService();

            AddBookCommand = new(async obj => await AddBook());
            UpdateBookCommand = new(async obj => await UpdateBook());
            SelectFileCommand = new(obj => SelectImageFile());

            ;

            //Update
            if (bookId != null)
            {
                IsUpdate = true;
                Id = bookId.Value;
                Task init = LoadBookToUpdateForm();

            }
            //Add
            else
            {

                IsAdd = true;
            }
            Init();
        }
        //Command
        public RelayCommand AddBookCommand { get; }
        public RelayCommand UpdateBookCommand { get; }
        public RelayCommand EnableUpdateCmd { get; }

        public RelayCommand SelectFileCommand { get; }
        public RelayCommand SaveChangeCommand { get; set; }

        private async void Init()
        {
            Publishers = new ObservableCollection<Publisher>(await PublisherService.GetAll());
            Authors = new ObservableCollection<Author>(await AuthorService.GetAll());
            Categories = new ObservableCollection<Category>(await CategoryService.GetAll());

        }
        private async Task LoadBookToUpdateForm()
        {
            Book? book = await BookService.FindBookById(Id);

            if (book == null) return;

            Id = book.Id;
            PublisherId = book.PublisherId;
            ISBN = book.ISBN;
            PublicationYear = book.PublicationYear;
            Title = book.Name;
            Description = book.Description;
            Price = book.Price;
            ImageUrl = book.ImageUrl;
            IsActive = book.IsActive;
            NumOfPage = book.NumberOfPage;
            BookSize = book.BookSize;
            CategoryId = book.CategoryId;
            AuthorId = book.AuthorId;
            Stock = book.Stock;

        }

        private Book BookFromViewModel()
        {
            Book book = new Book
            {
                Id = Id,
                PublisherId = PublisherId,
                ISBN = ISBN,
                PublicationYear = PublicationYear,
                Name = Title,
                Description = Description,
                Price = Price,
                ImageUrl = ImageUrl,
                IsActive = IsActive,
                NumberOfPage = NumOfPage,
                BookSize = BookSize,
                CategoryId = CategoryId,
                AuthorId = AuthorId,
                Stock = Stock,


            };
            return book;
        }

        private void SelectImageFile()
        {
            string path = DialogUtil.OpenImagePicker();
            ImageUrl = path;

        }
        private async Task AddBook()
        {
            try
            {

                bool success = await BookService.AddBook(BookFromViewModel());
                if (!success) { throw new Exception(); }
            }
            catch (Exception ex)
            {
                Notification.Error("Add fail!", "Error");
            }
            Notification.Error("Add book successfully!", "Success");
            BookChangeEvent.Invoke(Id);

        }

        private async Task UpdateBook()
        {
            bool isSuccess = await BookService.UpdateBook(BookFromViewModel());
            if (isSuccess)
            {
                Notification.Success("Update success!", "Success");
                BookChangeEvent.Invoke(Id);

            }
            else
            {
                Notification.Error("Update fail!", "Error");
            }
            await LoadBookToUpdateForm();
        }
    }
}
