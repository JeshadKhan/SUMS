using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.BLL;

namespace UniversityCourseAndResultManagementSystemApp.Controllers
{
    public class AllocateClassroomsController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        RoomNoManager roomNoManager = new RoomNoManager();
        CourseManager courseManager = new CourseManager();
        AllocateClassroomManager allocateClassroomManager = new AllocateClassroomManager();

        public ActionResult AllocateClassroom()
        {
            try
            {
                GetDepartmentAndRoomNo();
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }

        private void GetDepartmentAndRoomNo()
        {
            try
            {
                List<Department> listOfDepartment = departmentManager.GetAllDepartment();
                ViewBag.Departments = listOfDepartment;

                List<RoomNumber> listOfRoomNo = roomNoManager.GetAllRoomNo();
                ViewBag.RoomNoList = listOfRoomNo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult AllocateClassroom(AllocateClassroom allocateClassroom)
        {
            try
            {
                GetDepartmentAndRoomNo();

                int deptId = allocateClassroom.DepartmentId;
                int courseId = allocateClassroom.CourseId;
                string roomNo = allocateClassroom.RoomNo;
                string day = allocateClassroom.Day;
                string fromTime = allocateClassroom.FromTime.ToString();
                string toTime = allocateClassroom.ToTime.ToString();

                if (deptId < 1)
                {
                    ViewBag.DepartmentErrorMessage = "Please select department";
                    return View();
                }
                else if (courseId < 1)
                {
                    ViewBag.CourseErrorMessage = "Please select course";
                    return View();
                }
                else if (roomNo == "--Select--")
                {
                    ViewBag.RoomNoErrorMessage = "Please select room number";
                    return View();
                }
                else if (day == "--Select--")
                {
                    ViewBag.DayErrorMessage = "Please select day";
                    return View();
                }
                else if (string.IsNullOrEmpty(fromTime))
                {
                    ViewBag.FromTimeErrorMessage = "Please provide class start time";
                    return View();
                }
                else if (string.IsNullOrEmpty(toTime))
                {
                    ViewBag.ToTimeErrorMessage = "Please provide class end time";
                    return View();
                }
                else
                {
                    if (allocateClassroomManager.IsScheduleExist(allocateClassroom))
                    {
                        ViewBag.ExErrorMessage = "Schedule already exist.";
                        return View();
                    }
                    else
                    {
                        if (allocateClassroomManager.Save(allocateClassroom))
                        {
                            ViewBag.SaveMessage = "Classroom allocated.";
                        }
                        else
                        {
                            ViewBag.ExErrorMessage = "Failed to allocate classroom.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }

        public JsonResult GetCourseByDepartmentId(int departmentId)
        {
            var course = courseManager.GetAllCourse();
            var courseList = course.Where(c => c.DepartmentId == departmentId).ToList();
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get()
        {
            try
            {
                GetDepartmentAndRoomNo();
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }

        public JsonResult GetAllocateClassroomsByDepartmentId(int departmentId)
        {
            var allocateClassroom = allocateClassroomManager.GetAllAllocateClassroom();
            var allocateClassroomList = allocateClassroom.Where(ac => ac.DepartmentId == departmentId).ToList();
            return Json(allocateClassroomList, JsonRequestBehavior.AllowGet);
        }
    }
}