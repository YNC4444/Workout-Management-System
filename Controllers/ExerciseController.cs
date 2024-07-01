using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PassionProjectn01681774.Models;

namespace PassionProjectn01681774.Controllers
{
    public class ExerciseController : Controller
    {
        // GET: Exercise/List
        public ActionResult List()
        {   
            HttpClient client = new HttpClient();
            string url = "https://localhost:44384/api/ExerciseData/ListExercises";

            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<Exercise> Exercises = response.Content.ReadAsAsync<IEnumerable<Exercise>>().Result;
            return View(Exercises);
        }

        // GET: Exercise/Show/{id}
        public ActionResult Show(int id)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:44384/api/ExerciseData/FindExercises/"+id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Exercise Exercise = response.Content.ReadAsAsync<Exercise>().Result;
            return View(Exercise);
        }
    }
}