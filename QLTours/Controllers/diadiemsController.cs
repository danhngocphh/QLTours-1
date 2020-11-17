using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTours.Models;

namespace QLTours.Controllers
{
    public class diadiemsController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: diadiems
        public ActionResult Index()
        {
            return View(db.diadiems.ToList());
        }

        // GET: diadiems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            diadiem diadiem = db.diadiems.Find(id);
            if (diadiem == null)
            {
                return HttpNotFound();
            }
            return View(diadiem);
        }

        // GET: diadiems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: diadiems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ThanhPho,Ten,MoTa")] diadiem diadiem)
        {
            if (ModelState.IsValid)
            {
                db.diadiems.Add(diadiem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diadiem);
        }

        // GET: diadiems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            diadiem diadiem = db.diadiems.Find(id);
            if (diadiem == null)
            {
                return HttpNotFound();
            }
            return View(diadiem);
        }

        // POST: diadiems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ThanhPho,Ten,MoTa")] diadiem diadiem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diadiem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diadiem);
        }

        // GET: diadiems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            diadiem diadiem = db.diadiems.Find(id);
            if (diadiem == null)
            {
                return HttpNotFound();
            }
            return View(diadiem);
        }

        // POST: diadiems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            diadiem diadiem = db.diadiems.Find(id);
            db.diadiems.Remove(diadiem);
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
