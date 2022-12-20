using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace TestingPagination.Models
{
    public class Employee
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public bool Status { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modifed_date { get; set; }
    }
    public class EmpDBContext : DbContext
    {
        public EmpDBContext(DbContextOptions<EmpDBContext> options) : base(options)
        {

        }
        /*public EmpDBContext() { }*/
        public DbSet<Employee> employee_tb { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            object p = optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }*/
    }
}
