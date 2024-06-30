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
    public class WorkoutDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/WorkoutData/ListWorkouts
        [HttpGet]
        public IQueryable<Workout> ListWorkouts()
        {
            return db.Workouts;
        }

        // GET: api/WorkoutData/FindWorkout/5
        [ResponseType(typeof(Workout))]
        [HttpGet]
        public IHttpActionResult GetWorkout(int id)
        {
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }

        // POST: api/WorkoutData/UpdateWorkout/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateWorkout(int id, Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workout.WorkoutId)
            {
                return BadRequest();
            }

            db.Entry(workout).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
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

        // POST: api/WorkoutData/AddWorkout
        [ResponseType(typeof(Workout))]
        [HttpPost]
        public IHttpActionResult PostWorkout(Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Workouts.Add(workout);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workout.WorkoutId }, workout);
        }

        // POST]: api/WorkoutData/DeleteWorkout/5
        [ResponseType(typeof(Workout))]
        [HttpPost]
        public IHttpActionResult DeleteWorkout(int id)
        {
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return NotFound();
            }

            db.Workouts.Remove(workout);
            db.SaveChanges();

            return Ok(workout);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkoutExists(int id)
        {
            return db.Workouts.Count(e => e.WorkoutId == id) > 0;
        }
    }
}