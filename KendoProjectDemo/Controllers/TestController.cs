using KendoProjectDemo.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoProjectDemo.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string PostUsingParameters(string FirstName, string LastName)
        {
            return "Post Using Parameters   -" + FirstName + "," + LastName;
        }

        [HttpPost]
        public string PostUsingRequest()
        {
            string FirstName = Request["FirstName"];
            string LastName = Request["LastName"];
            return "Post Using Request -" + FirstName + "," + LastName;
        }

        [HttpPost]
        public string PostUsingFormCollection(FormCollection form)
        {
            string FirstName = form["FirstName"];
            string LastName = form["LastName"];
            return "Post Using Request -" + FirstName + "," + LastName;
        }

        [HttpPost]
        public string PostUsingModelBinding(Employees employees)
        {  
            return "Post Using ModelBinding -" + employees.FirstName + "," + employees.LastName;
        }
    }
}