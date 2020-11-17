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
    public class loaisController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: loais
        public ActionResult Index()
        {
            return View(db.loais.ToList());
        }

        // GET: loais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loai loai = db.loais.Find(id);
            if (loai == null)
            {
                return HttpNotFound();
            }
            return View(loai);
        }

        // GET: loais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: loais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ten,MoTa")] loai loai)
        {
            if (ModelState.IsValid)
            {
                db.loais.Add(loai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loai);
        }

        // GET: loais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loai loai = db.loais.Find(id);
            if (loai == null)
            {
                return HttpNotFound();
            }
            return View(loai);
        }

        // POST: loais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ten,MoTa")] loai loai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loai);
        }

        // GET: loais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loai loai = db.loais.Find(id);
            if (loai == null)
            {
                return HttpNotFound();
            }
            return View(loai);
        }

        // POST: loais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            loai loai = db.loais.Find(id);
            db.loais.Remove(loai);
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
