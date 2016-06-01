using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailingApp
{
 public   class CustomersLogic: BaseLogic
    {
        public List<MoneyTransferType> GetMoneyTransferTypes()
        {
            //check to add validation for ActioniD, another query to check if contain 

            return DB.MoneyTransferTypes.ToList();
        }

        public  string GetMailTemplateByActionId(int ActionId)
        {
           return   DB.GetMailTemplateByAction(ActionId).FirstOrDefault();
        }
    }
}
