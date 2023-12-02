using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AxisBank.Models;

namespace AxisBank.Controllers
{
    public class WorkerController : Controller
    {
        /*
     Summary
     author: S Rudra Prasad Choudhury
     date of creation: 27/10/2019    
     description : This is a Project of banking system. This is the Worker controller of the project.
      */

        // : Worker
        public ActionResult WorkerPanel()
        {
            return View();
        }

       
    }
}