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
    public class EmployeeService
    {

        public delegate void EmployeeUpdatedHandler(Employee employee);
        public event EmployeeUpdatedHandler EmployeeUpdated;
        private StoreDbContext Context { get; set; }
        public EmployeeService() { }

        private void StartTransaction()
        {
            Context = new StoreDbContext();
        }
        public string HashPassword()
        {
            return string.Empty;
        }

        public async Task<int> CountActive()
        {
            using (Context = new StoreDbContext())
            {
                return await Context.Employees.Where(e => e.IsActive).CountAsync();
            }
        }
        public async Task<List<Employee>> FindAll(int? excludeId = null)
        {
            StartTransaction();
            return await Context.Employees.Where(e => e.Id != excludeId).ToListAsync();
        }

        public async Task<Employee?> FindByEmail(string email)
        {
            StartTransaction();
            return await Context.Employees.FirstOrDefaultAsync(emp => emp.Email.Equals(email));

        }
        public async Task<Employee?> FindById(int id)
        {
            StartTransaction();
            return await Context.Employees.FirstOrDefaultAsync(emp => emp.Id == id);

        }
        
        public async Task<Employee> Signup(string fullName, string email,string phone, string password,string confirmPassword, Employee.Role role)
        {
            if (!password.Equals(confirmPassword)) throw new Exception("Password not match");

            StartTransaction();
            Employee? check = await FindByEmail(email);
            if (check != null)
            {
                throw new ArgumentException("Employee already exist!");
            }

            Employee employee = new Employee
            {
                FullName = fullName,
                Email = email,
                Password = password,
                PhoneNumber = phone,
                RoleId = (int)role,
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                IsActive = false


            };
            var addedEmp = await AddOne(employee);
            return addedEmp;

        }

        public async Task<Employee> Login(string email, string password)
        {
            StartTransaction();
            var emp = await FindByEmail(email);
            if (emp is null)
            {
                throw new Exception("Employee not exist!");
            }
            PasswordHasheHelper helper = new();
            if (!emp.Password.Equals(password))
            {
                throw new Exception("Password incorrect");
            }
            return emp;
        }

        public async Task DeleteById(int id)
        {
            StartTransaction();
            Employee? emp = await FindById(id) ?? throw new Exception("Employee not found!");
            emp.IsActive = false;
            await Context.SaveChangesAsync();
        }
        public async Task<Employee> AddOne(Employee emp)
        {
            StartTransaction();
            var entity = (await Context.Employees.AddAsync(emp)).Entity;
            Context.SaveChanges();
            return entity;
        }

        public async Task Update(Employee emp)
        {
            StartTransaction();
            var ent = await FindById(emp.Id);
            if (ent is null) { throw new Exception("Employee not found!"); }
            ent.FullName = emp.FullName;
            ent.Address = emp.Address;  
            ent.Email = emp.Email;
            ent.Age = emp.Age;
            ent.PhoneNumber = emp.PhoneNumber;
            ent.ProfileImage = emp.ProfileImage;
            ent.RoleId = emp.RoleId;
            ent.CitizenId =emp.CitizenId;
            ent.UpdatedDate = DateTime.Now;
            ent.UpdatedBy = emp.UpdatedBy;
            ent.IsActive = emp.IsActive;
            
            await Context.SaveChangesAsync();
 
        }
        public bool AnyEmployeesExist()
        {
            StartTransaction();
            return Context.Employees.Any();
        }

        
    }
}
