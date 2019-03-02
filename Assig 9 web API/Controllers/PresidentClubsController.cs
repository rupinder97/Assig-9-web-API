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
    public class PresidentClubsController : ApiController
    {
        private CLUBDBEntities db = new CLUBDBEntities();

        // GET: api/PresidentClubs
        public IQueryable<PresidentClub> GetPresidentClubs()
        {
            return db.PresidentClubs;
        }

        // GET: api/PresidentClubs/5
        [ResponseType(typeof(PresidentClub))]
        public IHttpActionResult GetPresidentClub(int id)
        {
            PresidentClub presidentClub = db.PresidentClubs.Find(id);
            if (presidentClub == null)
            {
                return NotFound();
            }

            return Ok(presidentClub);
        }

        // PUT: api/PresidentClubs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPresidentClub(int id, PresidentClub presidentClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != presidentClub.ID)
            {
                return BadRequest();
            }

            db.Entry(presidentClub).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PresidentClubExists(id))
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

        // POST: api/PresidentClubs
        [ResponseType(typeof(PresidentClub))]
        public IHttpActionResult PostPresidentClub(PresidentClub presidentClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PresidentClubs.Add(presidentClub);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = presidentClub.ID }, presidentClub);
        }

        // DELETE: api/PresidentClubs/5
        [ResponseType(typeof(PresidentClub))]
        public IHttpActionResult DeletePresidentClub(int id)
        {
            PresidentClub presidentClub = db.PresidentClubs.Find(id);
            if (presidentClub == null)
            {
                return NotFound();
            }

            db.PresidentClubs.Remove(presidentClub);
            db.SaveChanges();

            return Ok(presidentClub);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PresidentClubExists(int id)
        {
            return db.PresidentClubs.Count(e => e.ID == id) > 0;
        }
    }
}