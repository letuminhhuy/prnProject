using Microsoft.EntityFrameworkCore;
using Store_Management.Data;
using Store_Management.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;


namespace Store_Management.Services
{
    public class ProductService
    {
        private StoreDbContext _context;


        public ProductService(StoreDbContext context)
        {
            _context = context;

        }
        public ProductService()
        {
            _context = new StoreDbContext();

        }
    

        public async Task<List<Book>> GetAllBooks()
        {
            var products = await _context.Books.ToListAsync();
            return products;
        }



        public async Task AddBook(Book book)
        {
            book.CreatedDate = DateTime.Now;
            book.CreatedBy = 1;
            await _context.AddAsync(book);
            _context.SaveChanges();

        }
        public async Task<bool> UpdateBook(Book book)
        {
            try
            {
                book.UpdatedDate = DateTime.Now;
                book.UpdatedBy = 1;
                _context.Books.Update(book);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }



    }
}