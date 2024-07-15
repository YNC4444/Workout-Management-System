﻿using System;
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
using PassionProjectn01681774.Migrations;
using PassionProjectn01681774.Models;

namespace PassionProjectn01681774.Controllers
{
    public class ExerciseDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all exercises in the system
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all exercises in the database, including the weight they were last performed at
        /// </returns>
        /// <example>
        /// GET: api/ExerciseData/ListExercises -->
        /// [{"ExerciseId":2,"ExerciseName":"Deadlift","GroupName":"legs","ExerciseDescription":"Place feet shoulder-width apart, ..."},
        /// {"ExerciseId":3,"ExerciseName":"Hip thrust","GroupName":"legs","ExerciseDescription":"Tighten belt such that it ..."}]
        /// </example>
        [HttpGet]
        [Route("api/ExerciseData/ListExercises")]
        [ResponseType(typeof(ExerciseDto))]
        public IHttpActionResult ListExercises()
        {
            List<Exercise> Exercises = db.Exercises.ToList();
            List<ExerciseDto> ExerciseDtos = new List<ExerciseDto>();

            Exercises.ForEach(e => ExerciseDtos.Add(new ExerciseDto()
            {
                ExerciseId = e.ExerciseId,
                ExerciseName = e.ExerciseName,
                GroupName = e.Muscle.GroupName,
                ExerciseDescription = e.ExerciseDescription
            }));

            return Ok(ExerciseDtos);
        }

        /// <summary>
        /// Returns all exercises in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An exercise in the system matching up to the exercise ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the exercise</param>
        /// <example>
        /// GET: api/ExerciseData/FindExercise/5
        /// </example>
        [ResponseType(typeof(ExerciseDto))]
        [HttpGet]
        public IHttpActionResult FindExercise(int id)
        {
            Exercise Exercise = db.Exercises.Find(id);
            ExerciseDto ExerciseDto = new ExerciseDto()
            {
                ExerciseId = Exercise.ExerciseId,
                ExerciseName = Exercise.ExerciseName,
                GroupName = Exercise.Muscle.GroupName,
                ExerciseDescription = Exercise.ExerciseDescription
            };
            if (Exercise == null)
            {
                Debug.WriteLine("Exercise not found");

                return NotFound();
            }

            return Ok(ExerciseDto);
        }


        /// <summary>
        /// Updates a particular exercise in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Exercise ID primary key</param>
        /// <param name="exercise">JSON FORM DATA of an exercise</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/ExerciseData/UpdateExercise/5
        /// FORM DATA: Exercise JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateExercise(int id, Exercise exercise)
        {
            Debug.WriteLine("Reached update exercise method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != exercise.ExerciseId)
            {
                Debug.WriteLine("Id mismatch");
                Debug.WriteLine("GET parameter: " + id);
                Debug.WriteLine("POST parameter" + exercise.ExerciseId);
                Debug.WriteLine("POST parameter" + exercise.ExerciseName);
                Debug.WriteLine("POST parameter" + exercise.ExerciseDescription);

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
                    Debug.WriteLine("Exercise not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// Adds an exercise to the system
        /// </summary>
        /// <param name="exercise">JSON FORM DATA of an exercise</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Exercise ID, Exercise Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/ExerciseData/AddExercise
        /// FORM DATA: Exercise JSON Object
        /// </example>
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

        /// <summary>
        /// Deletes an exercise from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the exercise</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/ExerciseData/DeleteExercise/5
        /// FORM DATA: (empty)
        /// </example>
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

            return Ok(exercise); //deleted exercise parameter
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