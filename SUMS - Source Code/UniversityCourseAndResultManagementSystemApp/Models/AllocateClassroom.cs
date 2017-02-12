using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystemApp.Models
{
    public class AllocateClassroom
    {
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public string RoomNo { get; set; }
        public string Day { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime AllocationDate { get; set; }

        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string ScheduleInfo { get; set; }
    }
}