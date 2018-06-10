using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Egzaminy.Models;

namespace Egzaminy.Controllers
{
    public class EgzaminsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Egzamins
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return View(db.Egzamins.ToList());
            else
                if(User.IsInRole("Wykładowca"))
            {

            }
                return HttpNotFound();
        }

        // GET: Egzamins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Egzamin egzamin = db.Egzamins.Find(id);
            if (egzamin == null)
            {
                return HttpNotFound();
            }
            return View(egzamin);
        }

        // GET: Egzamins/Create
        [Authorize(Roles = "Admin,Wykładowca")]
        public ActionResult Create()
        {
            ViewBag.id_wykladowcy = new SelectList(
                db.Users.Where(x => x.Roles.Any(y => y.RoleId.Equals(db.Roles.Where
                (a => a.Name.Equals("Wykładowca")).Select(b => b.Id).FirstOrDefault()))).ToList()
                , "Id", "Imie");
            //ViewBag.id_wykladowcy = new SelectList(db.Users, "Id", "Imie");
            ViewBag.Podgrupa = new SelectList(db.Podgrupas, "Id", "Nazwa");
            ViewBag.Sala = new SelectList(db.Sales, "Id", "NazwaSali");
            return View();
        }

        // POST: Egzamins/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Wykładowca")]
        public ActionResult Create([Bind(Include = "Id,Data,CzasRozpoczecia,CzasTrwania,Sala,Podgrupa,id_wykladowcy")] Egzamin egzamin)
        {
            if (ModelState.IsValid)
            {
                db.Egzamins.Add(egzamin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(egzamin);
        }

        // GET: Egzamins/Edit/5
        [Authorize(Roles = "Admin,Wykładowca")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Egzamin egzamin = db.Egzamins.Find(id);
            if (egzamin == null)
            {
                return HttpNotFound();
            }
            return View(egzamin);
        }

        // POST: Egzamins/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Wykładowca")]
        public ActionResult Edit([Bind(Include = "Id,Data,CzasRozpoczecia,CzasTrwania,Sala,Podgrupa,id_wykladowcy")] Egzamin egzamin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(egzamin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(egzamin);
        }

        // GET: Egzamins/Delete/5
        [Authorize(Roles = "Admin,Wykładowca")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Egzamin egzamin = db.Egzamins.Find(id);
            if (egzamin == null)
            {
                return HttpNotFound();
            }
            return View(egzamin);
        }

        // POST: Egzamins/Delete/5
        [Authorize(Roles = "Admin,Wykładowca")]
        [HttpPost, ActionName("Usuń")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Egzamin egzamin = db.Egzamins.Find(id);
            db.Egzamins.Remove(egzamin);
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
