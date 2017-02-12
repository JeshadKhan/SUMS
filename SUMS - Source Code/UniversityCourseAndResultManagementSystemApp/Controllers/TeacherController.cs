using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.BLL;

namespace UniversityCourseAndResultManagementSystemApp.Controllers
{
    public class TeacherController : Controller
    {
        TeacherManager teacherManager = new TeacherManager();
        DepartmentManager departmentManager = new DepartmentManager();

        public ActionResult Save()
        {
            try
            {
                GetDesignationAndDepartment();
            }
            catch (Exception ex)
            {
                ViewBag.ExErrorMessage = ex.Message;
                return View();
            }

            return View();
        }

        private void GetDesignationAndDepartment()
        {
            try
            {
                List<Designation> listOfDesignations = teacherManager.GetAllDesignation();
                ViewBag.Designations = listOfDesignations;

                List<Department> listODepartments = departmentManager.GetAllDepartment();
                ViewBag.Departments = listODepartments;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Save(Teacher teacher)
        {
            try
            {
                GetDesignationAndDepartment();

                string name = teacher.Name;
                string email = teacher.Email;
                string contactNo = teacher.ContactNo;
                int designationId = teacher.DesignationId;
                int departmentId = teacher.DepartmentId;
                int credirToBeTaken = teacher.CreditToBeTaken;

                if (string.IsNullOrEmpty(name))
                {
                    ViewBag.NameErrorMessage = "Please provide name.";
                    return View();
                }
                else if (string.IsNullOrEmpty(email))
                {
                    ViewBag.EmailErrorMessage = "Please provide email.";
                    return View();
                }
                else if (string.IsNullOrEmpty(contactNo))
                {
                    ViewBag.ContactNoErrorMessage = "Please provide contact number.";
                    return View();
                }
                else if (designationId < 1)
                {
                    ViewBag.DesignationErrorMessage = "Select designation.";
                    return View();
                }
                else if (departmentId < 1)
                {
                    ViewBag.DepartmentErrorMessage = "Select department.";
                    return View();
                }
                else if (string.IsNullOrEmpty(credirToBeTaken.ToString().Trim()))
                {
                    ViewBag.CreditToBeTakenErrorMessage = "Please provide credit.";
                    return View();
                }
                else
                {
                    if (credirToBeTaken < 0)
                    {
                        ViewBag.CreditToBeTakenErrorMessage = "Credit should be non-negative value.";
                        return View();
                    }
                    else
                    {
                        if (teacherManager.SaveTeacher(teacher))
                        {
                            ViewBag.SaveMessage = "Teacher saved successfully.";
                        }
                        else
                        {
                            ViewBag.ExErrorMessage = "Failed to save.";
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

        public ActionResult Get()
        {
            try
            {
                List<Teacher> listOfTeacher = teacherManager.GetAllTeacher();
                ViewBag.Teachers = listOfTeacher;
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