using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.BLL;

namespace UniversityCourseAndResultManagementSystemApp.Controllers
{
    public class CourseController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        SemesterManager semesterManager = new SemesterManager();
        CourseManager courseManager = new CourseManager();
        TeacherManager teacherManager = new TeacherManager();

        public ActionResult Save()
        {
            GetDepartmentAndSeester();
            return View();
        }

        private void GetDepartmentAndSeester()
        {
            List<Department> listOfDepartment = departmentManager.GetAllDepartment();
            ViewBag.Departments = listOfDepartment;

            List<Semester> listOfSemester = semesterManager.GetAllSemester();
            ViewBag.Semesters = listOfSemester;
        }

        [HttpPost]
        public ActionResult Save(Course course)
        {
            GetDepartmentAndSeester();

            try
            {
                string code = course.Code;
                string name = course.Name;
                float credit = course.Credit;
                int departmentId = course.DepartmentId;
                int semesterId = course.SemesterId;

                if (string.IsNullOrEmpty(code))
                {
                    ViewBag.CodeErrorMessage = "Please provide code.";
                    return View();
                }
                else if (string.IsNullOrEmpty(name))
                {
                    ViewBag.NameErrorMessage = "Please provide name.";
                    return View();
                }
                else if (string.IsNullOrEmpty(credit.ToString()))
                {
                    ViewBag.CreditErrorMessage = "Please provide credit.";
                    return View();
                }
                else if (departmentId < 1)
                {
                    ViewBag.DepartmentErrorMessage = "Please select department.";
                    return View();
                }
                else if (semesterId < 1)
                {
                    ViewBag.SemesterErrorMessage = "Please select semester.";
                    return View();
                }
                else
                {
                    if (code.Length < 5)
                    {
                        ViewBag.CodeErrorMessage = "Code length should be at least five character long.";
                        return View();
                    }
                    else
                    {
                        if (credit < 0.5 || credit > 5)
                        {
                            ViewBag.CreditErrorMessage = "Credit should be numeric value and between half to five.";
                            return View();
                        }
                        else
                        {
                            if (courseManager.SaveCourse(course))
                            {
                                ViewBag.SaveMessage = "Saved successfully.";
                            }
                            else
                            {
                                ViewBag.ExErrorMessage = "Failed to save.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
                return View();
            }

            return View();
        }

        public ActionResult Get()
        {
            try
            {
                List<Course> listOfCourse = courseManager.GetAllCourse();
                ViewBag.Courses = listOfCourse;
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
                return View();
            }

            return View();
        }

        public ActionResult CourseTeacher()
        {
            try
            {
                GetDepartmentAndCourse();
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        private void GetDepartmentAndCourse()
        {
            try
            {
                List<Department> listOfDepartment = departmentManager.GetAllDepartment();
                ViewBag.Departments = listOfDepartment;

                List<Course> listOfCourse = courseManager.GetAllCourse();
                ViewBag.Courses = listOfCourse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult CourseTeacher(CourseTeacher courseTeacher)
        {
            try
            {
                GetDepartmentAndCourse();

                int deptId = courseTeacher.DepartmentId;
                int teacherId = courseTeacher.TeacherId;
                int courseId = courseTeacher.CourseId;

                if (deptId < 1)
                {
                    ViewBag.DepartmentErrorMessage = "Please select department.";
                    return View();
                }
                else if (teacherId < 1)
                {
                    ViewBag.TeacherErrorMessage = "Please select teacher.";
                    return View();
                }
                else if (courseId < 1)
                {
                    ViewBag.CourseErrorMessage = "Please select course.";
                    return View();
                }
                else
                {
                    if (courseManager.AssignCourseTeacher(courseTeacher))
                    {
                        ViewBag.SaveMessage = "Course assign to teacher successfully.";
                    }
                    else
                    {
                        ViewBag.ExErrorMessage = "Failed to assigning course.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
                return View();
            }

            return View();
        }

        public JsonResult GetTeacherByDepartmentId(int departmentId)
        {
            var dept = teacherManager.GetAllTeacher();
            var deptList = dept.Where(d => d.DepartmentId == departmentId).ToList();
            return Json(deptList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseByDepartmentId(int departmentId)
        {
            var course = courseManager.GetAllCourse();
            var courseList = course.Where(c => c.DepartmentId == departmentId).ToList();
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacherByTeacherId(int teacherId)
        {
            var teacher = teacherManager.GetAllTeacher();
            var techerList = teacher.Where(d => d.Id == teacherId).ToList();
            return Json(techerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseByCourseId(int courseId)
        {
            var course = courseManager.GetAllCourse();
            var courseList = course.Where(c => c.Id == courseId).ToList();
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewCourseStatics()
        {
            try
            {
                List<Department> listOfDepartment = departmentManager.GetAllDepartment();
                ViewBag.Departments = listOfDepartment;
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
                return View();
            }

            return View();
        }

        public JsonResult GetCourseStaticsByDepartmentId(int departmentId)
        {
            var courseTeacherSemesters = courseManager.GetCourseTeacherSemesters();
            var courseTeacherSemestersList = courseTeacherSemesters.Where(cts => cts.DepartmentId == departmentId).ToList();
            return Json(courseTeacherSemestersList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnassignCourses()
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
        public ActionResult UnassignCourses(UnassignCourse unassignCourse)
        {
            try
            {
                if (courseManager.UnassignCourses())
                {
                    ViewBag.SaveMessage = "All the courses will be unassigned.";
                }
                else
                {
                    ViewBag.ExErrorMessage = "Failed to unassign courses.";
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