using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.DAL;

namespace UniversityCourseAndResultManagementSystemApp.BLL
{
    public class TeacherManager
    {
        public List<Designation> GetAllDesignation()
        {
            try
            {
                TeacherGetway teacherGetway = new TeacherGetway();
                return teacherGetway.GetAllDesignation();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SaveTeacher(Teacher teacher)
        {
            try
            {
                if (IsTeacherExist(teacher.Email))
                {
                    throw new Exception("Teacher already exist.");
                }
                else
                {
                    TeacherGetway teacherGetway = new TeacherGetway();
                    return teacherGetway.SaveTeacher(teacher);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsTeacherExist(string teacherEmail)
        {
            try
            {
                TeacherGetway teacherGetway = new TeacherGetway();
                return teacherGetway.IsTeacherExist(teacherEmail);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Teacher> GetAllTeacher()
        {
            try
            {
                TeacherGetway teacherGetway = new TeacherGetway();
                return teacherGetway.GetAllTeacher();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}