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
using WebApplication4;

namespace WebApplication4.Controllers
{
    public class PresidentsController : ApiController
    {
        private CLUBDBEntities db = new CLUBDBEntities();

        // GET: api/Presidents
        public IQueryable<President> GetPresidents()
        {
            return db.Presidents;
        }

        // GET: api/Presidents/5
        [ResponseType(typeof(President))]
        public IHttpActionResult GetPresident(int id)
        {
            President president = db.Presidents.Find(id);
            if (president == null)
            {
                return NotFound();
            }

            return Ok(president);
        }

        // PUT: api/Presidents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPresident(int id, President president)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != president.PresidentID)
            {
                return BadRequest();
            }

            db.Entry(president).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PresidentExists(id))
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

        // POST: api/Presidents
        [ResponseType(typeof(President))]
        public IHttpActionResult PostPresident(President president)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Presidents.Add(president);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PresidentExists(president.PresidentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = president.PresidentID }, president);
        }

        // DELETE: api/Presidents/5
        [ResponseType(typeof(President))]
        public IHttpActionResult DeletePresident(int id)
        {
            President president = db.Presidents.Find(id);
            if (president == null)
            {
                return NotFound();
            }

            db.Presidents.Remove(president);
            db.SaveChanges();

            return Ok(president);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PresidentExists(int id)
        {
            return db.Presidents.Count(e => e.PresidentID == id) > 0;
        }
    }
}