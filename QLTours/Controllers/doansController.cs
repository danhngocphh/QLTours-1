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
    public class doansController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: doans
        public ActionResult Index()
        {
            var doans = db.doans.Include(d => d.tour);
            return View(doans.ToList());
        }

        // GET: doans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            doan doan = db.doans.Find(id);
            if (doan == null)
            {
                return HttpNotFound();
            }
            return View(doan);
        }

        // GET: doans/Create
        public ActionResult Create(int id)
        {
            var tour = db.tours.Find(id);
            ViewBag.TourTen = tour.Ten;
            ViewBag.TourId = id;

            return View();
        }

        // POST: doans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdTour,Ten,NgayDi,NgayVe,ChiTietChuongTrinh")] doan doan)
        {
            if (ModelState.IsValid)
            {
                db.doans.Add(doan);
                db.SaveChanges();
                return RedirectToAction("Details", "tours", new { id = doan.IdTour });
            }
            int id = Convert.ToInt32(Request.QueryString["id"]);
            var tour = db.tours.Find(id);
            ViewBag.TourTen = tour.Ten;
            ViewBag.TourId = id;
            return View(doan);
        }

        // GET: doans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            doan doan = db.doans.Find(id);
            if (doan == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTour = new SelectList(db.tours, "Id", "Ten", doan.IdTour);
            return View(doan);
        }

        // POST: doans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdTour,Ten,NgayDi,NgayVe,ChiTietChuongTrinh")] doan doan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTour = new SelectList(db.tours, "Id", "Ten", doan.IdTour);
            return View(doan);
        }

        // GET: doans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            doan doan = db.doans.Find(id);
            if (doan == null)
            {
                return HttpNotFound();
            }
            return View(doan);
        }

        // POST: doans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            doan doan = db.doans.Find(id);
            db.doans.Remove(doan);
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
