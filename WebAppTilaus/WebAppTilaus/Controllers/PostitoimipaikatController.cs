using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilaus.Models;
using System.Data.Entity;

namespace WebAppTilaus.Controllers
{
    public class PostitoimipaikatController : Controller
    {
        // GET: Postitoimipaikat
        TilausDBEntities db = new TilausDBEntities();
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                List<Postitoimipaikat> model = db.Postitoimipaikat.ToList();
                return View(model);
            }
        }
        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
            if (postitoimipaikat == null) return HttpNotFound();
            return View(postitoimipaikat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="Postinumero,Postitoimipaikka")] Postitoimipaikat postitoimipaikat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postitoimipaikat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postitoimipaikat);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Postinumero,Postitoimipaikka")] Postitoimipaikat postitoimipaikat)
        {
            if (ModelState.IsValid)
            {
                db.Postitoimipaikat.Add(postitoimipaikat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postitoimipaikat);
        }
        public ActionResult Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
            if (postitoimipaikat == null) return HttpNotFound();
            return View(postitoimipaikat);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
            db.Postitoimipaikat.Remove(postitoimipaikat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}