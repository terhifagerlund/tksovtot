using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilaus.Models;

namespace WebAppTilaus.Controllers
{
    public class TilausrivitController : Controller
    {
        // GET: Tilausrivit
        TilausDBEntities db = new TilausDBEntities();
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                List<Tilausrivit> model = db.Tilausrivit.ToList();
                return View(model);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            if (tilausrivit == null) return HttpNotFound();
            return View(tilausrivit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Ahinta")] Tilausrivit tilausrivit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilausrivit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tilausrivit);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Ahinta")] Tilausrivit tilausrivit)
        {
            if (ModelState.IsValid)
            {
                db.Tilausrivit.Add(tilausrivit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tilausrivit);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            if (tilausrivit == null) return HttpNotFound();
            return View(tilausrivit);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            db.Tilausrivit.Remove(tilausrivit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}