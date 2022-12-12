using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Data.Interface
{
    public interface IDepartment
    {

        JsonResult AddDepartment(Department dep);
        JsonResult UpdateDepartment(Department dep);
        JsonResult DeleteDepartment(int id);
        JsonResult GetDepartment(int id);
        JsonResult Get();
    }
}
