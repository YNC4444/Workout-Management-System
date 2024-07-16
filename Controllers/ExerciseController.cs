using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PassionProjectn01681774.Models;
using System.Web.Script.Serialization;

namespace PassionProjectn01681774.Controllers
{
    public class ExerciseController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ExerciseController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384/api/ExerciseData/");
        }

        // GET: Exercise/List
        public ActionResult List()
        {
            // objective: communicate with our exercise data api to retrieve a list of exercises
            // curl https://localhost:44384/api/ExerciseData/ListExercises

            string url = "https://localhost:44384/api/ExerciseData/ListExercises";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<ExerciseDto> Exercises = response.Content.ReadAsAsync<IEnumerable<ExerciseDto>>().Result;
            //Debug.WriteLine("Number of exercises received : ");
            //Debug.WriteLine(Exercises.Count());

            return View(Exercises);
        }

        // GET: Exercise/Show/{id}
        public ActionResult Show(int id)
        {
            // objective: communicate with our exercise data api to retrieve a list of exercises
            // curl https://localhost:44384/api/ExerciseData/FindExercise/{id}

            string url = "https://localhost:44384/api/ExerciseData/FindExercise/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            ExerciseDto Exercise = response.Content.ReadAsAsync<ExerciseDto>().Result;
            //Debug.WriteLine("exercise received: ");
            //Debug.WriteLine(Exercise.ExerciseName);

            return View(Exercise);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Exercise/New
        public ActionResult New()
        {

            return View();
        }

        // POST: Exercise/Create
        [HttpPost]
        public ActionResult Create(Exercise exercise)
        {
            Debug.WriteLine("the json payload is: ");

            //objective: add a new exercise into our system using the API
            //curl -H "Content-Type:application/json" -d @exercise.json https://localhost:44384/api/ExerciseData/AddExercise 
            string url = "AddExercise";

            string jsonpayload = jss.Serialize(exercise);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Exercise/Edit/5
        public ActionResult Edit(int id)
        {
            //grab the exercise information

            //objective: communicate with our exercise data api to retrieve one exercise
            //curl -d @exercise.json "https://localhost:44384/api/ExerciseData/FindExercise/{id}"

            string url = "FindExercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            Exercise Exercise = response.Content.ReadAsAsync<Exercise>().Result;
            //Debug.WriteLine("exercise received : ");
            //Debug.WriteLine(Exercise.ExerciseName);

            return View(Exercise);
        }

        [HttpPost]
        public ActionResult Update(int id, Exercise exercise)
        {
            //curl -H "Content-Type:application/json" -d @exercise.json "https://localhost:44384/api/ExerciseData/UpdateExercise/{id}"

            try
            {
                //Debug.WriteLine("The new exercise info is:");
                //Debug.WriteLine(exercise.ExerciseName);
                //Debug.WriteLine(exercise.ExerciseDescription);
                //Debug.WriteLine(exercise.ExerciseWeight);

                //serialize into JSON
                //Send the request to the API

                string url = "UpdateExercise/" + id;

                string jsonpayload = jss.Serialize(exercise);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                //POST: api/ExerciseData/UpdateExercise/{id}
                //Header : Content-Type: application/json
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                return RedirectToAction("Show/" + id);
            }
            catch
            {
                return View();
            }
        }

        //get: animal/delete/5
        //public actionresult deleteconfirm(int id)
        //{
        //    if (exercise == null)
        //    {
        //        return httpnotfound();
        //    }

        //    return view();
        //}

        // get: exercise/delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindExercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            Exercise exercise = response.Content.ReadAsAsync<Exercise>().Result;
            return View(exercise);
        }

        // POST: Animal/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // curl -d "" https://localhost:44384/api/ExerciseData/DeleteExercise/{id}

            try
            {
                string url = "DeleteExercise/" + id;
                HttpContent content = new StringContent("");

                client.PostAsync(url, content);

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        //// POST: Exercise/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    string url = "ExerciseData/DeleteExercise/" + id;
        //    HttpContent content = new StringContent("");
        //    content.Headers.ContentType.MediaType = "application/json";
        //    HttpResponseMessage response = client.PostAsync(url, content).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("List");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Error");
        //    }
        //}

    }
}