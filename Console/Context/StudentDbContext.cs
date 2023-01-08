using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.Models;

namespace Console.Context
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext (): base("StudentDbContext")
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // way 2 to build one-to-one relationship, fluent api
            modelBuilder.Entity<Student>().HasOptional(s => s.StudentDetail);
            modelBuilder.Entity<StudentDetail>().HasRequired(s => s.Student);

            //modelBuilder.Entity<Post>().HasRequired(post => post.Blog).WithMany(blog => blog.Posts);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentDetail> StudentDetails { get; set; }
        //public DbSet<Blog> Blogs { get; set; }
        //public DbSet<Post> Posts { get; set;}
    }

    public class DatabaseInitializer : CreateDatabaseIfNotExists<StudentDbContext>
    {
        protected override void Seed(StudentDbContext context)
        {
            context.Students.AddRange(new List<Student>()
            {
                new Student() {Name = "Andy", Gender = true, Age = 29},
                new Student() {Name = "John", Gender = true, Age = 26},
                new Student() {Name = "Joan", Gender = false, Age = 32},
                new Student() {Name = "Mike", Gender = true, Age = 22},
                new Student() {Name = "Nicky", Gender = false, Age = 19},
                new Student() {Name = "Cindy", Gender = false, Age = 52},
                new Student() {Name = "Yanny", Gender = false, Age = 23},
                new Student() {Name = "Tony", Gender = true, Age = 42},
                new Student() {Name = "Michelle", Gender = false, Age = 33},
                new Student() {Name = "Bill", Gender = true, Age = 2},
                new Student() {Name = "Annie", Gender = false, Age = 2},
                new Student() {Name = "Peter", Gender = true, Age = 37},
                new Student() {Name = "Kathy", Gender = false, Age = 6},
                new Student() {Name = "Steven", Gender = true, Age = 31}
            });
            base.Seed(context);
        }
    }
}
