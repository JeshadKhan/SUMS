using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.DAL;

namespace UniversityCourseAndResultManagementSystemApp.BLL
{
    public class StudentManager
    {
        public bool SaveStudent(Student student)
        {
            try
            {
                if (IsStudentExist(student.Email))
                {
                    throw new Exception("Student already exist.");
                }
                else
                {
                    StudentGetway studentGetway = new StudentGetway();
                    return studentGetway.SaveStudent(student);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsStudentExist(string studentEmail)
        {
            try
            {
                StudentGetway studentGetway = new StudentGetway();
                return studentGetway.IsStudentExist(studentEmail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Student> GetAllStudent()
        {
            try
            {
                StudentGetway studentGetway = new StudentGetway();
                return studentGetway.GetAllStudent();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public bool EnrollCourse(EnrollCourse enrollCourse)
        {
            try
            {
                if (IsStudentEnrollCourseExist(enrollCourse))
                {
                    throw new Exception("Student already enrolled in this course.");
                }
                else
                {
                    StudentGetway studentGetway = new StudentGetway();
                    return studentGetway.EnrollCourse(enrollCourse);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsStudentEnrollCourseExist(EnrollCourse enrollCourse)
        {
            try
            {
                StudentGetway studentGetway = new StudentGetway();
                return studentGetway.IsStudentEnrollCourseExist(enrollCourse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Student> GetAllStudentCourse()
        {
            try
            {
                StudentGetway studentGetway = new StudentGetway();
                return studentGetway.GetAllStudentCourse();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool StudentResult(StudentResult studentResult)
        {
            try
            {
                if (IsStudentResultExist(studentResult))
                {
                    throw new Exception("Student result already saved.");
                }
                else
                {
                    StudentGetway studentGetway = new StudentGetway();
                    return studentGetway.StudentResult(studentResult);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsStudentResultExist(StudentResult studentResult)
        {
            try
            {
                StudentGetway studentGetway = new StudentGetway();
                return studentGetway.IsStudentResultExist(studentResult);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetStudentResultReportByStudentId(int studentId)
        {
            try
            {
                StudentGetway studentGetway = new StudentGetway();
                return studentGetway.GetStudentResultReportByStudentId(studentId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}