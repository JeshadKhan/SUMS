using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.BLL;

namespace UniversityCourseAndResultManagementSystemApp.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();

        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Department department)
        {
            try
            {
                string code = department.Code;
                string name = department.Name;

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
                else
                {
                    if (code.Length < 2 || code.Length > 7)
                    {
                        ViewBag.CodeErrorMessage = "Code length should be between two to seven character long.";
                        return View();
                    }
                    else
                    {
                        if (departmentManager.SaveDepartment(department))
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
    }
}