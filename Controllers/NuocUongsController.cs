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
    public class NuocUongsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/NuocUongs
        public System.Object GetNuocUongs()
        {
            var result = (from a in db.NuocUongs



                          select new
                          {
                              a.Nuoc_ID,
                              a.Nuoc_Ten,
                              a.Nuoc_DonViTinh,
                              a.Nuoc_Gia,
                              a.Nuoc_HinhAnh,
                            
                          }).ToList();

            return result;
        }

        // GET: api/NuocUongs/5
        [ResponseType(typeof(NuocUong))]
        public IHttpActionResult GetNuocUong(int id)
        {
            NuocUong nuocUong = db.NuocUongs.Find(id);
            if (nuocUong == null)
            {
                return NotFound();
            }

            return Ok(nuocUong);
        }

        // PUT: api/NuocUongs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNuocUong(int id, NuocUong nuocUong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nuocUong.Nuoc_ID)
            {
                return BadRequest();
            }

            db.Entry(nuocUong).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NuocUongExists(id))
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

        // POST: api/NuocUongs
        [ResponseType(typeof(NuocUong))]
        public IHttpActionResult PostNuocUong(NuocUong nuocUong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NuocUongs.Add(nuocUong);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nuocUong.Nuoc_ID }, nuocUong);
        }

        // DELETE: api/NuocUongs/5
        [ResponseType(typeof(NuocUong))]
        public IHttpActionResult DeleteNuocUong(int id)
        {
            NuocUong nuocUong = db.NuocUongs.Find(id);
            if (nuocUong == null)
            {
                return NotFound();
            }

            db.NuocUongs.Remove(nuocUong);
            db.SaveChanges();

            return Ok(nuocUong);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NuocUongExists(int id)
        {
            return db.NuocUongs.Count(e => e.Nuoc_ID == id) > 0;
        }
    }
}