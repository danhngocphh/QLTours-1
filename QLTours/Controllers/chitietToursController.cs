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
    public class chitietToursController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: chitietTours
        public ActionResult Index()
        {
            var chitietTours = db.chitietTours.Include(c => c.diadiem).Include(c => c.tour);
            return View(chitietTours.ToList());
        }

        // GET: chitietTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietTour chitietTour = db.chitietTours.Find(id);
            if (chitietTour == null)
            {
                return HttpNotFound();
            }
            return View(chitietTour);
        }

        // GET: chitietTours/Create
        public ActionResult Create(int id)
        {
            ViewBag.IDTOUR = id;
            ViewBag.TenTour = db.tours.Where(w => w.Id == id).Select(s => s.Ten).FirstOrDefault();
            //DropDownList Thành phố
            string thanhpho = "Đà lạt"; int idtour = id;
            var ThanhPho = db.diadiems.GroupBy(d => new { d.ThanhPho }).Select(g => new { g.Key.ThanhPho });
            ViewBag.ThanhPho = new SelectList(ThanhPho, "ThanhPho", "ThanhPho");
            //DropDownList Tên địa điểm
            var diadiem = db.diadiems.Where(dd => dd.ThanhPho.Contains(thanhpho));
            ViewBag.IdDD = new SelectList(diadiem, "Id", "Ten");
            //DropDownList Id tour
            var Tour = db.tours.Select(t => new { t.Id, t.Ten });
            ViewBag.IDT = new SelectList(Tour, "Id", "Ten");
            //DropDownList những địa điểm của Id tour
            var chitiettour = db.chitietTours.Join(db.diadiems,
                                                    ct => ct.IdDiaDiem,
                                                    dds => dds.Id,
                                                    (ct, dds) => new { TenDiaDiem = dds.Ten, STT = ct.STTDiaDiem, ct.IdTour })
                                                    .Where(j => j.IdTour == idtour)
                                                    .Select(s => new { s.STT, TenDiaDiemSTT = s.TenDiaDiem + "---STT: " + s.STT.ToString() });
            ViewBag.chitiettour = new SelectList(chitiettour, "TenDiaDiemSTT", "TenDiaDiemSTT");
            ViewBag.lastSTT = (chitiettour.Select(s=>s.STT).FirstOrDefault()) > 0 ? 
                               chitiettour.Select(s => s.STT).Max() : 
                               chitiettour.Select(s => s.STT).FirstOrDefault();
            return View();
        }

        // POST: chitietTours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdTour,IdDiaDiem,STTDiaDiem")] List<chitietTour> chitietTour, string thanhpho, string idt, string Saving, string count)
        {
            ViewBag.IDTOUR = idt;
            int id = Convert.ToInt32(idt);
            ViewBag.TenTour = db.tours.Where(w => w.Id == id).Select(s => s.Ten).FirstOrDefault();
            if (ModelState.IsValid && !count.Equals("0") && Saving.Equals("true"))
            {
                db.chitietTours.AddRange(chitietTour);
                db.SaveChanges();
                return RedirectToAction("Details", "tours", new { id = idt });
            }
            //DropDownList Thành phố
            var ThanhPho = db.diadiems.GroupBy(d => new { d.ThanhPho }).Select(g => new { g.Key.ThanhPho });
            ViewBag.ThanhPho = new SelectList(ThanhPho, "ThanhPho", "ThanhPho", thanhpho);
            //DropDownList Tên địa điểm
            var diadiem = db.diadiems.Where(dd => dd.ThanhPho.Contains(thanhpho));
            ViewBag.IdDD = new SelectList(diadiem, "Id", "Ten");
            //DropDownList Id tour
            var Tour = db.tours.Select(t => new { t.Id, t.Ten });
            ViewBag.IDT = new SelectList(Tour, "Id", "Ten", idt);
            //DropDownList những địa điểm của Id tour
            var chitiettour = db.chitietTours.Join(db.diadiems,
                                                    ct => ct.IdDiaDiem,
                                                    dds => dds.Id,
                                                    (ct, dds) => new { TenDiaDiem = dds.Ten, STT = ct.STTDiaDiem, ct.IdTour })
                                                    .Where(j => j.IdTour == id)
                                                    .Select(s => new { s.STT, TenDiaDiemSTT = s.TenDiaDiem + "---STT: " + s.STT.ToString() });
            ViewBag.chitiettour = new SelectList(chitiettour, "STT", "TenDiaDiemSTT");
            ViewBag.lastSTT = (chitiettour.Select(s => s.STT).FirstOrDefault()) > 0 ?
                               chitiettour.Select(s => s.STT).Max() :
                               chitiettour.Select(s => s.STT).FirstOrDefault();

            return View(chitietTour);
        }

        // GET: chitietTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietTour chitietTour = db.chitietTours.Find(id);
            if (chitietTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDiaDiem = new SelectList(db.diadiems, "Id", "ThanhPho", chitietTour.IdDiaDiem);
            ViewBag.IdTour = new SelectList(db.tours, "Id", "Ten", chitietTour.IdTour);
            return View(chitietTour);
        }

        // POST: chitietTours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdTour,IdDiaDiem,STTDiaDiem")] chitietTour chitietTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chitietTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDiaDiem = new SelectList(db.diadiems, "Id", "ThanhPho", chitietTour.IdDiaDiem);
            ViewBag.IdTour = new SelectList(db.tours, "Id", "Ten", chitietTour.IdTour);
            return View(chitietTour);
        }

        // GET: chitietTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietTour chitietTour = db.chitietTours.Find(id);
            if (chitietTour == null)
            {
                return HttpNotFound();
            }
            return View(chitietTour);
        }

        // POST: chitietTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            chitietTour chitietTour = db.chitietTours.Find(id);
            db.chitietTours.Remove(chitietTour);
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
