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
    public class NguyenLieuxController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/NguyenLieux
        public System.Object GetNguyenLieux()
        {
            var result = (from a in db.NguyenLieux


                          select new
                          {
                              a.NL_ID,
                              a.NL_Ten,
                              a.NL_DonViTinh,
                              a.NL_SoLuong


                          }).ToList();

            return result;
        }
        // GET: api/NguyenLieux/5
        [ResponseType(typeof(NguyenLieu))]
        public IHttpActionResult GetNguyenLieu(int id)
        {
            NguyenLieu nguyenLieu = db.NguyenLieux.Find(id);
            if (nguyenLieu == null)
            {
                return NotFound();
            }

            return Ok(nguyenLieu);
        }

        // PUT: api/NguyenLieux/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNguyenLieu(int id, NguyenLieu nguyenLieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nguyenLieu.NL_ID)
            {
                return BadRequest();
            }

            db.Entry(nguyenLieu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NguyenLieuExists(id))
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

        // POST: api/NguyenLieux
        [ResponseType(typeof(NguyenLieu))]
        public IHttpActionResult PostNguyenLieu(NguyenLieu nguyenLieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NguyenLieux.Add(nguyenLieu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nguyenLieu.NL_ID }, nguyenLieu);
        }

        // DELETE: api/NguyenLieux/5
        [ResponseType(typeof(NguyenLieu))]
        public IHttpActionResult DeleteNguyenLieu(int id)
        {
            NguyenLieu nguyenLieu = db.NguyenLieux.Find(id);
            if (nguyenLieu == null)
            {
                return NotFound();
            }

            db.NguyenLieux.Remove(nguyenLieu);
            db.SaveChanges();

            return Ok(nguyenLieu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NguyenLieuExists(int id)
        {
            return db.NguyenLieux.Count(e => e.NL_ID == id) > 0;
        }
    }
}