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
    public class ChiTietMonAnsController : ApiController
    {
        private DBModel db = new DBModel();


        // GET: api/ChiTietMonAns
        public System.Object GetChiTietMonAns()
        {
            var result = (from a in db.ChiTietMonAns
                          join b in db.MonAns on a.MonAn_ID equals b.MonAn_ID
                          join c in db.NguyenLieux on a.NL_ID equals c.NL_ID

                          select new
                          {
                              a.CTMonAn_ID,
                              c.NL_Ten,
                              a.ChiTietMonAn_SoLuong,
                              a.ChiTietMonAn_DonViTinh,
                              a.GhiChu,
                              a.MonAn_ID

                          }).ToList();

            return result;
        }
        // GET: api/ChiTietMonAns/5
        [ResponseType(typeof(ChiTietMonAn))]
        public IHttpActionResult GetChiTietMonAn(int id)
        {
            ChiTietMonAn chiTietMonAn = db.ChiTietMonAns.Find(id);
            if (chiTietMonAn == null)
            {
                return NotFound();
            }

            return Ok(chiTietMonAn);
        }

        // PUT: api/ChiTietMonAns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChiTietMonAn(int id, ChiTietMonAn chiTietMonAn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chiTietMonAn.CTMonAn_ID)
            {
                return BadRequest();
            }

            db.Entry(chiTietMonAn).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietMonAnExists(id))
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

        // POST: api/ChiTietMonAns
        [ResponseType(typeof(ChiTietMonAn))]
        public IHttpActionResult PostChiTietMonAn(ChiTietMonAn chiTietMonAn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChiTietMonAns.Add(chiTietMonAn);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = chiTietMonAn.CTMonAn_ID }, chiTietMonAn);
        }

        // DELETE: api/ChiTietMonAns/5
        [ResponseType(typeof(ChiTietMonAn))]
        public IHttpActionResult DeleteChiTietMonAn(int id)
        {
            ChiTietMonAn chiTietMonAn = db.ChiTietMonAns.Find(id);
            if (chiTietMonAn == null)
            {
                return NotFound();
            }

            db.ChiTietMonAns.Remove(chiTietMonAn);
            db.SaveChanges();

            return Ok(chiTietMonAn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChiTietMonAnExists(int id)
        {
            return db.ChiTietMonAns.Count(e => e.CTMonAn_ID == id) > 0;
        }
    }
}