using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuizAsp.Entities;

namespace QuizAsp.Controllers
{
    public class TestResultsController : Controller
    {
        private QuizModel db = new QuizModel();

        // GET: TestResults
        public ActionResult Index()
        {
            var testResult = db.TestResult.Include(t => t.Test).Include(t => t.User);
            return View(testResult.ToList());
        }

        // GET: TestResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResult.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        // GET: TestResults/Create
        public ActionResult Create()
        {
            ViewBag.TestId = new SelectList(db.Test, "Id", "Caption");
            ViewBag.UserId = new SelectList(db.User, "Id", "Login");
            return View();
        }

        // POST: TestResults/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TestId,UserId,Date")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                db.TestResult.Add(testResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TestId = new SelectList(db.Test, "Id", "Caption", testResult.TestId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Login", testResult.UserId);
            return View(testResult);
        }

        // GET: TestResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResult.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestId = new SelectList(db.Test, "Id", "Caption", testResult.TestId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Login", testResult.UserId);
            return View(testResult);
        }

        // POST: TestResults/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TestId,UserId,Date")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TestId = new SelectList(db.Test, "Id", "Caption", testResult.TestId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Login", testResult.UserId);
            return View(testResult);
        }

        // GET: TestResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResult.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        // POST: TestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestResult testResult = db.TestResult.Find(id);
            db.TestResult.Remove(testResult);
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
