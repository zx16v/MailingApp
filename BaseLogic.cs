using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailingApp
{
    public class BaseLogic:IDisposable
    {
        protected readonly GetMailListEntities DB = new GetMailListEntities();
        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
