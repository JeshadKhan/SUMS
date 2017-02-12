using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystemApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }

        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }

        public string Grade { get; set; }

        public int StudentId { get; set; }
    }
}