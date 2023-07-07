using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoProjectDemo.Models.DataModel
{
    public class Order
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        // Foreign key to the associated employee
        public Guid EmployeeId { get; set; }
        public Employees Employee { get; set; }
    }
}