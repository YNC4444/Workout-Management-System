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
    public class ExerciseDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ExerciseData/ListExercises
        [HttpGet]
        public IQueryable<Exercise> ListExercises()
        {
            return db.Exercises;
        }

        // GET: api/ExerciseData/FindExercise/5
        [ResponseType(typeof(Exercise))]
        [HttpGet]
        public IHttpActionResult FindExercise(int id)
        {
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        // POST: api/ExerciseData/UpdateExercise/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateExercise(int id, Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exercise.ExerciseId)
            {
                return BadRequest();
            }

            db.Entry(exercise).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(id))
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

        // POST: api/ExerciseData/AddExercise
        [ResponseType(typeof(Exercise))]
        [HttpPost]
        public IHttpActionResult AddExercise(Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Exercises.Add(exercise);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = exercise.ExerciseId }, exercise);
        }

        // POST: api/ExerciseData/DeleteExercise/5
        [ResponseType(typeof(Exercise))]
        [HttpPost]  
        public IHttpActionResult DeleteExercise(int id)
        {
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return NotFound();
            }

            db.Exercises.Remove(exercise);
            db.SaveChanges();

            return Ok(exercise);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExerciseExists(int id)
        {
            return db.Exercises.Count(e => e.ExerciseId == id) > 0;
        }
    }
}