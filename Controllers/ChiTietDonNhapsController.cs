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
    public class ChiTietDonNhapsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/ChiTietDonNhaps
        public System.Object GetChiTietDonNhaps()
        {
            var result = (from a in db.ChiTietDonNhaps
                          join b in db.DonNhapNguyenLieux on a.DonNhap_ID equals b.DonNhap_ID
                          join c in db.NguyenLieux on a.NL_ID equals c.NL_ID

                          select new
                          {
                              a.CTDonNhap_ID,
                              c.NL_Ten,
                              a.ChiTietDon_DonViTinh,
                              a.ChiTietDon_SoLuong,
                              a.ChiTietDon_DonGia,
                              a.ChiTietDon_ThanhTien,
                              a.DonNhap_ID
                          }).ToList();

            return result;
        }

        // GET: api/ChiTietDonNhaps/5
        [ResponseType(typeof(ChiTietDonNhap))]
        public IHttpActionResult GetChiTietDonNhap(int id)
        {
            ChiTietDonNhap chiTietDonNhap = db.ChiTietDonNhaps.Find(id);
            if (chiTietDonNhap == null)
            {
                return NotFound();
            }

            return Ok(chiTietDonNhap);
        }

        // PUT: api/ChiTietDonNhaps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChiTietDonNhap(int id, ChiTietDonNhap chiTietDonNhap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chiTietDonNhap.CTDonNhap_ID)
            {
                return BadRequest();
            }

            db.Entry(chiTietDonNhap).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietDonNhapExists(id))
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

        // POST: api/ChiTietDonNhaps
        [ResponseType(typeof(ChiTietDonNhap))]
        public IHttpActionResult PostChiTietDonNhap(ChiTietDonNhap chiTietDonNhap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChiTietDonNhaps.Add(chiTietDonNhap);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = chiTietDonNhap.CTDonNhap_ID }, chiTietDonNhap);
        }

        // DELETE: api/ChiTietDonNhaps/5
        [ResponseType(typeof(ChiTietDonNhap))]
        public IHttpActionResult DeleteChiTietDonNhap(int id)
        {
            ChiTietDonNhap chiTietDonNhap = db.ChiTietDonNhaps.Find(id);
            if (chiTietDonNhap == null)
            {
                return NotFound();
            }

            db.ChiTietDonNhaps.Remove(chiTietDonNhap);
            db.SaveChanges();

            return Ok(chiTietDonNhap);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChiTietDonNhapExists(int id)
        {
            return db.ChiTietDonNhaps.Count(e => e.CTDonNhap_ID == id) > 0;
        }
    }
}