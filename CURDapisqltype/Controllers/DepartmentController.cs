using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;
using Data.Interface;
using Newtonsoft.Json;

namespace CURDapisqltype.Controllers
{

    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDepartment  _dept;
        public DepartmentController(IConfiguration configuration, IDepartment dept)
        {
            _configuration = configuration;
            _dept = dept;
        }

            
    
        [Route("api/Department")]
        [HttpGet]
  
        public IActionResult Getdetails()
        {

          return _dept.Get();
        }



        [Route("api/Department/{id}")]

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {

            return _dept.GetDepartment(id);
        }


        [Route("api/Department")]
        [HttpPost]
        public IActionResult Create([FromBody] Department dep)
        {
            if (ModelState.IsValid)
            {
               _dept.AddDepartment(dep);
                return Ok("Added Successfully");
            }
            return BadRequest();
        }


        [Route("api/Department")]
        [HttpPut]
        public IActionResult Edit([FromBody] Department dep)
        {
            if (ModelState.IsValid)
            {
                _dept.UpdateDepartment(dep);
                return Ok("Updated Successfully");
            }
            return BadRequest();
        }

        [Route("api/Departmentdel/{id}")]
        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dept.DeleteDepartment(id);
            if (data == null)
            {
                return NotFound();
            }
            _dept.DeleteDepartment(id);
            return Ok("Deleted Successfully");
        }











      

        //////////////////////////////////////////////////////////////////////////////////////////////

        [Route("api/Departments")]
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select DepartmentId as ""DepartmentId"",DepartmentName as ""DepartmentName""
                   from Department ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);
        }


        [Route("api/Departments")]
        [HttpPost]
        public JsonResult Post(Department dep)
        {
            string query = @"
                insert into Department(DepartmentName)
                values (@DepartmentName) ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Added Successfully");
        }


        [Route("api/Departments")]
        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"
                update Department
                set DepartmentName = @DepartmentName
                where DepartmentId=@DepartmentId ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Updated Successfully");
        }

        [Route("api/Departments/{id}")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from Department
                where DepartmentId=@DepartmentId ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Successfully");
        }


    }
}
