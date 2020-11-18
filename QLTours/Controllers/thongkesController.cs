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
        public class ShiftsModel
        {
            public string TenDoan { get; set; }
            public int DSnguoidi { get; set; }
            public double GiaTour { get; set; }
            public double Doanhthu { get; set; }
            public double Total { get; set; }
            public double Lai { get; set; }

            public ShiftsModel(string TenDoan, int DSnguoidi, double GiaTour, double Total, double Doanhthu, double Lai)
            {
                this.TenDoan = TenDoan;
                this.DSnguoidi = DSnguoidi;
                this.GiaTour = GiaTour;
                this.Doanhthu = Doanhthu;
                this.Total = Total;
                this.Lai = Lai;
            }
        }
        public class ShiftsModeltour
        {
            public string TenTour { get; set; }
            public int Tongsodoan { get; set; }
            public double Tongdoanhthu { get; set; }
            public double Tongchiphi { get; set; }
            public double Tonglai { get; set; }

            public ShiftsModeltour(string TenTour, int Tongsodoan, double Tongdoanhthu, double Tongchiphi, double Tonglai)
            {
                this.TenTour = TenTour;
                this.Tongsodoan = Tongsodoan;
                this.Tongdoanhthu = Tongdoanhthu;
                this.Tongchiphi = Tongchiphi;
                this.Tonglai = Tonglai;
            }
        }

        // GET: thongkes
        public ActionResult Index()
        {
            List<ShiftsModel> ketqua = new List<ShiftsModel>();


            var cttour = db.doans.Select(s => s.Id);
            var chitiettour = db.doans.Join(db.nguoidis,
                                                        d => d.Id,
                                                        nd => nd.IdDoan,
                                                        (d, nd) => new { TenDoan = d.Ten, DSnguoidi = nd.DSKhach, IDtours = d.IdTour, IDdoan = d.Id })
                                                        .Join(db.tours,
                                                        d => d.IDtours,
                                                        t => t.Id,
                                                        (d, t) => new { IdGiaTour = t.IdGiaTour, Loaitour = t.IdLoaiTour, IDdoan = d.IDdoan, d.DSnguoidi, d.TenDoan, d.IDtours })
                                                        .Join(db.chiphis,
                                                        ct => ct.IDdoan,
                                                        cp => cp.IdDoan,
                                                        (ct, cp) => new { cp.Total, ct.IDdoan, ct.IdGiaTour, ct.TenDoan, ct.DSnguoidi, ct.IDtours })
                                                        .Join(db.gias,
                                                        ctt => ctt.IdGiaTour,
                                                        g => g.Id,
                                                        (ctt, g) => new { ctt.Total, ctt.IDdoan, g.SoTien, ctt.TenDoan, ctt.DSnguoidi, ctt.IDtours })

                                                        .Select(s => new { s.TenDoan, s.DSnguoidi, s.SoTien, s.Total, s.IDdoan });
            foreach (var doa in cttour)

            {
                string[] khach;
                int totalElements = 0;
                double doanhthu = 0;
                double lai = 0;
                string TenDoan = "";
                double SoTien = 0;
                double Total = 0;
                int co = 0;
                foreach (var item in chitiettour)
                {
                    if(doa == item.IDdoan)
                    {
                        khach = item.DSnguoidi.Split(',');
                        totalElements = khach.Count();
                        doanhthu = (double)totalElements * item.SoTien;
                        
                        TenDoan = item.TenDoan;
                        SoTien = item.SoTien;
                        Total += item.Total;
                        co = 1;

                    }
                    
                    
                    

                }
                lai = doanhthu - Total;
                if(co == 1)
                {
                    ketqua.Add(new ShiftsModel(TenDoan, totalElements,SoTien, doanhthu, Total, lai));

                }
               

            }
                


            ViewBag.shifts = ketqua; // this line will pass your object but now to model

            return View(chitiettour.ToList());
        }

        // GET: thongkes/Details/5
        public ActionResult Details()
        {
            List<ShiftsModeltour> ketqua = new List<ShiftsModeltour>();


            var cttour = db.tours.Select(s => new {  s.Id, s.Ten });
            var ctchiphi = db.chiphis.GroupBy(s => s.IdDoan).Select(s => new { Total = s.Sum(d => d.Total), s.Key});
            var doandi = db.tours.Join(db.doans,
                                                        d => d.Id,
                                                        t => t.IdTour,
                                                        (d, t) => new { IDtour = t.Ten,  t.IdTour })
                                                        .Select(s => new { s.IDtour, s.IdTour });


            var chitiettour = db.doans.Join(db.nguoidis,
                                                        d => d.Id,
                                                        nd => nd.IdDoan,
                                                        (d, nd) => new { TenDoan = d.Ten, DSnguoidi = nd.DSKhach, IDtours = d.IdTour, IDdoan = d.Id })
                                                        .Join(db.tours,
                                                        d => d.IDtours,
                                                        t => t.Id,
                                                        (d, t) => new { IdGiaTour = t.IdGiaTour, Loaitour = t.IdLoaiTour, IDdoan = d.IDdoan, d.DSnguoidi, d.TenDoan, d.IDtours })
                                                        .Join(db.chiphis,
                                                        ct => ct.IDdoan,
                                                        cp => cp.IdDoan,
                                                        (ct, cp) => new { ct.IDdoan, ct.IdGiaTour, ct.TenDoan, ct.DSnguoidi, ct.IDtours,cp.Total })
                                                        .Join(db.gias,
                                                        ctt => ctt.IDtours,
                                                        g => g.Id,
                                                        (ctt, g) => new { ctt.IDdoan, g.SoTien, ctt.TenDoan, ctt.DSnguoidi, ctt.IDtours, ctt.Total })

                                                        .Select(s => new { s.TenDoan, s.DSnguoidi, s.SoTien, s.IDtours, s.IDdoan , s.Total });
            foreach (var tours in cttour)
            {


                double tongdoanhthu = 0;
                double tongchiphi = 0;
                double tonglai = 0;
                int tongdoan = 0;
                foreach (var item in chitiettour)
                {
                    
                    if (item.IDtours == tours.Id)
                    {
                        
                                tongchiphi += item.Total;

                    }
                    

                        
                        
                    
                }

                foreach (var ctdoandi in doandi)
                {
                    if(tours.Id == ctdoandi.IdTour)
                    {
                        ++tongdoan;
                    }
                }
                foreach (var ctt in cttour)
                {
                    foreach (var item in chitiettour)
                    {
                        if (tours.Id == ctt.Id)
                        {
                            tongdoanhthu += item.SoTien;
                        }
                    }
                }
                tonglai = tongdoanhthu - tongchiphi;
                if(tongdoan>0)
                {
                    ketqua.Add(new ShiftsModeltour(tours.Ten, tongdoan, tongdoanhthu, tongchiphi, tonglai));
                }
                
            }


            ViewBag.shifts = ketqua; // this line will pass your object but now to model

            return View(chitiettour.ToList());
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
