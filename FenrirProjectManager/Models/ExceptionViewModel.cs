using System;
using System.Web.Mvc;

namespace FenrirProjectManager.Models
{
    public class ExceptionViewModel
    {
        public string ExceptionMessage { get; private set; }
        public ActionResult ReturnUrl { get; private set; }

        public ExceptionViewModel(Exception exception, ActionResult action = null)
        {
            ExceptionMessage = exception.Message;
            ReturnUrl = action ?? MVC.Home.Index();
        }


    }
}
