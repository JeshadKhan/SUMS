using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.BLL;
using System.Data;

namespace UniversityCourseAndResultManagementSystemApp.DAL
{
    public class StudentGetway
    {
        public bool SaveStudent(Student student)
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                if (string.IsNullOrEmpty(student.Address))
                {
                    student.Address = string.Empty;
                }

                //DateTime now = DateTime.Now;
                //string year = now.Year.ToString();
                string year = DateTime.Parse(student.Date.ToString()).Year.ToString();
                int studentCount = GetStudentNumber(student.DepartmentId, year) + 1;

                db.cmdText = "INSERT INTO Students(RegNo, Name, Email, ContactNo, Date, Address, DepartmentId) VALUES(@RegNo, @Name, @Email, @ContactNo, @Date, @Address, @DepartmentId)";

                db.command.Parameters.AddWithValue("@RegNo", student.DepartmentCode + "-" + year + "-" + GetThreeDigitNumber(studentCount));
                db.command.Parameters.AddWithValue("@Name", student.Name);
                db.command.Parameters.AddWithValue("@Email", student.Email);
                db.command.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                db.command.Parameters.AddWithValue("@Date", student.Date);
                db.command.Parameters.AddWithValue("@Address", student.Address);
                db.command.Parameters.AddWithValue("@DepartmentId", student.DepartmentId);

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

        private string GetThreeDigitNumber(int studentCount)
        {
            string number = "000";

            if (studentCount < 10)
            {
                number = "00" + studentCount.ToString();
            }
            else if (studentCount > 9 && studentCount < 100)
            {
                number = "0" + studentCount.ToString();
            }
            else
            {
                number = studentCount.ToString();
            }

            return number;
        }

        private int GetStudentNumber(int studentDepartmentId, string year)
        {
            int studentCount = 0;
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT COUNT(*) AS StudentCount FROM Students WHERE DepartmentId = @DepartmentId AND YEAR(Students.Date) = @Year";
                db.command.Parameters.AddWithValue("@DepartmentId", studentDepartmentId);
                db.command.Parameters.AddWithValue("@Year", year);

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        studentCount = int.Parse(reader["StudentCount"].ToString());
                        break;
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

            return studentCount;
        }

        public bool IsStudentExist(string studentEmail)
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT Email FROM Students WHERE Email = @Email";
                db.command.Parameters.AddWithValue("@Email", studentEmail);

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

        public List<Student> GetAllStudent()
        {
            List<Student> listOfStudent = new List<Student>();
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT * FROM StudentDepartmentView";

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.Id = int.Parse(reader["Id"].ToString());
                        student.RegNo = reader["RegNo"].ToString();
                        student.Name = reader["Name"].ToString();
                        student.Email = reader["Email"].ToString();
                        student.DepartmentId = int.Parse(reader["DepartmentId"].ToString());
                        student.DepartmentName = reader["DepartmentName"].ToString();
                        listOfStudent.Add(student);
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

            return listOfStudent;
        }

        public bool EnrollCourse(EnrollCourse enrollCourse)
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "INSERT INTO StudentEnrollCourse(StudentId, CourseId, Date, Status) VALUES(@StudentId, @CourseId, @Date, @Status)";

                db.command.Parameters.AddWithValue("@StudentId", enrollCourse.StudentId);
                db.command.Parameters.AddWithValue("@CourseId", enrollCourse.CourseId);
                db.command.Parameters.AddWithValue("@Date", enrollCourse.Date);
                db.command.Parameters.AddWithValue("@Status", true);

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

        public bool IsStudentEnrollCourseExist(EnrollCourse enrollCourse)
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT * FROM StudentEnrollCourse WHERE StudentId = @StudentId AND CourseId = @CourseId";

                db.command.Parameters.AddWithValue("@StudentId", enrollCourse.StudentId);
                db.command.Parameters.AddWithValue("@CourseId", enrollCourse.CourseId);

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

        public List<Student> GetAllStudentCourse()
        {
            List<Student> listOfStudent = new List<Student>();
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT * FROM StudentCourseView";

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.Id = int.Parse(reader["Id"].ToString());
                        student.RegNo = reader["RegNo"].ToString();
                        student.Name = reader["Name"].ToString();
                        student.Email = reader["Email"].ToString();
                        student.DepartmentName = reader["DepartmentName"].ToString();

                        if (string.IsNullOrEmpty(reader["CourseId"].ToString().Trim()))
                        {
                            student.CourseId = 0;
                        }
                        else
                        {
                            student.CourseId = int.Parse(reader["CourseId"].ToString());
                        }

                        student.CourseName = reader["CourseName"].ToString();
                        student.CourseCode = reader["CourseCode"].ToString();

                        bool status = false;
                        if (string.IsNullOrEmpty(reader["Status"].ToString()))
                        {
                            status = false;
                        }
                        else
                        {
                            status = bool.Parse(reader["Status"].ToString());
                        }

                        if (status)
                        {
                            if (string.IsNullOrEmpty(reader["Grade"].ToString().Trim()))
                            {
                                student.Grade = "Not Graded Yet";
                            }
                            else
                            {
                                student.Grade = reader["Grade"].ToString();
                            }
                        }
                        else
                        {
                            student.Grade = string.Empty;
                        }

                        listOfStudent.Add(student);
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

            return listOfStudent;
        }

        public bool StudentResult(StudentResult studentResult)
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "INSERT INTO StudentResults(StudentId, CourseId, Grade) VALUES(@StudentId, @CourseId, @Grade)";

                db.command.Parameters.AddWithValue("@StudentId", studentResult.StudentId);
                db.command.Parameters.AddWithValue("@CourseId", studentResult.CourseId);
                db.command.Parameters.AddWithValue("@Grade", studentResult.Grade);

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

        public bool IsStudentResultExist(StudentResult studentResult)
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT * FROM StudentResults WHERE StudentId = @StudentId AND CourseId = @CourseId";

                db.command.Parameters.AddWithValue("@StudentId", studentResult.StudentId);
                db.command.Parameters.AddWithValue("@CourseId", studentResult.CourseId);

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

        public DataTable GetStudentResultReportByStudentId(int studentId)
        {
            DataTable dt = new DataTable();
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "EXEC Report_StudentResultSheet @Id = @StudentId";
                db.command.Parameters.AddWithValue("@StudentId", studentId);

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
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

            return dt;
        }
    }
}