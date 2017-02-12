using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystemApp.Models
{
    public class StudentResult
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Grade { get; set; }
    }
}