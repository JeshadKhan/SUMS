using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystemApp.Models;

namespace UniversityCourseAndResultManagementSystemApp.DAL
{
    public class SemesterGetway
    {
        public List<Semester> GetAllSemester()
        {
            List<Semester> listOfSemester = new List<Semester>();
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT * FROM Semesters";

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Semester semester = new Semester();
                        semester.Id = int.Parse(reader["Id"].ToString());
                        semester.Name = reader["Name"].ToString();
                        listOfSemester.Add(semester);
                    }
                }

                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Close();
            }

            return listOfSemester;
        }
    }
}