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
    public class BanAnsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/BanAns
        public System.Object GetBanAns()
        {
            var result = (from a in db.BanAns


                          select new
                          {
                              a.Ban_ID,
                              a.Ban_Ten,
                              a.Ban_Loai,
                              a.Ban_SoNguoi,
                              a.Ban_TrangThai

                          }).ToList();

            return result;
        }

        // GET: api/BanAns/5
        [ResponseType(typeof(BanAn))]
        public IHttpActionResult GetBanAn(int id)
        {
            BanAn banAn = db.BanAns.Find(id);
            if (banAn == null)
            {
                return NotFound();
            }

            return Ok(banAn);
        }

        // PUT: api/BanAns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBanAn(int id, BanAn banAn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != banAn.Ban_ID)
            {
                return BadRequest();
            }

            db.Entry(banAn).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BanAnExists(id))
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

        // POST: api/BanAns
        [ResponseType(typeof(BanAn))]
        public IHttpActionResult PostBanAn(BanAn banAn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BanAns.Add(banAn);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = banAn.Ban_ID }, banAn);
        }

        // DELETE: api/BanAns/5
        [ResponseType(typeof(BanAn))]
        public IHttpActionResult DeleteBanAn(int id)
        {
            BanAn banAn = db.BanAns.Find(id);
            if (banAn == null)
            {
                return NotFound();
            }

            db.BanAns.Remove(banAn);
            db.SaveChanges();

            return Ok(banAn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BanAnExists(int id)
        {
            return db.BanAns.Count(e => e.Ban_ID == id) > 0;
        }
    }
}