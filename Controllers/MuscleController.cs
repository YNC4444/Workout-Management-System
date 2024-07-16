using PassionProjectn01681774.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PassionProjectn01681774.Controllers
{
    public class MuscleController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static MuscleController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384/api/");
        }

        // GET: Muscle/List
        public ActionResult List()
        {
            // objective: communicate with our muscle data api to retrieve a list of muscles
            // curl https://localhost:44384/api/MuscleData/ListMuscles

            string url = "MuscleData/ListMuscles";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<Muscle> Muscles = response.Content.ReadAsAsync<IEnumerable<Muscle>>().Result;
            //Debug.WriteLine("Number of muscles received : ");
            //Debug.WriteLine(Muscles.Count());

            return View(Muscles);
        }

        // GET: Muscle/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Muscle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Muscle/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Muscle/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Muscle/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Muscle/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Muscle/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
