using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoProjectDemo.Models;
using KendoProjectDemo.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoProjectDemo.Controllers
{
    public class OrderController : Controller
    {
        private readonly EmployeeDbContext _dbContext;
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public OrderController()
        {
            _dbContext = new EmployeeDbContext();
        }
        public ActionResult Index()
        {
            // Retrieve the employees from the database
            var employees = _dbContext.Employees.ToList();

            // Assign the employees to the ViewBag
            ViewBag.Employees = employees;

            return View();
        }

        public JsonResult ReadOrders([DataSourceRequest] DataSourceRequest request, Guid employeeId)
        {
            var orders = _dbContext.Orders.Where(o => o.EmployeeId == employeeId).ToList();
            return Json(orders.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddedOrder()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateOrder([DataSourceRequest] DataSourceRequest request, List<string> ids, Order order)
        {
            if (ModelState.IsValid)
            {
                if (ids != null)
                {
                    foreach (var employeeId in ids)
                    {
                        var parsedId = Guid.Parse(employeeId);
                        var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == parsedId);

                        if (employee != null)
                        {
                            if (employee.Orders == null)
                                employee.Orders = new List<Order>();

                            var newGuid = Guid.NewGuid();
                            var newOrder = new Order
                            {
                                Id = newGuid,
                                OrderNumber = order.OrderNumber,
                                TotalAmount = order.TotalAmount,
                                OrderDate = order.OrderDate,
                                EmployeeId = newGuid,
                                Employee = employee
                            };

                            employee.Orders.Add(newOrder);
                        }
                    }
                }

                _dbContext.SaveChanges();
                return Json(order);
            }

            return Json(null);
        }



        [HttpPost]
        public JsonResult UpdateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                var existingOrder = _dbContext.Orders.Find(order.Id);
                if (existingOrder != null)
                {
                    existingOrder.OrderNumber = order.OrderNumber;
                    existingOrder.TotalAmount = order.TotalAmount;
                    existingOrder.OrderDate = order.OrderDate;
                    _dbContext.SaveChanges();
                    return Json(order);
                }
            }

            return Json(null);
        }

        [HttpPost]
        public JsonResult DeleteOrder(Guid id)
        {
            var order = _dbContext.Orders.Find(id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                _dbContext.SaveChanges();
                return Json(id);
            }

            return Json(null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}