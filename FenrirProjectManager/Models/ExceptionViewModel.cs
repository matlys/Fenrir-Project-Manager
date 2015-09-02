using System.Web.Mvc;

namespace FenrirProjectManager.Models
{
    public class ExceptionViewModel
    {
        public string ExceptionMessage { get; set; }
        public ActionResult ReturnUrl { get; set; }
    }
}
