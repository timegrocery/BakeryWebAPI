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
    public class ChiTietHoaDonsController : ApiController
    {
        private DBModel db = new DBModel();

        public ChiTietHoaDonsController() 
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/ChiTietHoaDons
        public System.Object GetChiTietHoaDons()
        {
            var result = (from a in db.ChiTietHoaDons
                          join b in db.MonAns on a.MonAn_ID equals b.MonAn_ID
                          join c in db.HoaDons on a.HoaDon_ID equals c.HoaDon_ID

                          select new
                          {
                              a.CTHoaDon_ID,
                              c.HoaDon_ID,
                              b.MonAn_Ten,
                              b.MonAn_HinhAnh,
                              a.ChiTietHD_SoLuong,
                              a.ChiTietHD_DonGia,
                              a.ChiTietHD_GhiChu,
                              a.ChiTietHD_TrangThai

                          }).ToList();

            return result;
        }

        // GET: api/ChiTietHoaDons/5
        [ResponseType(typeof(ChiTietHoaDon))]
        public IHttpActionResult GetChiTietHoaDon(int id)
        {
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }

            return Ok(chiTietHoaDon);
        }
        
        // GET: api/ChiTietHoaDons/5
        // lấy chi tiết hóa đơn của 1 hóa đơn
        [Route("api/GetCTHDOFHD")]
        [ResponseType(typeof(ChiTietHoaDon[]))]
        public IHttpActionResult GetCTHDOHD(int HoaDon_id)
        {
            var listChiTietHD = db.ChiTietHoaDons.Where(x => x.HoaDon_ID == HoaDon_id);
            return Ok(listChiTietHD);
        }

        // PUT: api/ChiTietHoaDons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChiTietHoaDon(ChiTietHoaDon chiTietHoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

         

            db.Entry(chiTietHoaDon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietHoaDonExists(chiTietHoaDon.HoaDon_ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ChiTietHoaDons
        [ResponseType(typeof(ChiTietHoaDon))]
        public IHttpActionResult PostChiTietHoaDon(ChiTietHoaDon chiTietHoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChiTietHoaDons.Add(chiTietHoaDon);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = chiTietHoaDon.CTHoaDon_ID }, chiTietHoaDon);
        }

        // DELETE: api/ChiTietHoaDons/5
        [ResponseType(typeof(ChiTietHoaDon))]
        public IHttpActionResult DeleteChiTietHoaDon(int id)
        {
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }

            db.ChiTietHoaDons.Remove(chiTietHoaDon);
            db.SaveChanges();

            return Ok(chiTietHoaDon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChiTietHoaDonExists(int id)
        {
            return db.ChiTietHoaDons.Count(e => e.CTHoaDon_ID == id) > 0;
        }

        

       
    }
}