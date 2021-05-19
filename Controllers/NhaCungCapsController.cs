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
    public class NhaCungCapsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/NhaCungCaps
        public System.Object GetNhaCungCaps()
        {
            var result = (from a in db.NhaCungCaps


                          select new
                          {
                              a.NCC_ID,
                              a.NCC_Ten,
                              a.NCC_SDT,
                              a.NCC_DiaChi

                          }).ToList();

            return result;
        }

        // GET: api/NhaCungCaps/5
        [ResponseType(typeof(NhaCungCap))]
        public IHttpActionResult GetNhaCungCap(int id)
        {
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return Ok(nhaCungCap);
        }

        // PUT: api/NhaCungCaps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNhaCungCap(int id, NhaCungCap nhaCungCap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nhaCungCap.NCC_ID)
            {
                return BadRequest();
            }

            db.Entry(nhaCungCap).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhaCungCapExists(id))
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


        // POST: api/NhaCungCaps
        [ResponseType(typeof(NhaCungCap))]
        public IHttpActionResult PostNhaCungCap(NhaCungCap nhaCungCap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NhaCungCaps.Add(nhaCungCap);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nhaCungCap.NCC_ID }, nhaCungCap);
        }

        // DELETE: api/NhaCungCaps/5
        [ResponseType(typeof(NhaCungCap))]
        public IHttpActionResult DeleteNhaCungCap(int id)
        {
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            db.NhaCungCaps.Remove(nhaCungCap);
            db.SaveChanges();

            return Ok(nhaCungCap);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NhaCungCapExists(int id)
        {
            return db.NhaCungCaps.Count(e => e.NCC_ID == id) > 0;
        }
    }
}