using Microsoft.EntityFrameworkCore;
using Store_Management.Data;
using Store_Management.Data.Models;
using Store_Management.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Store_Management.Services
{
    public class BookService
    {
        private StoreDbContext Context { get; set; }
        private ImageService ImageService { get; set; } = new ImageService();
        public BookService()
        {
            Context = new StoreDbContext();
        }

        public async Task<int> CountAll()
        {
            using(Context = new StoreDbContext())
            {
                return await Context.Books.CountAsync();
            }
        }
        public async Task<List<Book>> SearchBooks(string searchTerm)
        {
            using (Context = new StoreDbContext())
            {
                int id;
                try
                {
                    id = int.Parse(searchTerm);

                }
                catch (Exception ex)
                {
                    id = -1;
                }
                // Query books using EF Core with accent-insensitive search
                return await Context.Books.Where(b =>
                                    EF.Functions.Collate(b.Name, "Latin1_General_CI_AI")
                                    .Contains(searchTerm)
                                    || b.Id == id
                                    ) // CI: Case-insensitive, AI: Accent-insensitive

                                  .ToListAsync();
            }
        }


        public async Task<Book?> FindBookById(int id)
        {
            using (Context = new StoreDbContext())
            {

                return await Context.Books
                    .Include(book => book.Publisher)
                    .Include(book => book.Category)
                    .Include(book => book.Author)
                    .FirstOrDefaultAsync(book => book.Id == id);
            }
        }

        public async Task<List<Book>> FindAllBook()
        {
            using (Context = new StoreDbContext())
            {

                return await Context.Books
                    .Include(book => book.Publisher)
                    .Include(book => book.Category)
                    .Include(book => book.Author)
                    .ToListAsync();
            }
        }
        public async Task<List<Book>> FindAllAvailableBook()
        {
            using (Context = new StoreDbContext())
            {

                return await Context.Books
                    .Include(book => book.Publisher)
                    .Include(book => book.Category)
                    .Include(book => book.Author)
                    .Where(b => b.IsActive && b.Stock > 0)
                    .ToListAsync();
            }
        }


        public async Task UpdateBookInStock(List<Book> booksToUpdate)
        {
            using (Context = new StoreDbContext())
            {
                foreach (var book in booksToUpdate)
                {
                    // Attach the book to the context
                    Context.Books.Attach(book);

                    // Mark the Stock property as modified
                    Context.Entry(book).Property(b => b.Stock).IsModified = true;
                }

                // Save changes to the database
                await Context.SaveChangesAsync();

            }
        }

        public async Task<bool> AddBook(Book book)
        {
            using (Context = new StoreDbContext())
            {
                book.CreatedDate = DateTime.Now;
                book.CreatedBy = StoreSession.Instance.ActiveEmployee.Id;
                Context.Books.Add(book);
                await Context.SaveChangesAsync();

                book.ImageUrl = UploadBookImage(book.Id, book.ImageUrl);
                await Context.SaveChangesAsync();

                return true;

            }
        }
        public async Task<bool> UpdateBook(Book book)
        {
            var oldBook = await FindBookById(book.Id);
            using (Context = new StoreDbContext())
            {
                if (!oldBook.ImageUrl.Equals(book.ImageUrl))
                {
                    book.ImageUrl = UploadBookImage(book.Id, book.ImageUrl);

                }

                book.UpdatedDate = DateTime.Now;
                book.UpdatedBy = StoreSession.Instance.ActiveEmployee?.Id;

                Context.Update(book);
                return await Context.SaveChangesAsync() > 0;
            }
        }
        public async Task<bool> DeleteBook(Book book)
        {
            using (Context = new StoreDbContext())
            {

                Book? check = await Context.Books.FindAsync(book.Id);
                if (check == null) { throw new Exception("Book not found!"); }

                Context.Books.Remove(book);
                return await Context.SaveChangesAsync() > 0;
            }

        }

        public string UploadBookImage(int bookId, String originalPath)
        {

            string destinationPath = Path.Combine("Products");
            string extension = Path.GetExtension(originalPath);
            return ImageService.Upload(originalPath, destinationPath, $"Book{bookId}{extension}");
        }
    }
}
