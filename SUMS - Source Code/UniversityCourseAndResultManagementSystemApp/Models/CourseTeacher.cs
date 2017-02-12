using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystemApp.Models
{
    public class CourseTeacher
    {
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public bool CourseAssignStatus { get; set; }

        public int CourseCredit { get; set; }
    }
}