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
    public class giasController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: gias
        public ActionResult Index()
        {
            var gias = db.gias.Include(g => g.tour);
            return View(gias.ToList());
        }

        // GET: gias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gia gia = db.gias.Find(id);
            if (gia == null)
            {
                return HttpNotFound();
            }
            return View(gia);
        }

        // GET: gias/Create
        public ActionResult Create(int id)
        {
            var tours = db.tours.Find(id);
            ViewBag.TourId = id;
            ViewBag.TourTen = tours.Ten;
            return View();
        }

        // POST: gias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SoTien,IdTour,TuNgay,DenNgay")] gia gia)
        {
            if (ModelState.IsValid)
            {
                db.gias.Add(gia);
                db.SaveChanges();
                return RedirectToAction("Details", "tours", new { id = gia.IdTour });
            }
            int id = Convert.ToInt32(Request.QueryString["id"]);
            var tours = db.tours.Find(id);
            ViewBag.TourId = id;
            ViewBag.TourTen = tours.Ten;
            return View(gia);
        }

        // GET: gias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gia gia = db.gias.Find(id);
            if (gia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTour = new SelectList(db.tours, "Id", "Ten", gia.IdTour);
            return View(gia);
        }

        // POST: gias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SoTien,IdTour,TuNgay,DenNgay")] gia gia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTour = new SelectList(db.tours, "Id", "Ten", gia.IdTour);
            return View(gia);
        }

        // GET: gias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gia gia = db.gias.Find(id);
            if (gia == null)
            {
                return HttpNotFound();
            }
            return View(gia);
        }

        // POST: gias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gia gia = db.gias.Find(id);
            db.gias.Remove(gia);
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
