using Microsoft.EntityFrameworkCore;
using Store_Management.Data;
using Store_Management.Data.Models;
using Store_Management.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Services
{
    public class CustomerService
    {

        StoreDbContext Context = new StoreDbContext();

        public CustomerService() { }

        public Task<List<Customer>> GetAll()
        {
            using(Context = new StoreDbContext())
            {
                return Context.Customers.ToListAsync();
            }
        }

        public async Task<Customer?> GetByPhone(string phone) {
            using var context = new StoreDbContext();
            return await context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber.Equals(phone));

        }
        public async Task Create(Customer customer)
        {
           
            using(Context = new StoreDbContext())
            {


                customer.CreatedDate = DateTime.Now;
                customer.CreatedBy = StoreSession.Instance.ActiveEmployee.Id;


                Context.Customers.Add(customer);
                await Context.SaveChangesAsync();
            }
        }
    }
}
