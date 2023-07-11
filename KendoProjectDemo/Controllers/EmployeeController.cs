using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoProjectDemo.Models;
using KendoProjectDemo.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoProjectDemo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeController()
        {
            _dbContext = new EmployeeDbContext();
        }

        public ActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            return View();
        }
        public ActionResult AddEmployee()
        {
            var model = new Employees();
            return PartialView("_AddEmployee", model);
        }

        public JsonResult ReadEmployees([DataSourceRequest] DataSourceRequest request)
        {
            var employees = _dbContext.Employees.ToList();
            return Json(employees.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddedEmp()
        {
            return View();
        }

        public JsonResult ReadEmployees1([DataSourceRequest] DataSourceRequest request)
        {
            var employees = _dbContext.Employees.ToList();
            return Json(employees.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employees employee)
        {
            employee.Id = Guid.NewGuid();
            if (employee.Id != null)
            {
                employee.Id = Guid.NewGuid();
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Employee");
            }

            return View(employee);
        }

        [HttpPost]
        public JsonResult UpdateEmployee([DataSourceRequest] DataSourceRequest request, Employees employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(employee).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(employee);
            }

            return Json(null);
        }

        [HttpPost]
        public JsonResult DeleteEmployee([DataSourceRequest] DataSourceRequest request, List<Guid> ids)
        {
            var employeesToDelete = _dbContext.Employees.Where(e => ids.Contains(e.Id)).ToList();

            foreach (var employee in employeesToDelete)
            {
                _dbContext.Employees.Remove(employee);
            }

            _dbContext.SaveChanges();

            return Json(ids);
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
