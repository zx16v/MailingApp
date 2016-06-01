using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailingApp
{
   public  class EmployeesLogic: BaseLogic
    {
      
        public List<GetMailListByAction_Result> GetEmployeesMailListbyAction(int ActionId)
        {
                //check to add validation for ActioniD, another query to check if contain 
                return DB.GetMailListByAction(ActionId).ToList();
        }

        public void updateMail2EmployeesLog (MailLog log)
        {
            DB.Entry(log).State = System.Data.Entity.EntityState.Added;
            DB.SaveChanges();
        }
    }
}
