using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Egzaminy.Models;

namespace Egzaminy.Controllers
{
    public class RoksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Roks
        public ActionResult Index()
        {
            return View(db.Roks.ToList());
        }

        // GET: Roks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rok rok = db.Roks.Find(id);
            if (rok == null)
            {
                return HttpNotFound();
            }
            return View(rok);
        }

        // GET: Roks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roks/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NazwaRoku")] Rok rok)
        {
            if (ModelState.IsValid)
            {
                db.Roks.Add(rok);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rok);
        }

        // GET: Roks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rok rok = db.Roks.Find(id);
            if (rok == null)
            {
                return HttpNotFound();
            }
            return View(rok);
        }

        // POST: Roks/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NazwaRoku")] Rok rok)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rok).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rok);
        }

        // GET: Roks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rok rok = db.Roks.Find(id);
            if (rok == null)
            {
                return HttpNotFound();
            }
            return View(rok);
        }

        // POST: Roks/Delete/5
        [HttpPost, ActionName("Usuń")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rok rok = db.Roks.Find(id);
            db.Roks.Remove(rok);
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
