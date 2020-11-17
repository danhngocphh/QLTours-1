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
    public class loaichiphisController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: loaichiphis
        public ActionResult Index()
        {
            return View(db.loaichiphis.ToList());
        }

        // GET: loaichiphis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loaichiphi loaichiphi = db.loaichiphis.Find(id);
            if (loaichiphi == null)
            {
                return HttpNotFound();
            }
            return View(loaichiphi);
        }

        // GET: loaichiphis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: loaichiphis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ten,Mota")] loaichiphi loaichiphi)
        {
            if (ModelState.IsValid)
            {
                db.loaichiphis.Add(loaichiphi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaichiphi);
        }

        // GET: loaichiphis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loaichiphi loaichiphi = db.loaichiphis.Find(id);
            if (loaichiphi == null)
            {
                return HttpNotFound();
            }
            return View(loaichiphi);
        }

        // POST: loaichiphis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ten,Mota")] loaichiphi loaichiphi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaichiphi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaichiphi);
        }

        // GET: loaichiphis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loaichiphi loaichiphi = db.loaichiphis.Find(id);
            if (loaichiphi == null)
            {
                return HttpNotFound();
            }
            return View(loaichiphi);
        }

        // POST: loaichiphis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            loaichiphi loaichiphi = db.loaichiphis.Find(id);
            db.loaichiphis.Remove(loaichiphi);
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
