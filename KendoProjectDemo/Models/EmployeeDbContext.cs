using KendoProjectDemo.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KendoProjectDemo.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() : base("Data Source=DESKTOP-SCCHQ7K;Initial Catalog=EmployeeDBKendo;Integrated Security=True")
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}