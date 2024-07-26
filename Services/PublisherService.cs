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
    public class PublisherService
    {
        private StoreDbContext Context { get; set; }
        public PublisherService()
        {

        }

        public async Task<List<Publisher>> GetAll()
        {
            Context = new StoreDbContext();
            return await Context.Publishers.ToListAsync();
        }
    }
}
