using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FenrirProjectManager.Models
{
    public class ProjectHeaderViewModel
    {
        public byte[] ProjectLogo { get; set; }
        public string ProjectName { get; set; }
        public ActionResult ActionLink { get; set; }
    }
}
