using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripLog.Models;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace TripLog.Controllers
{
    public class TripController : Controller
    {
        private readonly TripContext _context;
        //public List<Trip> trips = new List<Trip>();

        public TripController(TripContext context)
        {
            _context = context;
        }
        // GET: /Trip/
        public IActionResult Index()
        {
            
            return View();
        }

        // GET: /Trip/Add
        public IActionResult Add()
        {
                return View();
        }

        // POST: /Trip/Add
        [HttpPost]
        public IActionResult Add(IFormCollection form)
        {
            // Read form data and store in TempData
            TempData["Destination"] = form["Destination"];
            TempData["Accommodation"] = form["Accommodation"];
            TempData["StartDate"] = form["StartDate"];
            TempData["EndDate"] = form["EndDate"];

            // Redirect to Page 2
            return RedirectToAction("AddPage2");
        }

        // GET: /Trip/Add/Page2
        public IActionResult AddPage2()
        {
            return View();
        }

        // POST: /Trip/Add/Page2
        [HttpPost]
        public IActionResult AddPage2(IFormCollection form)
        {
            // Read form data and store in TempData
            TempData["AccommodationPhone"] = form["AccommodationPhone"];
            TempData["AccommodationEmail"] = form["AccommodationEmail"];

            return RedirectToAction("AddPage3");
        }

        // GET: /Trip/Add/Page3
        public IActionResult AddPage3()
        {
            return View();
        }

        // POST: /Trip/Add/Page3
        [HttpPost]
        public IActionResult AddPage3(IFormCollection form)
        {
            TempData["ThingToDo1"] = form["ThingToDo1"];
            TempData["ThingToDo2"] = form["ThingToDo2"];
            TempData["ThingToDo3"] = form["ThingToDo3"];

            return RedirectToAction("Save"); ;
        }
        public IActionResult Save()
        {
            try
            {
                if (TempData.ContainsKey("Id")) {
                    TempData["Id"] = (int)TempData["Id"] + 1;
                } else
                {
                    TempData["Id"] = 0;
                }
                
                var tripRow = new Trip
                {

                    TripId = (int)TempData["Id"] + 1,
                    Destination = TempData.ContainsKey("Destination") ? ((String[])TempData["Destination"])[0] : "",
                    StartDate = TempData.ContainsKey("StartDate") ? DateTime.Parse(((String[])TempData["StartDate"])[0]) : DateTime.MinValue,
                    EndDate = TempData.ContainsKey("EndDate") ? DateTime.Parse(((String[])TempData["EndDate"])[0]) : DateTime.MinValue,
                    Accommodation = TempData.ContainsKey("Accommodation") ? ((String[])TempData["Accommodation"])[0] : "",
                    AccommodationPhone = TempData.ContainsKey("AccommodationPhone") ? ((String[])TempData["AccommodationPhone"])[0] : "",
                    AccommodationEmail = TempData.ContainsKey("AccommodationEmail") ? ((String[])TempData["AccommodationEmail"])[0] : "",
                    ThingToDo1 = TempData.ContainsKey("ThingToDo1") ? ((String[])TempData["ThingToDo1"])[0] : "",
                    ThingToDo2 = TempData.ContainsKey("ThingToDo2") ? ((String[])TempData["ThingToDo2"])[0] : "",
                    ThingToDo3 = TempData.ContainsKey("ThingToDo3") ? ((String[])TempData["ThingToDo3"])[0] : "",
                };

                // Add the model to TempData and trips list
                if(TempData.ContainsKey("TripRows"))
                {
                    List<Trip> trips = JsonConvert.DeserializeObject<List<Trip>>(TempData["TripRows"].ToString());
                    trips.Add(tripRow);
                    TempData["TripRows"] = JsonConvert.SerializeObject(trips);
                }
                else
                {
                    List<Trip> trips = new List<Trip>();
                    trips.Add(tripRow);
                    TempData["TripRows"] = JsonConvert.SerializeObject(trips);
                }
                // TempData["TripRow"] = tripRow;
                TempData["message"] = $"Trip to {tripRow.Destination} added ";
                //trips.Add(tripRow);
                //TempData["TripRows"] = JsonConvert.SerializeObject(trips);
            }catch(NullReferenceException e) { TempData["message"] = e.Message; }

            return RedirectToAction("Index");
        }
        public IActionResult Cancel()
        {
            TempData.Clear();
            return RedirectToAction("Index");
        }
    }
}
