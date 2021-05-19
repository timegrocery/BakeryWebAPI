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
    public class IDsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/IDs
        public IQueryable<ID> GetIDs()
        {
            return db.IDs;
        }

        // GET: api/IDs/5
        [ResponseType(typeof(ID))]
        public IHttpActionResult GetID(string id)
        {
            ID iD = db.IDs.Find(id);
            if (iD == null)
            {
                return NotFound();
            }

            return Ok(iD);
        }

        // PUT: api/IDs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutID(string id, ID iD)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iD.username)
            {
                return BadRequest();
            }

            db.Entry(iD).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IDExists(id))
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

        // POST: api/IDs
        [ResponseType(typeof(ID))]
        public IHttpActionResult PostID(ID iD)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IDs.Add(iD);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (IDExists(iD.username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = iD.username }, iD);
        }

        // DELETE: api/IDs/5
        [ResponseType(typeof(ID))]
        public IHttpActionResult DeleteID(string id)
        {
            ID iD = db.IDs.Find(id);
            if (iD == null)
            {
                return NotFound();
            }

            db.IDs.Remove(iD);
            db.SaveChanges();

            return Ok(iD);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IDExists(string id)
        {
            return db.IDs.Count(e => e.username == id) > 0;
        }
    }
}