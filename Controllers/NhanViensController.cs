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
    public class NhanViensController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/NhanViens
        public System.Object GetNhanViens()
        {
            var result = (from a in db.NhanViens


                          select new
                          {
                              a.NV_ID,
                              a.NV_Ten,
                              a.NV_ChucVu,
                              a.NV_SDT,
                              a.NV_DiaChi

                          }).ToList();

            return result;
        }

        // GET: api/NhanViens/5
        [ResponseType(typeof(NhanVien))]
        public IHttpActionResult GetNhanVien(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return Ok(nhanVien);
        }

        // PUT: api/NhanViens/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNhanVien(int id, NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nhanVien.NV_ID)
            {
                return BadRequest();
            }

            db.Entry(nhanVien).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(id))
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

        // POST: api/NhanViens
        [ResponseType(typeof(NhanVien))]
        public IHttpActionResult PostNhanVien(NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NhanViens.Add(nhanVien);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nhanVien.NV_ID }, nhanVien);
        }

        // DELETE: api/NhanViens/5
        [ResponseType(typeof(NhanVien))]
        public IHttpActionResult DeleteNhanVien(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            db.NhanViens.Remove(nhanVien);
            db.SaveChanges();

            return Ok(nhanVien);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NhanVienExists(int id)
        {
            return db.NhanViens.Count(e => e.NV_ID == id) > 0;
        }
    }
}