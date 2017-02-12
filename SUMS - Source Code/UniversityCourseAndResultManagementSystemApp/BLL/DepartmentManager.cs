using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystemApp.Models;
using UniversityCourseAndResultManagementSystemApp.DAL;

namespace UniversityCourseAndResultManagementSystemApp.BLL
{
    public class DepartmentManager
    {
        public bool SaveDepartment(Department department)
        {
            DepartmentGetway departmentGetway = new DepartmentGetway();

            try
            {
                if (IsValidDeptCode(department.Code))
                {
                    throw new Exception("Department code already exist.");
                }
                else
                {
                    if (IsValidDeptName(department.Name))
                    {
                        throw new Exception("Department name already exist.");
                    }
                    else
                    {
                        return departmentGetway.SaveDepartment(department);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsValidDeptCode(string deptCode)
        {
            try
            {
                DepartmentGetway departmentGetway = new DepartmentGetway();
                return departmentGetway.IsValidDeptCode(deptCode);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsValidDeptName(string deptName)
        {
            try
            {
                DepartmentGetway departmentGetway = new DepartmentGetway();
                return departmentGetway.IsValidDeptName(deptName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Department> GetAllDepartment()
        {
            try
            {
                DepartmentGetway departmentGetway = new DepartmentGetway();
                return departmentGetway.GetAllDepartment();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}