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
        public ActionResult Index()
        {
            return View();
        }

        public OrderController()
        {
            _dbContext = new EmployeeDbContext();
        }

        public JsonResult ReadOrders([DataSourceRequest] DataSourceRequest request, Guid employeeId)
        {
            var orders = _dbContext.Orders.Where(o => o.EmployeeId == employeeId).ToList();
            return Json(orders.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateOrder(Guid employeeId, Order order)
        {
            if (ModelState.IsValid)
            {
                order.EmployeeId = employeeId;
                _dbContext.Orders.Add(order);
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