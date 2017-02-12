using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.BLL;
using System.Data.SqlClient;

namespace UniversityCourseAndResultManagementSystemApp.DAL
{
    public class TeacherGetway
    {
        public List<Designation> GetAllDesignation()
        {
            List<Designation> lisdtDesignations = new List<Designation>();
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT * FROM Designations";

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Designation designation = new Designation();
                        designation.Id = int.Parse(reader["Id"].ToString());
                        designation.Name = reader["Name"].ToString();
                        lisdtDesignations.Add(designation);
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

            return lisdtDesignations;
        }

        public bool SaveTeacher(Teacher teacher)
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "INSERT INTO Teachers(Name, Address, Email, ContactNo, DesignationId, DepartmentId, CreditToBeTaken, RemainingCredit) VALUES(@Name, @Address, @Email, @ContactNo, @DesignationId, @DepartmentId, @CreditToBeTaken, @RemainingCredit)";

                db.command.Parameters.AddWithValue("@Name", teacher.Name);
                db.command.Parameters.AddWithValue("@Address", teacher.Address);
                db.command.Parameters.AddWithValue("@Email", teacher.Email);
                db.command.Parameters.AddWithValue("@ContactNo", teacher.ContactNo);
                db.command.Parameters.AddWithValue("@DesignationId", teacher.DesignationId);
                db.command.Parameters.AddWithValue("@DepartmentId", teacher.DepartmentId);
                db.command.Parameters.AddWithValue("@CreditToBeTaken", teacher.CreditToBeTaken);
                db.command.Parameters.AddWithValue("@RemainingCredit", teacher.CreditToBeTaken);

                db.Open();
                int rowsAffected = db.command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Close();
            }

            return flag;
        }

        public bool IsTeacherExist(string teacherEmail)
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT Email FROM Teachers WHERE Email = @Email";
                db.command.Parameters.AddWithValue("@Email", teacherEmail);

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
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

            return flag;
        }

        public List<Teacher> GetAllTeacher()
        {
            List<Teacher> listOfTeacher = new List<Teacher>();
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT * FROM TeacherDesignationDepartmentView";

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Teacher teacher = new Teacher();
                        teacher.Id = int.Parse(reader["Id"].ToString());
                        teacher.Name = reader["Name"].ToString();
                        teacher.Address = reader["Address"].ToString();
                        teacher.Email = reader["Email"].ToString();
                        teacher.ContactNo = reader["ContactNo"].ToString();
                        teacher.DesignationName = reader["DesignationName"].ToString();
                        teacher.DepartmentId = int.Parse(reader["DepartmentId"].ToString());
                        teacher.DepartmentName = reader["DepartmentName"].ToString();
                        teacher.CreditToBeTaken = int.Parse(reader["CreditToBeTaken"].ToString());
                        teacher.RemainingCredit = int.Parse(reader["RemainingCredit"].ToString());
                        listOfTeacher.Add(teacher);
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

            return listOfTeacher;
        }
    }
}