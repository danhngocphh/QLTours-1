using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTours.Models;
using System.Web.Mvc;

namespace QLTours.Controllers
{
    public class thongkesController : Controller
    {
        private QLToursModels db = new QLToursModels();

        public class dstour
        {
            public int Id { set; get; }
            public string Name { set; get; }
        }
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



            var cttour = db.tours.Join(db.gias,
                                                        d => d.IdGiaTour,
                                                        t => t.Id,
                                                        (d, t) => new { IDtour = d.Id, d.Ten, t.SoTien })
                                                        .Select(s => new { s.IDtour, s.Ten, s.SoTien });
            var ctchiphi = db.chiphis.GroupBy(s => s.IdDoan).Select(s => new { Total = s.Sum(d => d.Total), s.Key});
            var doandi = db.tours.Join(db.doans,
                                                        d => d.Id,
                                                        t => t.IdTour,
                                                        (d, t) => new { IDtour = t.Ten,  t.IdTour ,t.Id})
                                                        .Join(db.nguoidis,
                                                        d => d.Id,
                                                        t => t.IdDoan,
                                                        (d, t) => new { d.IDtour, d.IdTour, t.DSKhach })
                                                        .Select(s => new { s.IDtour, s.IdTour, s.DSKhach });


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
                string[] khach;
                int totalElements = 0;
                double doanhthu = 0;
                var tongdt = new Dictionary<int, object>();



                double tongdoanhthu = 0;
                double tongchiphi = 0;
                double tonglai = 0;
                int tongdoan = 0;
                foreach (var item in chitiettour)
                {
                    
                    if (item.IDtours == tours.IDtour)
                    {
                        
                                tongchiphi += item.Total;

                    }
                    

                        
                        
                    
                }

                foreach (var ctdoandi in doandi)
                {
                    if(tours.IDtour == ctdoandi.IdTour)
                    {
                        ++tongdoan;
                    }
                }foreach (var ctt in cttour)
                     {foreach (var ctdoandi in doandi)
                    {
                
                    
                        if (ctt.IDtour == ctdoandi.IdTour)
                        {
                            khach = ctdoandi.DSKhach.Split(',');
                            totalElements = khach.Count();
                            doanhthu = ctt.SoTien * (double)totalElements;
                            tongdoanhthu += doanhthu;
                            
                        }
                    }
                    tongdt.Add(ctt.IDtour, tongdoanhthu);
                }
                
                if(tongdoan>0)
                {
                    object dt = tongdt[tours.IDtour];
                    tonglai = (double)dt - tongchiphi;
                    ketqua.Add(new ShiftsModeltour(tours.Ten, tongdoan, (double)dt, tongchiphi, tonglai));
                }
                
            }


            ViewBag.shifts = ketqua; // this line will pass your object but now to model

            return View(chitiettour.ToList());
        }
        public class Toursss {   
            public int id { get; set; }
            public string Ten { get; set; }
        }

        // GET: thongkes/Create
        public ActionResult Create(int idd)
        {
            ViewBag.IDTOUR = idd;
            ViewBag.TenTour = db.tours.Where(w => w.Id == idd).Select(s => s.Ten).FirstOrDefault();
            //DropDownList Thành phố
            string tttour = "Đà lạt"; int idtour = idd;
            var ttour = db.tours.Select(g => new { g.Id, g.Ten });
            
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
                                                        .Where(d => d.IDtours == idd)

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
                    if (doa == item.IDdoan)
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
                if (co == 1)
                {
                    ketqua.Add(new ShiftsModel(TenDoan, totalElements, SoTien, doanhthu, Total, lai));

                }


            }



            ViewBag.shifts = ketqua; // this line will pass your object but now to model
            ViewBag.tttour = chitiettour;

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
