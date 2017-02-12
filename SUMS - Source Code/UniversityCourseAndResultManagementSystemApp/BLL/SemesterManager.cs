using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.DAL;

namespace UniversityCourseAndResultManagementSystemApp.BLL
{
    public class SemesterManager
    {
        public List<Semester> GetAllSemester()
        {
            try
            {
                SemesterGetway semesterGetway = new SemesterGetway();
                return semesterGetway.GetAllSemester();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}