using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystemApp.Models
{
    public class CourseTeacherSemestersView
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string SemesterName { get; set; }
        public string AssignTeacherName { get; set; }
        public int DepartmentId { get; set; }
    }
}