using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store_Management.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Data
{
    public class StoreDbContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookSerialNumber> BookSerialNumber { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeePaymentRecord> EmployeePaymentRecord { get; set; }
        public virtual DbSet<KPIRecord> KPIRecords { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SaleItem> SaleItem { get; set; }



        public StoreDbContext()
        {

        }
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true)
               .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("StoreDbContext"));
            optionsBuilder.LogTo((msg) => Debug.WriteLine(msg));

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>()
                .HasOne(cate => cate.Parent)
                .WithMany(cate => cate.SubCategories)
                .HasForeignKey(cate => cate.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            #region Author
            modelBuilder.Entity<Author>().HasData(
             (new Author { Id = 1, Name = "Nguyễn Nhật Ánh", Description = "Popular contemporary novelist and screenwriter." }),
         (new Author { Id = 2, Name = "Nguyễn Hồng", Description = "Acclaimed poet and author of modern Vietnamese literature." }),
          (new Author { Id = 3, Name = "Nguyễn Thành Long", Description = "Novelist known for exploring social issues in Vietnam." }),
         (new Author { Id = 4, Name = "Nguyễn Phan Quế Mai", Description = "Award-winning author of novels and poetry, focusing on Vietnamese history and culture." }),
         (new Author { Id = 5, Name = "Lê Minh Khuê", Description = "Young novelist and short story writer known for contemporary themes." }),
         (new Author { Id = 6, Name = "Bùi Anh Tấn", Description = "Modern poet and essayist exploring existential and philosophical themes." }),
         (new Author { Id = 7, Name = "Phan Hồn Nhân", Description = "Contemporary writer of short stories and novels reflecting on urban life." }),
         (new Author { Id = 8, Name = "Trịnh Bích Ngân", Description = "Novelist and playwright known for feminist themes in Vietnamese literature." })
              );
            #endregion

            #region Employees
            modelBuilder.Entity<Employee>()
                .HasData(
                    new Employee
                    {
                        Id = 1,
                        FullName = "Le Trung Ha",
                        RoleId = (int)Employee.Role.ADMIN,
                        CitizenId = "0049393859",
                        Age = 30,
                        CreatedDate = DateTime.Now,
                        CreatedBy = 1,
                        IsActive = true,
                        Email = "admin@gmail.com",
                        Address = "Ha Noi, Viet Nam",
                        PhoneNumber = "094859404",
                        Password = "123123"
                    },
                     new Employee
                     {
                         Id = 2,
                         FullName = "Tran Van B",
                         RoleId = (int)Employee.Role.STAFF,
                         CitizenId = "0049393859",
                         Age = 30,
                         CreatedDate = DateTime.Now,
                         CreatedBy = 1,
                         IsActive = true,
                         Email = "c@gmail.com",
                         Address = "Ha Noi, Viet Nam",
                         PhoneNumber = "094859404",
                         Password = "123123"
                     },
                     new Employee
                     {
                         Id = 3,
                         FullName = "Nguyen Van A",
                         RoleId = (int)Employee.Role.STAFF,
                         CitizenId = "0049393859",
                         Age = 30,
                         CreatedDate = DateTime.Now,
                         CreatedBy = 1,
                         IsActive = true,
                         Email = "b@gmail.com",
                         Address = "Ha Noi, Viet Nam",
                         PhoneNumber = "094859404",
                         Password = "123123"
                     },
                     new Employee
                     {
                         Id = 4,
                         FullName = "Nguyen Thi C",
                         RoleId = (int)Employee.Role.STAFF,
                         CitizenId = "0049393859",
                         Age = 30,
                         CreatedDate = DateTime.Now,
                         CreatedBy = 1,
                         IsActive = false,
                         Email = "a@gmail.com",
                         Address = "Ha Noi, Viet Nam",
                         PhoneNumber = "094859404",
                         Password = "123123"
                     }
                );
            #endregion

            #region Publisher
            modelBuilder.Entity<Publisher>()
                .HasData(
                   new Publisher
                   {
                       Id = 1,
                       Name = "NXB Kim Đồng",
                       CreatedBy = 1,
                       CreatedDate = DateTime.Now
                   },
                new Publisher
                {
                    Id = 2,
                    Name = "NXB Giáo Dục",
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now
                },
                new Publisher
                {
                    Id = 3,
                    Name = "NXB Trẻ",
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now
                },
                new Publisher
                {
                    Id = 4,
                    Name = "NXB Hội Nhà Văn",
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now
                },
                new Publisher
                {
                    Id = 5,
                    Name = "NXB Văn Học",
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now
                }
                );
            #endregion

            #region Category

            modelBuilder.Entity<Category>().HasData(
      new Category { Id = 1, Name = "English Book", ParentId = null },
      new Category { Id = 2, Name = "Programming", ParentId = null },
      new Category { Id = 3, Name = "Science Fiction", ParentId = null },
      new Category { Id = 4, Name = "Fantasy", ParentId = null },
      new Category { Id = 5, Name = "Mystery", ParentId = null },
      new Category { Id = 6, Name = "Biography", ParentId = null },
      new Category { Id = 7, Name = "History", ParentId = null },
      new Category { Id = 8, Name = "Art", ParentId = null },

      // Sub-categories
      new Category { Id = 9, Name = "Java", ParentId = 2 },
      new Category { Id = 10, Name = "C#", ParentId = 2 },
      new Category { Id = 11, Name = "Python", ParentId = 2 },
      new Category { Id = 12, Name = "Space Opera", ParentId = 3 },
      new Category { Id = 13, Name = "High Fantasy", ParentId = 4 },
      new Category { Id = 14, Name = "Detective", ParentId = 5 },
      new Category { Id = 15, Name = "Autobiography", ParentId = 6 },
      new Category { Id = 16, Name = "World War II", ParentId = 7 },
      new Category { Id = 17, Name = "Painting", ParentId = 8 }
  );
            #endregion

            #region Book
            modelBuilder.Entity<Book>().HasData(
                 new Book
                 {
                     Id = 1,
                     PublisherId = 1,
                     ISBN = "BK235",
                     CategoryId = 1,
                     PublicationYear = 2025,
                     Name = "The Great Gatsby",
                     AuthorId = 1,
                     Description = "Classic novel by F. Scott Fitzgerald",
                     BookSize = "15.9 x 1.3 x 22.2",
                     NumberOfPage = 30,
                     Price = 150000,
                     IsActive = true,
                     Stock = 50,
                     ImageUrl = "the_great_gatsby.jpg",
                     CreatedBy = 1,
                     CreatedDate = DateTime.Now
                 },
                 new Book
                 {
                     Id = 2,
                     PublisherId = 2,
                     ISBN = "BK236",
                     CategoryId = 9, // Java
                     PublicationYear = 2024,
                     Name = "Effective Java",
                     AuthorId = 2,
                     Description = "Comprehensive guide to best practices in Java programming.",
                     BookSize = "16.0 x 2.0 x 23.0",
                     NumberOfPage = 400,
                     Price = 450000,
                     IsActive = true,
                     Stock = 100,
                     ImageUrl = "effective_java.jpg",
                     CreatedBy = 1,
                     CreatedDate = DateTime.Now
                 },
                 new Book
                 {
                     Id = 3,
                     PublisherId = 1,
                     ISBN = "BK237",
                     CategoryId = 10, // C#
                     PublicationYear = 2023,
                     Name = "Pro C# 8.0",
                     AuthorId = 3,
                     Description = "Deep dive into C# programming with professional tips and tricks.",
                     BookSize = "18.0 x 3.0 x 24.0",
                     NumberOfPage = 1200,
                     Price = 550000,
                     IsActive = true,
                     Stock = 75,
                     ImageUrl = "pro_csharp_8.jpg",
                     CreatedBy = 1,
                     CreatedDate = DateTime.Now
                 },
                 new Book
                 {
                     Id = 4,
                     PublisherId = 2,
                     ISBN = "BK238",
                     CategoryId = 12, // Space Opera
                     PublicationYear = 2021,
                     Name = "Dune",
                     AuthorId = 4,
                     Description = "Epic science fiction novel by Frank Herbert.",
                     BookSize = "14.0 x 2.5 x 21.0",
                     NumberOfPage = 900,
                     Price = 60000,
                     IsActive = true,
                     Stock = 200,
                     ImageUrl = "dune.jpg",
                     CreatedBy = 1,
                     CreatedDate = DateTime.Now
                 },
                 new Book
                 {
                     Id = 5,
                     PublisherId = 4,
                     ISBN = "BK239",
                     CategoryId = 14, // Detective
                     PublicationYear = 2022,
                     Name = "Sherlock Holmes: The Complete Novels and Stories",
                     AuthorId = 5,
                     Description = "All the classic tales of Sherlock Holmes by Arthur Conan Doyle.",
                     BookSize = "15.0 x 3.5 x 23.0",
                     NumberOfPage = 2200,
                     Price = 980000,
                     IsActive = true,
                     Stock = 80,
                     ImageUrl = "sherlock_holmes.jpg",
                     CreatedBy = 1,
                     CreatedDate = DateTime.Now
                 },
                 new Book
                 {
                     Id = 6,
                     PublisherId = 4,
                     ISBN = "BK240",
                     CategoryId = 15, // Autobiography
                     PublicationYear = 2020,
                     Name = "Becoming",
                     AuthorId = 6,
                     Description = "Michelle Obama's deeply personal memoir.",
                     BookSize = "16.0 x 2.0 x 24.0",
                     NumberOfPage = 400,
                     Price = 500000,
                     IsActive = true,
                     Stock = 120,
                     ImageUrl = "becoming.jpg",
                     CreatedBy = 1,
                     CreatedDate = DateTime.Now
                 },
                 new Book
                 {
                     Id = 7,
                     PublisherId = 4,
                     ISBN = "BK241",
                     CategoryId = 16, // World War II
                     PublicationYear = 2019,
                     Name = "The Second World War",
                     AuthorId = 7,
                     Description = "Comprehensive history of World War II by Antony Beevor.",
                     BookSize = "17.0 x 2.5 x 25.0",
                     NumberOfPage = 800,
                     Price = 2000000,
                     IsActive = true,
                     Stock = 50,
                     ImageUrl = "the_second_world_war.jpg",
                     CreatedBy = 1,
                     CreatedDate = DateTime.Now
                 },
                 new Book
                 {
                     Id = 8,
                     PublisherId = 4,
                     ISBN = "BK242",
                     CategoryId = 17, // Painting
                     PublicationYear = 2018,
                     Name = "The Art Book",
                     AuthorId = 8,
                     Description = "A comprehensive introduction to art and artists from around the world.",
                     BookSize = "20.0 x 3.0 x 28.0",
                     NumberOfPage = 600,
                     Price = 85000,
                     IsActive = true,
                     Stock = 30,
                     ImageUrl = "the_art_book.jpg",
                     CreatedBy = 1,
                     CreatedDate = DateTime.Now
                 }
 );


            #endregion


        }
    }
}
