using Microsoft.EntityFrameworkCore;
using Store_Management.Data;
using Store_Management.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Services
{
    public class AuthorService
    {
        private StoreDbContext Context { get; set; }

        public async Task<List<Author>> GetAll()
        {
            using (Context = new StoreDbContext())
            {
                return await Context.Authors.ToListAsync();
            }
        }
        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            using (Context = new StoreDbContext())
            {
                return await Context.Authors.FindAsync(id);
            }
        }

        public async Task AddAuthorAsync(Author author)
        {
            using (Context = new StoreDbContext())
            {

                Context.Authors.Add(author);
                await Context.SaveChangesAsync();
            }
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            using (Context = new StoreDbContext())
            {

                Context.Authors.Update(author);
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteAuthorAsync(int id)
        {

            using (Context = new StoreDbContext())
            {

                var author = await Context.Authors.FindAsync(id);
                if (author != null)
                {
                    Context.Authors.Remove(author);
                    await Context.SaveChangesAsync();
                }
            }
        }
    }
}
