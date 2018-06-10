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
    public class PodgrupasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Podgrupas
        public ActionResult Index()
        {
            return View(db.Podgrupas.ToList());
        }

        // GET: Podgrupas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Podgrupa podgrupa = db.Podgrupas.Find(id);
            if (podgrupa == null)
            {
                return HttpNotFound();
            }
            return View(podgrupa);
        }

        // GET: Podgrupas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Podgrupas/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa,Liczba,Idroku")] Podgrupa podgrupa)
        {
            if (ModelState.IsValid)
            {
                db.Podgrupas.Add(podgrupa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(podgrupa);
        }

        // GET: Podgrupas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Podgrupa podgrupa = db.Podgrupas.Find(id);
            if (podgrupa == null)
            {
                return HttpNotFound();
            }
            return View(podgrupa);
        }

        // POST: Podgrupas/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa,Liczba,Idroku")] Podgrupa podgrupa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(podgrupa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(podgrupa);
        }

        // GET: Podgrupas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Podgrupa podgrupa = db.Podgrupas.Find(id);
            if (podgrupa == null)
            {
                return HttpNotFound();
            }
            return View(podgrupa);
        }

        // POST: Podgrupas/Delete/5
        [HttpPost, ActionName("Usuń")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Podgrupa podgrupa = db.Podgrupas.Find(id);
            db.Podgrupas.Remove(podgrupa);
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
