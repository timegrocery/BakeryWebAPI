using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class HoaDonsController : ApiController
    {


        private DBModel db = new DBModel();

        public HoaDonsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }


        // GET: api/HoaDons
        public System.Object GetHoaDons()
        {
            var result = (from a in db.HoaDons
                          join b in db.BanAns on a.Ban_ID equals b.Ban_ID
                          join c in db.NhanViens on a.NV_ID equals c.NV_ID

                          select new
                          {
                              a.HoaDon_ID,
                              BanAn = b.Ban_Ten,
                              b.Ban_Loai,
                              NhanVien = c.NV_Ten,
                              a.HoaDon_ThoiGianVao,
                              a.HoaDon_ThoiGianRa,
                              a.HoaDon_TongTien,
                              a.HoaDon_TrangThai
                          }).ToList();

            return result;
        }

        // GET: api/HoaDons/5
        [Route("api/HOADON/{id}")]
        [ResponseType(typeof(HoaDon))]
        public IHttpActionResult GetHoaDon(int id)
        {
            var hoaDon = (from a in db.HoaDons
                          where a.Ban_ID == id

                          select new
                          {
                              a.HoaDon_ID,
                              a.Ban_ID,
                              a.NV_ID,
                              a.HoaDon_ThoiGianVao,
                              a.HoaDon_ThoiGianRa,
                              a.HoaDon_TongTien,
                              a.HoaDon_TrangThai,
                              DeletedOrderItemIDs = ""
                          }).FirstOrDefault();

            var chiTietHoaDon = (from a in db.ChiTietHoaDons.Distinct()
                                 join b in db.MonAns on a.MonAn_ID equals b.MonAn_ID
                                 join c in db.HoaDons on a.HoaDon_ID equals c.HoaDon_ID
                                 where a.HoaDon_ID == id && c.HoaDon_TrangThai == "Chưa hoàn thành"

                                 select new
                                 {
                                     a.CTHoaDon_ID,
                                     a.HoaDon_ID,
                                     a.MonAn_ID,
                                     MonAn = b.MonAn_Ten,
                                     a.ChiTietHD_DonGia,
                                     a.ChiTietHD_SoLuong,
                                     a.ChiTietHD_GhiChu,
                                     a.ChiTietHD_TrangThai,

                                 }).ToList();

            return Ok(new { hoaDon, chiTietHoaDon });
        }



        // POST: api/HoaDons
        [ResponseType(typeof(HoaDon))]
        public IHttpActionResult PostHoaDon(HoaDon hoaDon)
        {
            var hoaDonDb = db.HoaDons.Where(x => x.HoaDon_TrangThai.Equals("Chưa hoàn thành") && x.Ban_ID == hoaDon.Ban_ID).FirstOrDefault();
            if (hoaDonDb == null)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                var hoaDonTmp = db.HoaDons.Where(x => x.HoaDon_TrangThai.Equals("Chưa hoàn thành") && x.Ban_ID == hoaDon.Ban_ID).FirstOrDefault();

                return Ok(hoaDonTmp);
            }
            else
            {
                return Ok(hoaDonDb);
            }

        }


        [Route("api/HoaDons/UpdateStatusHoaDon")]
        [ResponseType(typeof(void))]
        public IHttpActionResult GetUpdateHoaDon(int HoaDon_ID, DateTime time)
        {
            var hoaDonDB = db.HoaDons.Where(x => x.HoaDon_ID == HoaDon_ID).FirstOrDefault();
            if(hoaDonDB != null)
            {
                hoaDonDB.HoaDon_TrangThai = "Đã hoàn thành";
                hoaDonDB.HoaDon_ThoiGianRa = time;
                db.SaveChanges();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/GetHoaDonID")]
        [ResponseType(typeof(HoaDon))]
        public IHttpActionResult GetHoaDonID(int BanAn_ID)
        {
            var hoaDon = db.HoaDons.Where(x => x.Ban_ID == BanAn_ID && x.HoaDon_TrangThai.Equals("Chưa hoàn thành")).FirstOrDefault();
            return Ok(hoaDon);
        }


        // DELETE: api/HoaDons/5
        [ResponseType(typeof(HoaDon))]
        public IHttpActionResult DeleteHoaDon(int id)
        {
            HoaDon hoaDon = db.HoaDons.Include(y => y.ChiTietHoaDons)
                .SingleOrDefault(x => x.HoaDon_ID == id);

            foreach (var item in hoaDon.ChiTietHoaDons.ToList())
            {
                db.ChiTietHoaDons.Remove(item);
            }

            db.HoaDons.Remove(hoaDon);
            db.SaveChanges();

            return Ok(hoaDon);
        }


        // Update Total money
        [Route("api/HoaDons/UpdateTotalMoney")]
        [ResponseType(typeof(HoaDon))]
        public IHttpActionResult GetTotalMoney(int totalMoney,int HoaDon_ID)
        {
            var hoaDon = db.HoaDons.Where(x => x.HoaDon_ID == HoaDon_ID).FirstOrDefault();
            if(hoaDon != null)
            {
                hoaDon.HoaDon_TongTien = totalMoney;
                db.SaveChanges();
            }
            return Ok(hoaDon);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HoaDonExists(int id)
        {
            return db.HoaDons.Count(e => e.HoaDon_ID == id) > 0;
        }
       
    }
}