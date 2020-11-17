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
    public class nguoidisController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: nguoidis
        public ActionResult Index()
        {
            var nguoidis = db.nguoidis.Include(n => n.doan);
            return View(nguoidis.ToList());
        }

        // GET: nguoidis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nguoidi nguoidi = db.nguoidis.Find(id);
            if (nguoidi == null)
            {
                return HttpNotFound();
            }
            return View(nguoidi);
        }

        // GET: nguoidis/Create
        public ActionResult Create(string StrN, string StrK)
        {
            ViewBag.IdDoan = new SelectList(db.doans, "Id", "Ten");
            ViewBag.StrK = StrK ;
            ViewBag.StrN = StrN; ;
            //-----------------ListBox Danh sách khách----------------------------//
            List<SelectListItem> listKhach = new List<SelectListItem>();
            if(StrK != null)
            {
                string[] IdKs = StrK.Split(',');
                foreach (string item in IdKs)
                {
                    int idk = Convert.ToInt32(item);
                    var k = db.khachhangs.Where(w => w.Id == idk).Select(s => new { Ten_CMND = s.Ten + "---" + s.CMND }).FirstOrDefault();
                    listKhach.Add(new SelectListItem { Text = k.Ten_CMND, Value = k.Ten_CMND });
                }
            }
            ViewBag.DSKhach = listKhach;
            //-----------------ListBox Danh sách nhân viên----------------------------//
            List<SelectListItem> listNhanVien = new List<SelectListItem>();
            if (StrN != null)
            {
                string[] IdNs = StrN.Split(',');
                foreach (string item in IdNs)
                {
                    var n = db.AspNetUsers.Where(w => w.Id.Equals(item)).Select(s => new { Ten_NV = s.TenNhanVien + "---" + s.NhiemVu }).FirstOrDefault();
                    listNhanVien.Add(new SelectListItem { Text = n.Ten_NV, Value = n.Ten_NV });
                }
            }
            ViewBag.DSNhanVien = listNhanVien;

            return View();
        }

        // POST: nguoidis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdDoan,DSNhanvien,DSKhach")] nguoidi nguoidi)
        {
            if (ModelState.IsValid)
            {
                db.nguoidis.Add(nguoidi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDoan = new SelectList(db.doans, "Id", "Ten", nguoidi.IdDoan);
            //-----------------ListBox Danh sách khách----------------------------//
            List<SelectListItem> listKhach = new List<SelectListItem>();
            if (nguoidi.DSKhach != null)
            {
                string[] IdKs = nguoidi.DSKhach.Split(',');
                foreach (string item in IdKs)
                {
                    int idk = Convert.ToInt32(item);
                    var k = db.khachhangs.Where(w => w.Id == idk).Select(s => new { Ten_CMND = s.Ten + "---" + s.CMND }).FirstOrDefault();
                    listKhach.Add(new SelectListItem { Text = k.Ten_CMND, Value = k.Ten_CMND });
                }
            }
            ViewBag.DSKhach = listKhach;
            //-----------------ListBox Danh sách nhân viên----------------------------//            
            List<SelectListItem> listNhanVien = new List<SelectListItem>();
            if(nguoidi.DSNhanvien != null)
            {
                string[] IdNs = nguoidi.DSNhanvien.Split(',');
                foreach (string item in IdNs)
                {
                    var n = db.AspNetUsers.Where(w => w.Id.Equals(item)).Select(s => new { Ten_NV = s.TenNhanVien + "---" + s.NhiemVu }).FirstOrDefault();
                    listNhanVien.Add(new SelectListItem { Text = n.Ten_NV, Value = n.Ten_NV });
                }
            }
            ViewBag.DSNhanVien = listNhanVien;

            return View(nguoidi);
        }

        // GET: nguoidis/Edit/5
        public ActionResult Edit(int? id, string StrN, string StrK)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nguoidi nguoidi = db.nguoidis.Find(id);
            if (nguoidi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDoan = new SelectList(db.doans, "Id", "Ten", nguoidi.IdDoan);
            ViewBag.StrK = StrK ?? nguoidi.DSKhach;
            ViewBag.StrN = StrN ?? nguoidi.DSNhanvien;
            //-----------------ListBox Danh sách khách----------------------------//
            string KStr = StrK ?? nguoidi.DSKhach;//(StrK == null) ? nguoidi.DSKhach : StrK;
            string[] IdKs = KStr.Split(',');
            List<SelectListItem> listKhach = new List<SelectListItem>();
            foreach (string item in IdKs)
            {
                int idk = Convert.ToInt32(item);
                var k = db.khachhangs.Where(w => w.Id == idk).Select(s => new { Ten_CMND = s.Ten + "---" + s.CMND }).FirstOrDefault();
                listKhach.Add(new SelectListItem { Text = k.Ten_CMND, Value = k.Ten_CMND });
            }
            ViewBag.DSKhach = listKhach;
            //-----------------ListBox Danh sách nhân viên----------------------------//
            string NvStr = StrN ?? nguoidi.DSNhanvien;//(StrN == null) ? nguoidi.DSNhanvien : StrN;
            string[] IdNs = NvStr.Split(',');
            List<SelectListItem> listNhanVien = new List<SelectListItem>();
            foreach (string item in IdNs)
            {
                var n = db.AspNetUsers.Where(w => w.Id.Equals(item)).Select(s => new { Ten_NV = s.TenNhanVien + "---" + s.NhiemVu }).FirstOrDefault();
                listNhanVien.Add(new SelectListItem { Text = n.Ten_NV, Value = n.Ten_NV });
            }
            ViewBag.DSNhanVien = listNhanVien;

            return View(nguoidi);
        }

        // POST: nguoidis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdDoan,DSNhanvien,DSKhach")] nguoidi nguoidi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoidi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDoan = new SelectList(db.doans, "Id", "Ten", nguoidi.IdDoan);
            //-----------------ListBox Danh sách khách----------------------------//            
            List<SelectListItem> listKhach = new List<SelectListItem>();
            if (nguoidi.DSKhach != null)
            {
                string[] IdKs = nguoidi.DSKhach.Split(',');
                foreach (string item in IdKs)
                {
                    int idk = Convert.ToInt32(item);
                    var k = db.khachhangs.Where(w => w.Id == idk).Select(s => new { Ten_CMND = s.Ten + "---" + s.CMND }).FirstOrDefault();
                    listKhach.Add(new SelectListItem { Text = k.Ten_CMND, Value = k.Ten_CMND });
                }
            }
            ViewBag.DSKhach = listKhach;
            //-----------------ListBox Danh sách nhân viên----------------------------// 
            List<SelectListItem> listNhanVien = new List<SelectListItem>();
            if(nguoidi.DSNhanvien != null)
            {
                string[] IdNs = nguoidi.DSNhanvien.Split(',');
                foreach (string item in IdNs)
                {
                    var n = db.AspNetUsers.Where(w => w.Id.Equals(item)).Select(s => new { Ten_NV = s.TenNhanVien + "---" + s.NhiemVu }).FirstOrDefault();
                    listNhanVien.Add(new SelectListItem { Text = n.Ten_NV, Value = n.Ten_NV });
                }
            }
            ViewBag.DSNhanVien = listNhanVien;

            return View(nguoidi);
        }

        // GET: nguoidis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nguoidi nguoidi = db.nguoidis.Find(id);
            if (nguoidi == null)
            {
                return HttpNotFound();
            }
            return View(nguoidi);
        }

        // POST: nguoidis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            nguoidi nguoidi = db.nguoidis.Find(id);
            db.nguoidis.Remove(nguoidi);
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

        // GET: khachhangs
        public ActionResult DSKhachHangEd(int iD, string StrN, string StrK)
        {
            ViewBag.Id = iD;
            ViewBag.StrN = StrN;
            ViewBag.StrK = StrK;
            return View(db.khachhangs.ToList());
        }

        // GET: nhanviens
        public ActionResult DSNhanVienEd(int iD, string StrN, string StrK)
        {
            ViewBag.Id = iD;
            ViewBag.StrN = StrN;
            ViewBag.StrK = StrK;
            return View(db.AspNetUsers.ToList());
        }

        [HttpPost]
        public ActionResult DSKhachHangEd(int iD, string[] IdK, string StrN, string StrK)
        {
            if (IdK != null)
            {
                string IdkSTR = "/";
                foreach (string item in IdK)
                {
                    IdkSTR += "," + item;
                }
                StrK = IdkSTR.Replace("/,", "");
            }
            return RedirectToAction("Edit", new { id = iD, StrN, StrK });
        }

        [HttpPost]
        public ActionResult DSNhanVienEd(int iD, string[] IdNv, string StrN, string StrK)
        {
            if (IdNv != null)
            {
                string IdNvSTR = "/";
                foreach (string item in IdNv)
                {
                    IdNvSTR += "," + item;
                }
                StrN = IdNvSTR.Replace("/,", "");
            }
            return RedirectToAction("Edit", new { id = iD, StrN, StrK });
        }
        /*---------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------*/
        // GET: khachhangs
        public ActionResult DSKhachHangCr(string StrN, string StrK)
        {
            ViewBag.StrN = StrN;
            ViewBag.StrK = StrK;
            return View(db.khachhangs.ToList());
        }

        // GET: nhanviens
        public ActionResult DSNhanVienCr(string StrN, string StrK)
        {            
            ViewBag.StrN = StrN;
            ViewBag.StrK = StrK;
            return View(db.AspNetUsers.ToList());
        }

        [HttpPost]
        public ActionResult DSKhachHangCr(string[] IdK, string StrN, string StrK)
        {
            if (IdK != null)
            {
                string IdkSTR = "/";
                foreach (string item in IdK)
                {
                    IdkSTR += "," + item;
                }
                StrK = IdkSTR.Replace("/,", "");
            }
            return RedirectToAction("Create", new { StrN, StrK });
        }

        [HttpPost]
        public ActionResult DSNhanVienCr(string[] IdNv, string StrN, string StrK)
        {
            if (IdNv != null)
            {
                string IdNvSTR = "/";
                foreach (string item in IdNv)
                {
                    IdNvSTR += "," + item;
                }
                StrN = IdNvSTR.Replace("/,", "");
            }
            return RedirectToAction("Create", new { StrN, StrK });
        }
    }
}
