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
    public class PrzedmiotiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Przedmioties
        public ActionResult Index()
        {
            return View(db.Przedmioties.ToList());
        }

        // GET: Przedmioties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmioty przedmioty = db.Przedmioties.Find(id);
            if (przedmioty == null)
            {
                return HttpNotFound();
            }
            return View(przedmioty);
        }
        [Authorize(Roles = "Admin")]
        // GET: Przedmioties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Przedmioties/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,NazwaPrzedmiotu")] Przedmioty przedmioty)
        {
            if (ModelState.IsValid)
            {
                db.Przedmioties.Add(przedmioty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(przedmioty);
        }

        // GET: Przedmioties/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmioty przedmioty = db.Przedmioties.Find(id);
            if (przedmioty == null)
            {
                return HttpNotFound();
            }
            return View(przedmioty);
        }

        // POST: Przedmioties/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,NazwaPrzedmiotu")] Przedmioty przedmioty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przedmioty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(przedmioty);
        }

        // GET: Przedmioties/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmioty przedmioty = db.Przedmioties.Find(id);
            if (przedmioty == null)
            {
                return HttpNotFound();
            }
            return View(przedmioty);
        }

        // POST: Przedmioties/Delete/5
        [HttpPost, ActionName("Usuń")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Przedmioty przedmioty = db.Przedmioties.Find(id);
            db.Przedmioties.Remove(przedmioty);
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
