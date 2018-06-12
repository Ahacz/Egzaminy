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
            Wypelnijprzedmiotami(rok);
            if (rok == null)
            {
                return HttpNotFound();
            }
            return View(rok);
        }
        private void Wypelnijprzedmiotami(Rok rok)
        {
            var przedmioty = db.Przedmioties;
            var przedmiotyrok = new HashSet<int>(rok.Przedmioty.Select(p => p.Id));
            var viewModel = new List<DajPrzedmioty>();
            foreach (var przedmiot in przedmioty)
            {
                viewModel.Add(new DajPrzedmioty
                {
                    PrzedmID = przedmiot.Id,
                    Nazwa = przedmiot.NazwaPrzedmiotu,
                    Przypisano = przedmiotyrok.Contains(przedmiot.Id)
                });
            }
            ViewBag.Przedmioty = viewModel;
        }

        // POST: Roks/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rok rok, string[] przedmiotyx)
        {
           
            if (ModelState.IsValid)
            {
                db.Entry(rok).State = EntityState.Modified;
                AktualizujPrzedmioty(przedmiotyx, rok);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rok);
        }
        private void AktualizujPrzedmioty(string[] przedmioty, Rok rok)
        {
            db.Entry(rok).Collection(prz => prz.Przedmioty).Load();
            if (przedmioty == null)
            {
                rok.Przedmioty = new List<Przedmioty>();
                return;
            }
            var przedmiotyHS = new HashSet<string>(przedmioty);
            var rokPrzedm = new HashSet<int>(rok.Przedmioty.Select(p => p.Id));
            foreach (var przedm in db.Przedmioties)
            {
                if (przedmiotyHS.Contains(przedm.Id.ToString()))
                {
                    if (!rokPrzedm.Contains(przedm.Id))
                    {
                        rok.Przedmioty.Add(przedm);
                    }
                }
                else if (rokPrzedm.Contains(przedm.Id))
                    rok.Przedmioty.Remove(przedm);
            }
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
