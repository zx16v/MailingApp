using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MailingApp;

namespace MailingApp
{
    public class MailLogsController : Controller
    {
        private GetMailListEntities db = new GetMailListEntities();

        // GET: MailLogs
        public async Task<ActionResult> Index()
        {
            return View(await db.MailLogs.ToListAsync());
        }

        // GET: MailLogs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailLog mailLog = await db.MailLogs.FindAsync(id);
            if (mailLog == null)
            {
                return HttpNotFound();
            }
            return View(mailLog);
        }

        // GET: MailLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MailLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomerMail,EmployeeMail,Action,ActionDate")] MailLog mailLog)
        {
            if (ModelState.IsValid)
            {
                db.MailLogs.Add(mailLog);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mailLog);
        }

        // GET: MailLogs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailLog mailLog = await db.MailLogs.FindAsync(id);
            if (mailLog == null)
            {
                return HttpNotFound();
            }
            return View(mailLog);
        }

        // POST: MailLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomerMail,EmployeeMail,Action,ActionDate")] MailLog mailLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mailLog).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mailLog);
        }

        // GET: MailLogs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailLog mailLog = await db.MailLogs.FindAsync(id);
            if (mailLog == null)
            {
                return HttpNotFound();
            }
            return View(mailLog);
        }

        // POST: MailLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MailLog mailLog = await db.MailLogs.FindAsync(id);
            db.MailLogs.Remove(mailLog);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
