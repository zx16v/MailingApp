using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
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
        public ActionResult Index()
        {
            return View(db.MailLogs.ToList());
        }

        // GET: MailLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailLog mailLog = db.MailLogs.Find(id);
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
        public ActionResult Create([Bind(Include = "Id,CustomerMail,EmployeeMail,Action")] MailLog mailLog)
        {
            if (ModelState.IsValid)
            {
                db.MailLogs.Add(mailLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mailLog);
        }

        // GET: MailLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailLog mailLog = db.MailLogs.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,CustomerMail,EmployeeMail,Action")] MailLog mailLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mailLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mailLog);
        }

        // GET: MailLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailLog mailLog = db.MailLogs.Find(id);
            if (mailLog == null)
            {
                return HttpNotFound();
            }
            return View(mailLog);
        }

        // POST: MailLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MailLog mailLog = db.MailLogs.Find(id);
            db.MailLogs.Remove(mailLog);
            db.SaveChanges();
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
