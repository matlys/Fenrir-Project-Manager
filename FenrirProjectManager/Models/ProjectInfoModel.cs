using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FenrirProjectManager.ValidationAttributes;

namespace FenrirProjectManager.Models
{
    public class ProjectInfoModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Logo { get; set; }
    }
}