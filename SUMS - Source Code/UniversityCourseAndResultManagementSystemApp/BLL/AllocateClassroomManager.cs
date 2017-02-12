using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.DAL;

namespace UniversityCourseAndResultManagementSystemApp.BLL
{
    public class AllocateClassroomManager
    {
        public bool Save(AllocateClassroom allocateClassroom)
        {
            try
            {
                if (IsOverlapping(allocateClassroom))
                {
                    throw new Exception("Schedule already exist.");
                }
                else
                {
                    AllocateClassroomGetway allocateClassroomGetway = new AllocateClassroomGetway();
                    return allocateClassroomGetway.Save(allocateClassroom);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsOverlapping(AllocateClassroom allocateClassroom)
        {
            try
            {
                AllocateClassroomGetway allocateClassroomGetway = new AllocateClassroomGetway();
                return allocateClassroomGetway.IsOverlapping(allocateClassroom);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AllocateClassroom> GetAllAllocateClassroom()
        {
            try
            {
                AllocateClassroomGetway allocateClassroomGetway = new AllocateClassroomGetway();
                return allocateClassroomGetway.GetAllAllocateClassroom();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsScheduleExist(AllocateClassroom allocateClassroom)
        {
            try
            {
                AllocateClassroomGetway allocateClassroomGetway = new AllocateClassroomGetway();
                return allocateClassroomGetway.IsScheduleExist(allocateClassroom);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UnallocateAllClassroom()
        {
            try
            {
                AllocateClassroomGetway allocateClassroomGetway = new AllocateClassroomGetway();
                return allocateClassroomGetway.UnallocateAllClassroom();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}