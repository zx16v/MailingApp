using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailingApp.Models;
using System.Threading.Tasks;
using MailingApp;
using System.Net.Mail;
using static System.Collections.Specialized.BitVector32;

namespace MailingApp
{
    public class NewHomeController : Controller
    {
        //private See["CustomerMail"];
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Employee()
        {
            ViewBag.Message = "Hi Employee.";

            return View();
        }

        public ActionResult Customer(LoginViewModel Loginmodel)
        {
            ViewBag.Message = "Hello ";
            int foundS1 = Loginmodel.Email.IndexOf("@");
            string PersoneName = Loginmodel.Email.Remove(foundS1);
            ViewBag.PersoneName = PersoneName;
            var tuple = new Tuple<LoginViewModel, ActionViewModel>(Loginmodel, new ActionViewModel());
            var MoneyTransferTypes =  GetAllMoneyTransferTypes();
            tuple.Item2.TansferTypes = GetSelectListItems(MoneyTransferTypes);
            Session["CustomerMail"] = Loginmodel.Email;
            return View(tuple);
          
            
        }

        [HttpPost]
        public async Task<ActionResult> Customer([Bind(Prefix = "Item2")] ActionViewModel ActionView)
        {
          
            // Get all TansferTypes again
            var MoneyTarnsferTypes = GetAllMoneyTransferTypes();
            ActionView.TansferTypeId = Convert.ToInt32(ActionView.TansferType);
             // Set these states on the model. We need to do this because
            // only the selected value from the DropDownList is posted back, not the whole
            // list of states.
            ActionView.TansferTypes = GetSelectListItems(MoneyTarnsferTypes);
            ActionView.TansferType = ActionView.TansferTypes.ElementAt(ActionView.TansferTypeId - 1).Text;
            // In case everything is fine - i.e. both "Name" and "State" are entered/selected,
            // redirect user to the "Done" page, and pass the user object along via Session
            ActionViewModel action = new ActionViewModel();
            ["CustomerMail"]
            action.ActionId = 1;
            action.ActionName = "Deposit";
            IEmail Login = new LoginMail(model.Email);
            EmailSender SendToEmployees = new EmailSender();
            await SendToEmployees.Sender(model.Email, action, Login);


            if (ModelState.IsValid)
            {
                Session["CustomerActionModel"] = ActionView;
                return RedirectToAction("Done");
            }

            // Something is not right - so render the registration page again,
            // keeping the data user has entered by supplying the model.
            return View("Customer", ActionView);
        }

        public ActionResult Done()
        {
            var model = Session["CustomerActionModel"] as ActionViewModel;

            // Display Done.html page that shows Name and selected state.
            return View(model);
        }


        private IEnumerable<MoneyTransferType> GetAllMoneyTransferTypes()
        {
            IEnumerable<MoneyTransferType> MoneyTansferTypesList = new List<MoneyTransferType>();
            using (CustomersLogic logic = new CustomersLogic())
                MoneyTansferTypesList = logic.GetMoneyTransferTypes();
            return MoneyTansferTypesList;
        }
        // DropDownList.
        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<MoneyTransferType> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.TarnsferType
                    //Text = element.Id.ToString(),
                    //Value = element.TarnsferType
                });
            }

            return selectList;
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}