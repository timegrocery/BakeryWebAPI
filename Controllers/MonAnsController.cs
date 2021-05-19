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
    public class MonAnsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/MonAns
        public System.Object GetMonAns()
        {
            var result = (from a in db.MonAns
                          


                          select new
                          {
                              a.MonAn_ID,
                              a.MonAn_Ten,
                              a.MonAn_Loai,
                              a.MonAn_Gia,
                              a.MonAn_HinhAnh,
                              a.MonAn_DonViTinh,
                              a.MonAn_TrangThai
                          }).ToList();

            return result;
        }
        // GET: api/MonAns/5
        [ResponseType(typeof(MonAn))]
        public IHttpActionResult GetMonAn(int id)
        {
            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return NotFound();
            }

            return Ok(monAn);
        }

        // PUT: api/MonAns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMonAn(int id, MonAn monAn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != monAn.MonAn_ID)
            {
                return BadRequest();
            }

            db.Entry(monAn).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonAnExists(id))
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

        // POST: api/MonAns
        [ResponseType(typeof(MonAn))]
        public IHttpActionResult PostMonAn(MonAn monAn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MonAns.Add(monAn);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = monAn.MonAn_ID }, monAn);
        }

        // DELETE: api/MonAns/5
        [ResponseType(typeof(MonAn))]
        public IHttpActionResult DeleteMonAn(int id)
        {
            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return NotFound();
            }

            db.MonAns.Remove(monAn);
            db.SaveChanges();

            return Ok(monAn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MonAnExists(int id)
        {
            return db.MonAns.Count(e => e.MonAn_ID == id) > 0;
        }
    }
}