using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
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
        public IEnumerable<WorkoutDto> ListWorkouts()
        {
            List<Workout> Workouts = db.Workouts.ToList();
            List<WorkoutDto> WorkoutDtos = new List<WorkoutDto>();

            Workouts.ForEach(a => WorkoutDtos.Add(new WorkoutDto()
            {
                WorkoutId = a.WorkoutId,
                muscleId = a.muscleId,
                WorkoutDate = a.WorkoutDate
            }));

            return WorkoutDtos;
        }

        // GET: api/WorkoutData/FindWorkout/5
        [ResponseType(typeof(Workout))]
        [HttpGet]
        public IHttpActionResult FindWorkout(int id)
        {
            Workout Workout = db.Workouts.Find(id);
            WorkoutDto WorkoutDto = new WorkoutDto()
            {
                WorkoutId = Workout.WorkoutId,
                muscleId = Workout.muscleId,
                WorkoutDate = Workout.WorkoutDate
            };
            if (Workout == null)
            {
                return NotFound();
            }

            return Ok(WorkoutDto);
        }

        // POST: api/WorkoutData/UpdateWorkout/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateWorkout(int id, Workout workout)
        {
            Debug.WriteLine("Reached update workout method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != workout.WorkoutId)
            {
                Debug.WriteLine("Id mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + workout.WorkoutId);
                Debug.WriteLine("POST parameter" + workout.WorkoutDate);
                Debug.WriteLine("POST parameter" + workout.muscleId);

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
                    Debug.WriteLine("workout not found");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("none of the conditions triggered");
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

            return Ok();
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