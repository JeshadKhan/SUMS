using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.BLL;
using System.Data.SqlClient;

namespace UniversityCourseAndResultManagementSystemApp.DAL
{
    public class AllocateClassroomGetway
    {
        public bool Save(AllocateClassroom allocateClassroom)
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                string fromTime = allocateClassroom.FromTime.ToString("hh:mm tt");
                string toTime = allocateClassroom.ToTime.ToString("hh:mm tt");

                TimeSpan start = TimeSpan.Parse(allocateClassroom.FromTime.ToString("HH:mm"));
                TimeSpan end = TimeSpan.Parse(allocateClassroom.ToTime.ToString("HH:mm"));
                TimeSpan duration = allocateClassroom.ToTime - allocateClassroom.FromTime;

                db.cmdText = "INSERT INTO AllocateClassrooms(DepartmentId, CourseId, RoomNo, Day, FromTime, ToTime, StartTime, EndTime, Duration, AllocationDate, Status) VALUES(@DepartmentId, @CourseId, @RoomNo, @Day, @FromTime, @ToTime, @StartTime, @EndTime, @Duration, @AllocationDate, @Status)";

                db.command.Parameters.AddWithValue("@DepartmentId", allocateClassroom.DepartmentId);
                db.command.Parameters.AddWithValue("@CourseId", allocateClassroom.CourseId);
                db.command.Parameters.AddWithValue("@RoomNo", allocateClassroom.RoomNo);
                db.command.Parameters.AddWithValue("@Day", allocateClassroom.Day);
                db.command.Parameters.AddWithValue("@FromTime", fromTime);
                db.command.Parameters.AddWithValue("@ToTime", toTime);
                db.command.Parameters.AddWithValue("@StartTime", start);
                db.command.Parameters.AddWithValue("@EndTime", end);
                db.command.Parameters.AddWithValue("@Duration", duration);
                db.command.Parameters.AddWithValue("@AllocationDate", DateTime.Now);
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

        public bool IsOverlapping(AllocateClassroom allocateClassroom)
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                string fromTime = allocateClassroom.FromTime.ToString("hh:mm tt");
                string toTime = allocateClassroom.ToTime.ToString("hh:mm tt");

                db.cmdText = "SELECT RoomNo, Day, FromTime, ToTime FROM AllocateClassrooms WHERE RoomNo = @RoomNo AND Day = @Day AND FromTime = @FromTime AND ToTime = @ToTime AND Status = @Status";

                db.command.Parameters.AddWithValue("@RoomNo", allocateClassroom.RoomNo);
                db.command.Parameters.AddWithValue("@Day", allocateClassroom.Day);
                db.command.Parameters.AddWithValue("@FromTime", fromTime);
                db.command.Parameters.AddWithValue("@ToTime", toTime);
                db.command.Parameters.AddWithValue("@Status", true);

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

        public List<AllocateClassroom> GetAllAllocateClassroom()
        {
            List<AllocateClassroom> listOfAllocateClassroom = new List<AllocateClassroom>();
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT * FROM AllocateClassroomAndCourseView";

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AllocateClassroom allocateClassroom = new AllocateClassroom();
                        allocateClassroom.DepartmentId = int.Parse(reader["DepartmentId"].ToString());
                        allocateClassroom.CourseCode = reader["CourseCode"].ToString();
                        allocateClassroom.CourseName = reader["CourseName"].ToString();
                        allocateClassroom.ScheduleInfo = GetScheduleInfoByCourseCode(allocateClassroom.CourseCode);

                        if (listOfAllocateClassroom == null)
                        {
                            listOfAllocateClassroom.Add(allocateClassroom);
                        }
                        else
                        {
                            if (!IsCourseCodeExistInList(listOfAllocateClassroom, allocateClassroom.CourseCode))
                            {
                                listOfAllocateClassroom.Add(allocateClassroom);
                            }
                        }
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

            return listOfAllocateClassroom;
        }

        private bool IsCourseCodeExistInList(List<AllocateClassroom> listOfAllocateClassroom, string courseCode)
        {
            bool flag = false;

            try
            {
                foreach (AllocateClassroom item in listOfAllocateClassroom)
                {
                    if (item.CourseCode == courseCode)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return flag;
        }

        private string GetScheduleInfoByCourseCode(string courseCode)
        {
            String scheduleInfo = string.Empty;
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT * FROM AllocateClassroomAndCourseView WHERE CourseCode = @CourseCode";
                db.command.Parameters.AddWithValue("@CourseCode", courseCode);

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bool status = false;

                        if (string.IsNullOrEmpty(reader["ACStatus"].ToString().Trim()))
                        {
                            status = false;
                        }
                        else
                        {
                            status = bool.Parse(reader["ACStatus"].ToString());
                        }


                        if (status)
                        {
                            if (scheduleInfo == "Not Scheduled Yet")
                            {
                                scheduleInfo = string.Empty;
                            }

                            string roomNo = reader["RoomNo"].ToString();
                            string day = reader["Day"].ToString();
                            string fromTime = reader["FromTime"].ToString();
                            string toTime = reader["ToTime"].ToString();

                            if (string.IsNullOrEmpty(roomNo + day + fromTime + toTime))
                            {
                                scheduleInfo = "Not Scheduled Yet";
                            }
                            else
                            {
                                scheduleInfo += "R. No: " + roomNo + ", " + day + ", " + fromTime + " - " + toTime + "<br />";
                            }
                        }
                        else
                        {
                            scheduleInfo = "Not Scheduled Yet";
                        }
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

            return scheduleInfo;
        }

        public bool IsScheduleExist(AllocateClassroom allocateClassroom)
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                TimeSpan fromTimeCurrent = TimeSpan.Parse(allocateClassroom.FromTime.ToString("HH:mm"));
                TimeSpan toTimeCurrent = TimeSpan.Parse(allocateClassroom.ToTime.ToString("HH:mm"));

                db.cmdText = "SELECT * FROM AllocateClassrooms WHERE RoomNo = @RoomNo AND Day = @Day AND Status = @Status";

                db.command.Parameters.AddWithValue("@RoomNo", allocateClassroom.RoomNo);
                db.command.Parameters.AddWithValue("@Day", allocateClassroom.Day);
                db.command.Parameters.AddWithValue("@Status", true);

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TimeSpan startTime = TimeSpan.Parse(reader["StartTime"].ToString());
                        TimeSpan endTime = TimeSpan.Parse(reader["EndTime"].ToString());

                        if (fromTimeCurrent >= startTime && fromTimeCurrent <= endTime)
                        {
                            flag = true;
                            break;
                        }
                        else if (toTimeCurrent >= startTime && toTimeCurrent <= endTime)
                        {
                            flag = true;
                            break;
                        }
                        else if (startTime >= fromTimeCurrent && startTime <= toTimeCurrent)
                        {
                            flag = true;
                            break;
                        }
                        else if (endTime >= fromTimeCurrent && endTime <= toTimeCurrent)
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            flag = false;
                        }
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

            return flag;
        }

        public bool UnallocateAllClassroom()
        {
            bool flag = false;
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "UPDATE AllocateClassrooms SET Status = @Status";
                db.command.Parameters.AddWithValue("@Status", false);

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
    }
}