using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Reporting.WebForms;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.BLL;
using UniversityCourseAndResultManagementSystemApp.Report;
using CrystalDecisions.CrystalReports.Engine;

namespace UniversityCourseAndResultManagementSystemApp.Controllers
{
    public class StudentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        StudentManager studentManager = new StudentManager();
        CourseManager courseManager = new CourseManager();

        public ActionResult Save()
        {
            try
            {
                GetDepartments();

                DateTime now = DateTime.Now;
                string today = now.ToShortDateString();
                ViewBag.Today = today;
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }

        private void GetDepartments()
        {
            try
            {
                List<Department> listOfDepartment = departmentManager.GetAllDepartment();
                ViewBag.Departments = listOfDepartment;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Save(Student student)
        {
            try
            {
                GetDepartments();

                string name = student.Name;
                string email = student.Email;
                string contactNo = student.ContactNo;
                string departmentCode = student.DepartmentCode;

                if (string.IsNullOrEmpty(name))
                {
                    ViewBag.NameErrorMessage = "Please provide name.";
                    return View();
                }
                else if (string.IsNullOrEmpty(email))
                {
                    ViewBag.EnailErrorMessage = "Please provide email.";
                    return View();
                }
                else if (string.IsNullOrEmpty(contactNo))
                {
                    ViewBag.ContactNoErrorMessage = "Please provide contact number.";
                    return View();
                }
                else if (string.IsNullOrEmpty(departmentCode))
                {
                    ViewBag.DepartmentErrorMessage = "Please select department.";
                    return View();
                }
                else
                {
                    if (studentManager.SaveStudent(student))
                    {
                        ViewBag.SaveMessage = "Student registered successfully.";
                    }
                    else
                    {
                        ViewBag.ExErrorMessage = "Failed to register.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }

        public JsonResult GetDepartmentByDepartmentId(int departmentId)
        {
            var dept = departmentManager.GetAllDepartment();
            var deptList = dept.Where(d => d.Id == departmentId).ToList();
            return Json(deptList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EnrollCourse()
        {
            try
            {
                GetStudentsAndCourses();

                DateTime now = DateTime.Now;
                string today = now.ToShortDateString();
                ViewBag.Today = today;
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }

        private void GetStudentsAndCourses()
        {
            try
            {
                List<Student> listOfStudent = studentManager.GetAllStudent();
                ViewBag.Students = listOfStudent;

                List<Course> listOfCourse = courseManager.GetAllCourse();
                ViewBag.Courses = listOfCourse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult EnrollCourse(EnrollCourse enrollCourse)
        {
            try
            {
                GetStudentsAndCourses();

                int studentId = enrollCourse.StudentId;
                int courseId = enrollCourse.CourseId;

                if (studentId < 1)
                {
                    ViewBag.StudentErrorMessage = "Please select student.";
                    return View();
                }
                else if (courseId < 1)
                {
                    ViewBag.CourseErrorMessage = "Please select course.";
                    return View();
                }
                else
                {
                    if (studentManager.EnrollCourse(enrollCourse))
                    {
                        ViewBag.SaveMessage = "Student enrolled in course successfully.";
                    }
                    else
                    {
                        ViewBag.ExErrorMessage = "Failed to enroll course.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }

        public JsonResult GetStudentByStudentId(int studentId)
        {
            var student = studentManager.GetAllStudent();
            var studentList = student.Where(s => s.Id == studentId).ToList();
            return Json(studentList, JsonRequestBehavior.AllowGet);
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
                GetStudentsAndCourses();
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }

        public ActionResult StudentResult()
        {
            try
            {
                GetStudentsAndCourses();
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }

        [HttpPost]
        public ActionResult StudentResult(StudentResult studentResult)
        {
            try
            {
                GetStudentsAndCourses();

                int studentId = studentResult.StudentId;
                int courseId = studentResult.CourseId;

                if (studentId < 1)
                {
                    ViewBag.StudentErrorMessage = "Please select student.";
                    return View();
                }
                else if (courseId < 1)
                {
                    ViewBag.CourseErrorMessage = "Please select course.";
                    return View();
                }
                else
                {
                    if (studentManager.StudentResult(studentResult))
                    {
                        ViewBag.SaveMessage = "Student result saved successfully.";
                    }
                    else
                    {
                        ViewBag.ExErrorMessage = "Failed to save result.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }

        public JsonResult GetCourseByStudentId(int studentId)
        {
            var student = studentManager.GetAllStudentCourse();
            var studentList = student.Where(s => s.Id == studentId).ToList();
            return Json(studentList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewResult()
        {
            try
            {
                GetStudentsAndCourses();
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }

        [HttpPost]
        public ActionResult ViewResult(Student student)
        {
            try
            {
                GetStudentsAndCourses();
                string saveSheetname = "Shudent Result Sheet of " + student.Name + ".pdf";

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Report/ShudentResultSheet.rpt")));
                rd.SetDataSource(studentManager.GetStudentResultReportByStudentId(student.StudentId));
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", saveSheetname);
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
            }

            return View();
        }
    }
}