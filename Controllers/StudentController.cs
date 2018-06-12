using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Egzaminy.Models;
using Egzaminy.ViewModels;

namespace Egzaminy.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ICollection<ApplicationUser> DajStudenta()
        {
            return db.Users.Where(x => x.Roles.Any(y => y.RoleId.Equals(db.Roles.Where
                (a => a.Name.Equals("Student")).Select(b => b.Id).FirstOrDefault()))).ToList();
        }
        // GET: Student
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(DajStudenta());
        }

        // GET: studadowca/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: studadowca/Create

        // GET: studadowca/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            Wypelnijlatami(applicationUser);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }
        private void Wypelnijlatami(ApplicationUser stud)
        {
            var lata = db.Roks;
            var latastud = new HashSet<int>(stud.Rok.Select(p => p.Id));
            var viewModel = new List<DajLata>();
            foreach (var rok in lata)
            {
                viewModel.Add(new DajLata
                {
                    RokID = rok.Id,
                    Nazwa = rok.NazwaRoku,
                    Przypisano = latastud.Contains(rok.Id)
                });
            }
            ViewBag.Lata = viewModel;
        }

        // POST: studadowca/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(ApplicationUser applicationUser, string[] lata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                Aktualizujlata(lata, applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Wypelnijlatami(applicationUser);
            return View(applicationUser);
        }

        private void Aktualizujlata(string[] lata, ApplicationUser stud)
        {
            //Załaduj kolekcje
            db.Entry(stud).Collection(r => r.Rok).Load();
            if (lata == null)
            {
                stud.Rok = new List<Rok>();
                return;
            }
            var lataHS = new HashSet<string>(lata);
            var studrok = new HashSet<int>(stud.Rok.Select(p => p.Id));
            foreach (var rok in db.Roks)
            {
                if (lataHS.Contains(rok.Id.ToString()))
                {
                    if (!studrok.Contains(rok.Id))
                    {
                        stud.Rok.Add(rok);
                    }
                }
                else if (studrok.Contains(rok.Id))
                    stud.Rok.Remove(rok);
            }
        }

        // GET: studadowca/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: studadowca/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            Aktualizujlata(null, applicationUser);
            db.Users.Remove(applicationUser);
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
