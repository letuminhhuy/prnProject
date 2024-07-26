using Store_Management.Data.Models;
using Store_Management.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Store_Management.ViewModels.AuthorVM
{
    public class AuthorDetailVM : ViewModelBase
    {
        private AuthorService AuthorService { get; } = new AuthorService();

        public AuthorDetailVM()
        {

            SaveCommand = new RelayCommand(Save);
            RefreshCommand = new(obj => { SelectedAuthor = null; });
            LoadAuthorCommand = new(obj => LoadAuthor(SelectedAuthor));

            //
            Init();
        }

        public async void Init()
        {
            Authors = new ObservableCollection<Author>(await AuthorService.GetAll());
        }
        #region Fields

        private int _id;
        private string _name;
        private string? _description;
        private ObservableCollection<Author> _authors;
        private Author? _selectedAuthor;

        #endregion

        #region Properties

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string? Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public ObservableCollection<Author> Authors { get => _authors; set => SetProperty(ref _authors, value); }

        public Author? SelectedAuthor { get => _selectedAuthor; set { SetProperty(ref _selectedAuthor, value); Name = _selectedAuthor?.Name; Description = _selectedAuthor?.Description; } }

        #endregion

        //Command
        public RelayCommand SaveCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand LoadAuthorCommand { get; }


        #region Methods

        private async void Save(object? obj)
        {
            var author = new Author
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description
            };

            if (author.Id == 0)
            {
                await AuthorService.AddAuthorAsync(author);
            }
            else
            {
                await AuthorService.UpdateAuthorAsync(author);
            }

            Authors = new ObservableCollection<Author>(await AuthorService.GetAll());
            SelectedAuthor = null;
        }

        public void LoadAuthor(Author? author)
        {

            if (author != null)
            {
                Id = author.Id;
                Name = author.Name;
                Description = author.Description;
            }
        }

        #endregion
    }
}
