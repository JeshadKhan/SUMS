using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.BLL;

namespace UniversityCourseAndResultManagementSystemApp.Controllers
{
    public class UnallocateClassroomsController : Controller
    {
        AllocateClassroomManager allocateClassroomManager = new AllocateClassroomManager();

        public ActionResult UnallocateClassroom()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
                return View();
            }

            return View();
        }
        
        [HttpPost]
        public ActionResult UnallocateClassroom(UnallocateClassroom unallocateClassroom)
        {
            try
            {
                if (allocateClassroomManager.UnallocateAllClassroom())
                {
                    ViewBag.SaveMessage = "All classroom successfully unallocated.";
                }
                else
                {
                    ViewBag.ExErrorMessage = "Failed to unallocate all classroom.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
                return View();
            }

            return View();
        }
    }
}