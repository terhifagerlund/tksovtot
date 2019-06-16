﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTilaus.Models;
using System.Net;
using System.Data.Entity;

namespace WebAppTilaus.Controllers
{
    public class HenkilotController : Controller
    {
        // GET: Henkilot
        TilausDBEntities db = new TilausDBEntities();
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                List<Henkilot> model = db.Henkilot.ToList();
                return View(model);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Henkilot henkilot = db.Henkilot.Find(id);
            if (henkilot == null) return HttpNotFound();
            return View(henkilot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Henkilo_id,Etunimi,Sukunimi,Osoite,Esimies,Postinumero,Sahkoposti")] Henkilot henkilot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(henkilot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(henkilot);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind (Include = "Henkilo_id,Etunimi,Sukunimi,Osoite,Esimies,Postinumero,Sahkoposti")] Henkilot henkilot)
        {
            if (ModelState.IsValid)
            {
                db.Henkilot.Add(henkilot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(henkilot);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Henkilot henkilot = db.Henkilot.Find(id);
            if (henkilot == null) return HttpNotFound();
            return View(henkilot);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Henkilot henkilot = db.Henkilot.Find(id);
            db.Henkilot.Remove(henkilot);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}