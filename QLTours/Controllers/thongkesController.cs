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
    public class thongkesController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: thongkes
        public ActionResult Index()
        {
            var tours = db.tours.Include(t => t.gia).Include(t => t.loai);
            return View(tours.ToList());
        }

        // GET: thongkes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour tour = db.tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: thongkes/Create
        public ActionResult Create()
        {
            ViewBag.IdGiaTour = new SelectList(db.gias, "Id", "Id");
            ViewBag.IdLoaiTour = new SelectList(db.loais, "Id", "Ten");
            return View();
        }

        // POST: thongkes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ten,MoTa,IdLoaiTour,IdGiaTour")] tour tour)
        {
            if (ModelState.IsValid)
            {
                db.tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdGiaTour = new SelectList(db.gias, "Id", "Id", tour.IdGiaTour);
            ViewBag.IdLoaiTour = new SelectList(db.loais, "Id", "Ten", tour.IdLoaiTour);
            return View(tour);
        }

        // GET: thongkes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour tour = db.tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGiaTour = new SelectList(db.gias, "Id", "Id", tour.IdGiaTour);
            ViewBag.IdLoaiTour = new SelectList(db.loais, "Id", "Ten", tour.IdLoaiTour);
            return View(tour);
        }

        // POST: thongkes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ten,MoTa,IdLoaiTour,IdGiaTour")] tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdGiaTour = new SelectList(db.gias, "Id", "Id", tour.IdGiaTour);
            ViewBag.IdLoaiTour = new SelectList(db.loais, "Id", "Ten", tour.IdLoaiTour);
            return View(tour);
        }

        // GET: thongkes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour tour = db.tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: thongkes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour tour = db.tours.Find(id);
            db.tours.Remove(tour);
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
