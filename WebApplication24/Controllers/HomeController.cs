using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication24.Models;

namespace WebApplication24.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewOrders(DateTime fromDate, DateTime toDate)
        {
            OrderManager manager = new OrderManager(Properties.Settings.Default.ConStr);
            IEnumerable<Order> orders = manager.GetOrders(fromDate, toDate);

            OrdersViewModel viewModel = new OrdersViewModel();
            viewModel.Orders = orders;
            viewModel.FromDate = fromDate;
            viewModel.ToDate = toDate;

            return View(viewModel);
        }

        public ActionResult ToDo()
        {
            ToDoManager manager = new ToDoManager(Properties.Settings.Default.ToDoConStr);
            IEnumerable<ToDoCategory> categories = manager.GetCategories();
            return View(new ToDoCategoriesViewModel
            {
                Categories = categories
            });
        }

        public ActionResult NewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(string name)
        {
            ToDoManager manager = new ToDoManager(Properties.Settings.Default.ToDoConStr);
            ToDoCategory category = new ToDoCategory { Name = name };
            manager.AddCategory(category);
            AddCategoryViewModel viewModel = new AddCategoryViewModel
            {
                Category = category
            };
            return Redirect("/home/todo");
            //return Redirect("/home/todo");
            //return View(viewModel);
        }

        public ActionResult EditCategory(int id)
        {
            ToDoManager manager = new ToDoManager(Properties.Settings.Default.ToDoConStr);
            ToDoCategory category = manager.GetById(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult UpdateCategory(string name, int id)
        {
            ToDoManager manager = new ToDoManager(Properties.Settings.Default.ToDoConStr);
            ToDoCategory category = new ToDoCategory
            {
                Name = name,
                Id = id
            };
            manager.UpdateCategory(category);
            return Redirect("/home/todo");
        }
    }
}