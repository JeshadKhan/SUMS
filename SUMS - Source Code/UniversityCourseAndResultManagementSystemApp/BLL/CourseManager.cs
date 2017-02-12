using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.DAL;

namespace UniversityCourseAndResultManagementSystemApp.BLL
{
    public class CourseManager
    {
        public bool SaveCourse(Course course)
        {
            try
            {
                CourseGetway courseGetway = new CourseGetway();

                if (IsValidCourseCode(course.Code))
                {
                    throw new Exception("Course code already exist.");
                }
                else
                {
                    if (IsValidCourseName(course.Name))
                    {
                        throw new Exception("Course name already exist.");
                    }
                    else
                    {
                        return courseGetway.SaveCourse(course);
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private bool IsValidCourseCode(string courseCode)
        {
            try
            {
                CourseGetway courseGetway = new CourseGetway();
                return courseGetway.IsValidCourseCode(courseCode);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private bool IsValidCourseName(string courseName)
        {
            try
            {
                CourseGetway courseGetway = new CourseGetway();
                return courseGetway.IsValidCourseName(courseName);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<Course> GetAllCourse()
        {
            try
            {
                CourseGetway courseGetway = new CourseGetway();
                return courseGetway.GetAllCourse();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AssignCourseTeacher(CourseTeacher courseTeacher)
        {
            try
            {
                if (IsCourseTeacherPairExist(courseTeacher))
                {
                    throw new Exception("This course and teacher assign already exist.");
                }
                else
                {
                    if (IsCourseAssign(courseTeacher.CourseId))
                    {
                        throw new Exception("Course already assigned.");
                    }
                    else
                    {
                        CourseGetway courseGetway = new CourseGetway();
                        return courseGetway.AssignCourseTeacher(courseTeacher);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsCourseAssign(int courseId)
        {
            try
            {
                CourseGetway courseGetway = new CourseGetway();
                return courseGetway.IsCourseAssign(courseId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsCourseTeacherPairExist(CourseTeacher courseTeacher)
        {
            try
            {
                CourseGetway courseGetway = new CourseGetway();
                return courseGetway.IsCourseTeacherPairExist(courseTeacher);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CourseTeacherSemestersView> GetCourseTeacherSemesters()
        {
            try
            {
                CourseGetway courseGetway = new CourseGetway();
                return courseGetway.GetCourseTeacherSemesters();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UnassignCourses()
        {
            try
            {
                CourseGetway courseGetway = new CourseGetway();
                return courseGetway.UnassignCourses();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}