using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.BLL;

namespace UniversityCourseAndResultManagementSystemApp.DAL
{
    public class RoomNoGetway
    {
        public List<RoomNumber> GetAllRoomNo()
        {
            List<RoomNumber> listOfRoomNo = new List<RoomNumber>();
            DBPlayer db = new DBPlayer();

            try
            {
                db.cmdText = "SELECT * FROM RoomNo";

                db.Open();
                SqlDataReader reader = db.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RoomNumber roomNo = new RoomNumber();
                        roomNo.RoomNo = reader["RoomNo"].ToString();
                        listOfRoomNo.Add(roomNo);
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

            return listOfRoomNo;
        }
    }
}