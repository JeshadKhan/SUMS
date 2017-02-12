using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.DAL;

namespace UniversityCourseAndResultManagementSystemApp.BLL
{
    public class RoomNoManager
    {
        public List<RoomNumber> GetAllRoomNo()
        {
            try
            {
                RoomNoGetway roomNoGetway = new RoomNoGetway();
                return roomNoGetway.GetAllRoomNo();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}