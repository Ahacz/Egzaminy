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
    public class WykladowcaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ICollection<ApplicationUser> DajWykladowce()
        {
            return db.Users.Where(x => x.Roles.Any(y => y.RoleId.Equals(db.Roles.Where
                (a => a.Name.Equals("Wykładowca")).Select(b => b.Id).FirstOrDefault()))).ToList();
        }
        // GET: Wykladowca
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(DajWykladowce());
        }

        // GET: Wykladowca/Details/5
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

        // GET: Wykladowca/Create

        // GET: Wykladowca/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            Wypelnijprzedmiotami(applicationUser);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }
        private void Wypelnijprzedmiotami(ApplicationUser wykl)
        {
            var przedmioty = db.Przedmioties;
            var przedmiotywykl = new HashSet<int>(wykl.Przedm.Select(p => p.Id));
            var viewModel = new List<DajPrzedmioty>();
            foreach (var przedmiot in przedmioty)
            {
                viewModel.Add(new DajPrzedmioty
                {
                    PrzedmID = przedmiot.Id,
                    Nazwa = przedmiot.NazwaPrzedmiotu,
                    Przypisano=przedmiotywykl.Contains(przedmiot.Id)
                });
            }
            ViewBag.Przedmioty = viewModel;
        }

        // POST: Wykladowca/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(ApplicationUser applicationUser, string[] przedmioty)
        {
            if (ModelState.IsValid)
            {
                //Musi zostać ustawiony na modified, żeby można było wprowadzić zmiany.
                db.Entry(applicationUser).State = EntityState.Modified;
                AktualizujPrzedmioty(przedmioty, applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Wypelnijprzedmiotami(applicationUser);
            return View(applicationUser);
        }

        private void AktualizujPrzedmioty(string [] przedmioty, ApplicationUser wykl)
        {
            db.Entry(wykl).Collection(prz => prz.Przedm).Load();
            if (przedmioty == null)
            {
                wykl.Przedm = new List<Przedmioty>();
                return;
            }
            var przedmiotyHS = new HashSet<string>(przedmioty);
            var wyklPrzedm = new HashSet<int>(wykl.Przedm.Select(p => p.Id));
            foreach (var przedm in db.Przedmioties)
            {
                if (przedmiotyHS.Contains(przedm.Id.ToString()))
                {
                    if (!wyklPrzedm.Contains(przedm.Id))
                    {
                        wykl.Przedm.Add(przedm);
                    }
                }
                else if (wyklPrzedm.Contains(przedm.Id))
                    wykl.Przedm.Remove(przedm);
            }
        }

        // GET: Wykladowca/Delete/5
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

        // POST: Wykladowca/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            AktualizujPrzedmioty(null, applicationUser);
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
