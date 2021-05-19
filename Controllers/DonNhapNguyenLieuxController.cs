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
    public class DonNhapNguyenLieuxController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/DonNhapNguyenLieux
        public System.Object GetDonNhapNguyenLieux()
        {
            var result = (from a in db.DonNhapNguyenLieux
                          join b in db.NhanViens on a.NV_ID equals b.NV_ID
                          join c in db.NhaCungCaps on a.NCC_ID equals c.NCC_ID


                          select new
                          {
                              a.DonNhap_ID,
                              a.DonNhap_NgayNhap,
                              a.DonNhap_TongTien,
                              c.NCC_Ten,
                              b.NV_Ten


                          }).ToList();

            return result;
        }

        // GET: api/DonNhapNguyenLieux/5
        [ResponseType(typeof(DonNhapNguyenLieu))]
        public IHttpActionResult GetDonNhapNguyenLieu(int id)
        {
            DonNhapNguyenLieu donNhapNguyenLieu = db.DonNhapNguyenLieux.Find(id);
            if (donNhapNguyenLieu == null)
            {
                return NotFound();
            }

            return Ok(donNhapNguyenLieu);
        }

        // PUT: api/DonNhapNguyenLieux/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDonNhapNguyenLieu(int id, DonNhapNguyenLieu donNhapNguyenLieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != donNhapNguyenLieu.DonNhap_ID)
            {
                return BadRequest();
            }

            db.Entry(donNhapNguyenLieu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonNhapNguyenLieuExists(id))
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

        // POST: api/DonNhapNguyenLieux
        [ResponseType(typeof(DonNhapNguyenLieu))]
        public IHttpActionResult PostDonNhapNguyenLieu(DonNhapNguyenLieu donNhapNguyenLieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DonNhapNguyenLieux.Add(donNhapNguyenLieu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = donNhapNguyenLieu.DonNhap_ID }, donNhapNguyenLieu);
        }

        // DELETE: api/DonNhapNguyenLieux/5
        [ResponseType(typeof(DonNhapNguyenLieu))]
        public IHttpActionResult DeleteDonNhapNguyenLieu(int id)
        {
            DonNhapNguyenLieu donNhapNguyenLieu = db.DonNhapNguyenLieux.Find(id);
            if (donNhapNguyenLieu == null)
            {
                return NotFound();
            }

            db.DonNhapNguyenLieux.Remove(donNhapNguyenLieu);
            db.SaveChanges();

            return Ok(donNhapNguyenLieu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DonNhapNguyenLieuExists(int id)
        {
            return db.DonNhapNguyenLieux.Count(e => e.DonNhap_ID == id) > 0;
        }
    }
}