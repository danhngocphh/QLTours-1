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
    public class toursController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: tours
        public ActionResult Index()
        {
            var tours = db.tours.Include(t => t.gia).Include(t => t.loai);
            return View(tours.ToList());
        }

        // GET: tours/Details/5
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

        // GET: tours/Create
        public ActionResult Create()
        {
            ViewBag.IdLoaiTour = new SelectList(db.loais, "Id", "Ten");
            return View();
        }

        // POST: tours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ten,MoTa,IdLoaiTour,IdGiaTour")] tour tour, [Bind(Include = "Id,SoTien,IdTour,TuNgay,DenNgay")] gia gia)
        {
            var t = db.tours.FirstOrDefault();
            //gia.SoTien = 0;//Hiển thị
            gia.IdTour = t.Id;
            //gia.TuNgay = DateTime.Now;//Hiển thị
            //gia.DenNgay = DateTime.Now;//Hiển thị
            db.gias.Add(gia);
            db.SaveChanges();
            tour.IdGiaTour = gia.Id;

            if (ModelState.IsValid)
            {
                db.tours.Add(tour);
                db.SaveChanges();
                gia.IdTour = tour.Id;
                db.Entry(gia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "tours", new { id = tour.Id });
            }            
            ViewBag.IdLoaiTour = new SelectList(db.loais, "Id", "Ten", tour.IdLoaiTour);
            return View(tour);
        }

        // GET: tours/Edit/5
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
            var gias = db.gias.Where(g => g.IdTour == id);
            ViewBag.IdGiaTour = new SelectList(gias, "Id", "SoTien", tour.IdGiaTour);
            ViewBag.IdLoaiTour = new SelectList(db.loais, "Id", "Ten", tour.IdLoaiTour);
            return View(tour);
        }

        // POST: tours/Edit/5
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
            int id =Convert.ToInt32(Request.QueryString["id"]);
            var gias = db.gias.Where(g => g.IdTour == id);
            ViewBag.IdGiaTour = new SelectList(gias, "Id", "SoTien", tour.IdGiaTour);
            ViewBag.IdLoaiTour = new SelectList(db.loais, "Id", "Ten", tour.IdLoaiTour);
            return View(tour);
        }

        // GET: tours/Delete/5
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

        // POST: tours/Delete/5
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
