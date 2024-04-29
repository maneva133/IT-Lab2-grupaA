using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    public class EventController : Controller
    {
      

         public static List<EventModel> events = new List<EventModel>()
        {
            new EventModel(){Id=1, Name="Grozdober",Location="Kavadarci"},
            new EventModel(){Id=2,Name="PivoF",Location="Prilep"},
            new EventModel(){Id=3,Name="ZSlavejce",Location="Skopje"}
        };

        public ActionResult tableEvents()
        {
            return View(events);
        }

       

        [HttpPost]
        public ActionResult addEvents(EventModel model)
        {
            
            if (ModelState.IsValid)
            {
                int newId = events.Count + 1;
                model.Id = newId;
                Console.WriteLine("Name: " + model.Name);
                Console.WriteLine("Location: " + model.Location);
                events.Add(model);
                return RedirectToAction("EventDetails", new { eventId = newId });
            }
            else
            {
                return View(model);
            }
            
        }
       public ActionResult addEvents()
        {
            var newEventModel = new EventModel();

            return View(newEventModel);
        }


        public ActionResult EventDetails(int eventId)
        {
            EventModel e = events[eventId - 1];
            

            return View(e);
        }

        public ActionResult UpdateEvent(int id)
        {
            // Пронајди настан со соодветното Id
            var eventToUpdate = events.FirstOrDefault(e => e.Id == id);

            if (eventToUpdate == null)
            {
                return HttpNotFound();
            }

            return View(eventToUpdate);
        }

        [HttpPost]
        public ActionResult UpdateEvent(EventModel updatedEvent)
        {
            if (ModelState.IsValid)
            {
                int index = events.FindIndex(e => e.Id == updatedEvent.Id);

                if (index != -1)
                {
                    events[index] = updatedEvent;

                    return RedirectToAction("tableEvents");
                }
                else
                {
                    return HttpNotFound();
                }
            }

            return View(updatedEvent);
        }
        [HttpPost]
        public ActionResult DeleteEvent(int eventId)
        {
            var eventToDelete = events[eventId];
            if (eventToDelete == null)
            {
                return HttpNotFound();
            }

            events.Remove(eventToDelete);

            return RedirectToAction("tableEvents");
        }
    }
}