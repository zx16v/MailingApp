using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MailingApp.Models
{
    public class ActionViewModel
    {
        // [Required, Display(Name = "Action ")] ClientsActionsMailReports GetMailListEntities 
        public string ActionName { get; set; }
        [Required, Display(Name = "Amount")]
        public decimal Amount { get; set; }
        public int ActionId { get; set; }
        [Required, Display(Name = "TansferType")]
        public string TansferType { get; set; }
        public int TansferTypeId { get; set; }
        // This property will hold all available states for selection
        public IEnumerable<SelectListItem> TansferTypes { get; set; }
    }
   
}