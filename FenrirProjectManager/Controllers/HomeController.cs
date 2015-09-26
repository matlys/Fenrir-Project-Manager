using System;
using System.Web.Mvc;
using FenrirProjectManager.Models;

namespace FenrirProjectManager.Controllers
{
    public partial class HomeController : Controller
    {

        public virtual ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction(MVC.Issues.Index());

            try
            {
                return View();
            }
            catch (Exception exception)
            {
                ExceptionViewModel exceptionViewModel = new ExceptionViewModel(exception);
                return View("Error", exceptionViewModel);
            }
        }

        public virtual ActionResult About()
        {
            try
            {
                return View();
            }
            catch (Exception exception)
            {
                ExceptionViewModel exceptionViewModel = new ExceptionViewModel(exception);
                return View("Error", exceptionViewModel);
            }
        }

        public virtual ActionResult Contact()
        {
            try
            {
                return View();
            }
            catch (Exception exception)
            {
                ExceptionViewModel exceptionViewModel = new ExceptionViewModel(exception);
                return View("Error", exceptionViewModel);
            }
        }
    }
}