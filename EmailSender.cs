using System;
using System.Threading.Tasks;
using MailingApp.Models;
using SimpleInjector;
using System.Net.Mail;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Net;
//the heart of the app , one central class and helpers that responsbale for mailing 
// every new action that needed to follow with mail list to employees need to inherit this interface
// and implement two methods.
//in the templatefit the values are loaded to the variable of the template/
namespace MailingApp
{
    public interface IEmailSender
    {
       Task Sender(string CustomerMail, ActionViewModel Action, IEmail CustomerAction);
    }
    public interface IEmail
    {
        ListDictionary TemplateFit(string EmployeeName);
        void ClearReplacements();
    }


    public class LoginMail : IEmail
    {
        private ListDictionary replacements;
        private string CustomerEmail;
        public LoginMail(string customerEmail)
        {
            CustomerEmail = customerEmail;
            replacements = new ListDictionary();
        }

        public void ClearReplacements()
        {
            replacements.Clear();
        }
        public ListDictionary TemplateFit(string EmployeeName)
        {
            replacements.Add("{employee}", EmployeeName);
            replacements.Add("{Username}", CustomerEmail);
            replacements.Add("{datetime}", DateTime.Now.ToString());
            return replacements;
        }

    }

    public class DepositMail : IEmail
    {
        private ListDictionary replacements;
        private string CustomerEmail;
        private decimal Amount;
        private string MoneyTransferType;

        public DepositMail(string CustomerEmailAd, decimal amount, string TransferType)
        {
            CustomerEmail = CustomerEmailAd;
            Amount = amount;
            MoneyTransferType = TransferType;
            replacements = new ListDictionary();
        }

        public void ClearReplacements()
        {
            replacements.Clear();
        }

        public ListDictionary TemplateFit(string EmployeeName)
        {
            replacements.Add("{employee}", EmployeeName);
            replacements.Add("{Username}", CustomerEmail);
            replacements.Add("{amount}", Amount.ToString());
            replacements.Add("{transfertype}", MoneyTransferType);
            replacements.Add("{datetime}", DateTime.Now.ToString());
            return replacements;
        }

    }

    public class EmailSender : IEmailSender
    {
       
        public async Task Sender( string CustomerMail, ActionViewModel Action, IEmail CustomerAction)
        {
            MailDefinition md = new MailDefinition();
            md.From = "shimi.levi@gmail.com";
            md.IsBodyHtml = true;
            md.Subject = "user" + CustomerMail + Action.ActionName ;
            string MessageBody;
            MailLog Mlog = new MailLog();
            Mlog.CustomerMail = CustomerMail;
            Mlog.Action = Action.ActionName;
            Mlog.ActionDate = DateTime.Now;
            using (CustomersLogic logic = new CustomersLogic())
            {
                MessageBody = logic.GetMailTemplateByActionId(Action.ActionId);
            }
                
            MailMessage msg;
            using (EmployeesLogic logic = new EmployeesLogic())
            {
                IEnumerable<GetMailListByAction_Result> GM = logic.GetEmployeesMailListbyAction(Action.ActionId);
               
                foreach (GetMailListByAction_Result item in GM)
                {
                    msg = md.CreateMailMessage(item.Email, CustomerAction.TemplateFit(item.Email), MessageBody, new System.Web.UI.Control());
                    Mlog.EmployeeMail = item.Email;
                    logic.updateMail2EmployeesLog(Mlog);
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(md.From, "password");
                        await smtp.SendMailAsync(msg);
                   }
                    CustomerAction.ClearReplacements();
                }
            }
           
        }
    }
}