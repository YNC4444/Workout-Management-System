using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PassionProjectn01681774.Models;
using System.Web.Script.Serialization;
using System.Security.Policy;
// distinction made between ViewModels and Models bc ViewModels stores information
// while Models create the database
using PassionProjectn01681774.Models.ViewModels;

namespace PassionProjectn01681774.Controllers
{
    public class WorkoutController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static WorkoutController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384/api/");
        }

        // GET: Workout/List
        public ActionResult List()
        {
            // objective: communicate with our workout data api to retrieve a list of workouts
            // curl https://localhost:44384/api/WorkoutData/ListWorkouts

            string url = "WorkoutData/ListWorkouts";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<WorkoutDto> workouts = response.Content.ReadAsAsync<IEnumerable<WorkoutDto>>().Result;

            //Debug.WriteLine("Number of workouts received : ");
            //Debug.WriteLine(workouts.Count());

            return View(workouts);
        }

        // GET: Workout/Show/5
        public ActionResult Show(int id)
        {
            // objective: communicate with our workout data api to retrieve a list of workouts
            // curl https://localhost:44384/api/WorkoutData/FindWorkout/{id}

            string url = "WorkoutData/FindWorkout/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            WorkoutDto Workout = response.Content.ReadAsAsync<WorkoutDto>().Result;
            //Debug.WriteLine("workout received: ");
            //Debug.WriteLine(Workout.WorkoutDate);

            return View(Workout);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Workout/New
        public ActionResult New()
        {
            // information about the exercises in the system
            //GET api/Exercise
            string url = "ExerciseData/ListExercises";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<Exercise> ExerciseOptions = response.Content.ReadAsAsync<IEnumerable<Exercise>>().Result;

            return View(ExerciseOptions);
        }

        // POST: Workout/Create
        [HttpPost]
        public ActionResult Create(Workout workout)
        {
            // objective: add a new workout into our system using the API
            //curl - H "Content-Type:application/json" - d @workout.json https://localhost:44384/api/WorkoutData/AddWorkout
            Debug.WriteLine("the json payload is: ");
            Debug.WriteLine(workout.WorkoutDate);

            string url = "WorkoutData/AddWorkout";

            string jsonpayload = jss.Serialize(workout);

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
        //objective: communicate with our workout data api to retrieve one workout
        //curl -d @workout.json "https://localhost:44384/api/WorkoutData/FindWorkout/{id}"
        // GET: Workout/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateWorkout ViewModel = new UpdateWorkout();
            
            // the existing workout information
            string url = "WorkoutData/FindWorkout/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            WorkoutDto Workout = response.Content.ReadAsAsync<WorkoutDto>().Result;
            ViewModel.Workout = Workout;

            // all exercises to choose from when updating this workout
            url = "ExerciseData/ListExercises";
            response = client.GetAsync(url).Result;
            IEnumerable<ExerciseDto> ExercisdOptions = response.Content.ReadAsAsync<IEnumerable<ExerciseDto>>().Result;

            ViewModel.ExerciseOptions = ExercisdOptions;

            return View(ViewModel);
        }

        // POST: Workout/Update/5
        [HttpPost]
        public ActionResult Update(int id, Workout workout)
        {
            //curl -H "Content-Type:application/json" -d @workout.json "https://localhost:44384/api/WorkoutData/UpdateWorkout/{id}"

            try
            {
                //Debug.WriteLine("The new workout info is:");
                //Debug.WriteLine(workout.WorkoutId);
                //Debug.WriteLine(workout.WorkoutDate);

                //serialize into JSON
                //Send the request to the API

                string url = "WorkoutData/UpdateWorkout/" + id;

                string jsonpayload = jss.Serialize(workout);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                //POST: api/WorkoutData/UpdateWorkout/{id}
                //Header : Content-Type: application/json
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                return RedirectToAction("Show/" + id);

            }
            catch
            {
                return View();
            }
        }

        // GET: Workout/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "WorkoutData/FindWorkout/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            Workout workout = response.Content.ReadAsAsync<Workout>().Result;
            return View(workout);
        }

        // POST: Workout/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // curl -d "" https://localhost:44384/api/WorkoutData/DeleteWorkout/{id}

            string url = "WorkoutData/DeleteWorkout/" + id;
            HttpContent content = new StringContent("");
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
    }
}
