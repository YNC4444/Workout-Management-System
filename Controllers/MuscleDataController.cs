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
using PassionProjectn01681774.Models;

namespace PassionProjectn01681774.Controllers
{
    public class MuscleDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all muscles in the system
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all muscle groups in the database
        /// </returns>
        /// <example>
        /// GET: api/MuscleData/ListMuscles -->
        /// [{"MuscleId":2,"GroupName":"Push"},...
        /// {"MuscleId":3,"GroupName":"Pull"}]
        /// </example>
        [HttpGet]
        [ResponseType(typeof(Muscle))]
        public IHttpActionResult ListMuscles()
        {
            List<Muscle> Muscles = db.Muscles.ToList();
            return Ok(Muscles);
        }

        // GET: api/MuscleData/5
        [ResponseType(typeof(Muscle))]
        public IHttpActionResult GetMuscle(int id)
        {
            Muscle muscle = db.Muscles.Find(id);
            if (muscle == null)
            {
                return NotFound();
            }

            return Ok(muscle);
        }

        // PUT: api/MuscleData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMuscle(int id, Muscle muscle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != muscle.MuscleId)
            {
                return BadRequest();
            }

            db.Entry(muscle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MuscleExists(id))
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

        // POST: api/MuscleData
        [ResponseType(typeof(Muscle))]
        public IHttpActionResult PostMuscle(Muscle muscle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Muscles.Add(muscle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = muscle.MuscleId }, muscle);
        }

        // DELETE: api/MuscleData/5
        [ResponseType(typeof(Muscle))]
        public IHttpActionResult DeleteMuscle(int id)
        {
            Muscle muscle = db.Muscles.Find(id);
            if (muscle == null)
            {
                return NotFound();
            }

            db.Muscles.Remove(muscle);
            db.SaveChanges();

            return Ok(muscle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MuscleExists(int id)
        {
            return db.Muscles.Count(e => e.MuscleId == id) > 0;
        }
    }
}